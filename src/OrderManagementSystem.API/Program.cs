using OrderManagementSystem.API.Configurations;
using OrderManagementSystem.API.Extensions;
using OrderManagementSystem.API.Middlewares;
using OrderManagementSystem.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Check if running in Docker
var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

if (isDocker)
{
    builder.WebHost.UseUrls("http://*:5000");
}
else
{
    builder.WebHost.UseUrls("http://*:5000", "https://*:5001");
}

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationConfiguration();
builder.Services.AddMediatRConfiguration();
builder.Services.AddMassTransitConfiguration();

builder.Services.AddDbContextConfiguration(builder.Configuration);
builder.Services.AddRepositoryConfiguration();
builder.Services.AddSettingsConfiguration(builder.Configuration);
builder.Services.AddServiceConfiguration(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.MapControllers();
app.Run();
