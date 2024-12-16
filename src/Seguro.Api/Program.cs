using System.Text.Json.Serialization;
using Hellang.Middleware.ProblemDetails;
using Seguro.Api.Warmup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer()
                .AddVersioning()
                .AddSwaggerDoc()
                .AddSwaggerGen()
                .AddRouting(opt => opt.LowercaseUrls = true)

                .AddDependencies(builder.Configuration)
                .AddMediaTR()
                .AddWorkflow()

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