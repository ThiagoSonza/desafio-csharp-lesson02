using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Seguro.Api.Domain.Proposta.Features.AprovarProposta;
using Seguro.Api.Domain.Proposta.Features.CadastrarProposta;
using Seguro.Api.Domain.Proposta.Features.RejeitarProposta;
using Seguro.Api.Domain.Proposta.Infraestrutura;
using Seguro.Api.Infrastructure;
using Swashbuckle.AspNetCore.SwaggerUI;
using Hellang.Middleware.ProblemDetails;
using Hellang.Middleware.ProblemDetails.Mvc;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using Serilog;
using Serilog.Enrichers.OpenTelemetry;
using Serilog.Sinks.OpenTelemetry;

namespace Seguro.Api.Warmup
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.CustomSchemaIds(x => x.ToString());

                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(description.GroupName, new OpenApiInfo()
                    {
                        Title = $"Proposta Consignado Api - v{description.ApiVersion}",
                        Version = description.ApiVersion.ToString()
                    });
                }

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                            },
                            Array.Empty<string>()
                        }
                    }
                );
            });

            return services;
        }

        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.ReportApiVersions = true; // Inclui as versões suportadas no cabeçalho da resposta
                config.AssumeDefaultVersionWhenUnspecified = true; // Define uma versão padrão se nenhuma for especificada
                config.DefaultApiVersion = new ApiVersion(1); // Versão padrão
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V"; // Formato: "v1", "v2", etc.
                options.SubstituteApiVersionInUrl = true;
            });
            return services;
        }

        public static IServiceCollection AddDependencies(this IServiceCollection services, ConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SeguroDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<PropostaRepository>();

            services.AddScoped<CadastrarPropostaHandler>();
            services.AddScoped<AprovarPropostaHandler>();
            services.AddScoped<RejeitarPropostaHandler>();

            return services;
        }

        public static IServiceCollection AddMediaTR(this IServiceCollection services)
        {
            string applicationAssemblyName = Assembly.GetExecutingAssembly().GetName().Name!;
            var assembly = AppDomain.CurrentDomain.Load(applicationAssemblyName);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

            return services;
        }

        public static WebApplication UseCustomSwagger(this WebApplication application)
        {
            application
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    var apiVersionProvider = application.Services.GetService<IApiVersionDescriptionProvider>();
                    if (apiVersionProvider == null)
                        throw new ArgumentException("API Versioning not registered.");

                    foreach (var description in apiVersionProvider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            $"API {description.GroupName}"
                        );
                    }

                    options.DocExpansion(DocExpansion.List);
                });

            return application;
        }

        public static IServiceCollection AddApiProblemDetails(this IServiceCollection services)
        {
            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (ctx, ex) =>
                {
                    var env = ctx.RequestServices.GetRequiredService<IHostEnvironment>();
                    return env.IsDevelopment() || env.IsStaging();
                };

                options.MapExceptionToStatusCodeWithMessage<UnauthorizedAccessException>(StatusCodes.Status401Unauthorized);
                options.MapExceptionToStatusCodeWithMessage<ArgumentException>(StatusCodes.Status400BadRequest);
                options.MapExceptionToStatusCodeWithMessage<ArgumentNullException>(StatusCodes.Status400BadRequest);
                options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
                options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);
                options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
            })
            .AddProblemDetailsConventions();

            return services;
        }

        public static IServiceCollection AddTelemetry(this IServiceCollection serviceCollection, string serviceName, string serviceVersion, IConfiguration configuration)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            TelemetrySettings settings;
            if (configuration.GetSection("OpenTelemetry") is var section && section.Exists())
                settings = new TelemetrySettings(serviceName, serviceVersion, new TelemetryExporter(section["Type"] ?? string.Empty, section["Endpoint"] ?? string.Empty));
            else
                settings = new TelemetrySettings(serviceName, serviceVersion, new TelemetryExporter("console", ""));

            PropostasOtelMetrics metrics = new();

            serviceCollection.AddSingleton(settings);
            serviceCollection.AddScoped(sp => new OtelTracingService(sp.GetService<TelemetrySettings>()));
            serviceCollection.AddScoped<TelemetryFactory>();
            serviceCollection.AddSingleton(metrics);
            serviceCollection.AddSingleton<OtelVariables>();

            Action<ResourceBuilder> configureResource = r => r.AddService(
                serviceName: "Seguro.Api",
                serviceVersion: "v1",
                serviceInstanceId: Environment.MachineName);

            serviceCollection.AddOpenTelemetry()
                .ConfigureResource(configureResource)
                .WithTracing(builder => builder
                    .AddSource("Seguro.Api")
                    .AddHttpClientInstrumentation()
                    .AddSqlClientInstrumentation(o =>
                    {
                        o.RecordException = true;
                        o.SetDbStatementForText = true;
                    })
                    .AddEntityFrameworkCoreInstrumentation(o => { })
                    .AddAspNetCoreInstrumentation(opts =>
                    {
                        opts.EnrichWithHttpRequest = (a, r) => a?.AddTag("env", environmentName);
                        opts.RecordException = true;
                    })
                    .AddOtlpExporter(config =>
                    {
                        config.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                        config.Endpoint = new Uri("http://localhost:4317");
                        config.Headers = "x-honeycomb-team=Je9GeCFwekC0l2mdfhHe6uA";
                    })
                );

            return serviceCollection;
        }

        public static IServiceCollection AddLogs(this IServiceCollection services, IConfiguration configuration, string serviceName)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithThreadId()
                .Enrich.WithOpenTelemetrySpanId()
                .Enrich.WithOpenTelemetryTraceId()
                .Enrich.WithProperty("service_name", serviceName)
                .WriteTo.OpenTelemetry(options =>
                {
                    options.Endpoint = "http://localhost:4317";
                    options.IncludedData = IncludedData.MessageTemplateTextAttribute
                        | IncludedData.TraceIdField | IncludedData.SpanIdField;
                    options.Protocol = OtlpProtocol.Grpc;
                })
                .WriteTo.Console()
                .CreateLogger();

            services.AddSingleton(Log.Logger);
            return services;
        }

        public static void MapExceptionToStatusCodeWithMessage<TException>(
            this Hellang.Middleware.ProblemDetails.ProblemDetailsOptions options, int statusCode)
            where TException : Exception
        {
            options.Map<TException>(ex => new StatusCodeProblemDetails(statusCode)
            {
                Detail = ex.Message
            });
        }
    }
}