using System.Reflection;
using System.Text.Json.Serialization;
using Hellang.Middleware.ProblemDetails;
using OpenTelemetry.Trace;
using Seguro.Api.Warmup;

var builder = WebApplication.CreateBuilder(args);
var assemblyName = Assembly.GetExecutingAssembly().GetName();
var serviceName = assemblyName.Name;
var serviceVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString();

builder.Services.AddEndpointsApiExplorer()
                .AddVersioning()
                .AddSwaggerDoc()
                .AddSwaggerGen()
                .AddRouting(opt => opt.LowercaseUrls = true)
                .AddTelemetry(serviceName!, serviceVersion!, builder.Configuration)
                .AddLogs(builder.Configuration, serviceName!)

                .AddDependencies(builder.Configuration)
                .AddMediaTR()
                // .AddWorkflow()

                .AddApiProblemDetails()
                .AddControllers(options => options.ModelValidatorProviders.Clear())
                .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

var app = builder.Build();
app.UseProblemDetails();
app.UseCustomSwagger();
app.UseHttpsRedirection();
app.UseCors(builder => builder
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
app.MapControllers();
app.Run();