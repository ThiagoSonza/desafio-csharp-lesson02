using System.Text.Json.Serialization;
using Seguro.Api.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDoc();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(options => options.ModelValidatorProviders.Clear())
                .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
builder.Services.AddDependencies(builder.Configuration);
builder.Services.AddMediaTR();
builder.Services.AddWorkflow();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();