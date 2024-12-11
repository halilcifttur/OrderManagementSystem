using OrderManagementSystem.Worker;
using OrderManagementSystem.Worker.Configurations;
using OrderManagementSystem.Infrastructure.Configurations;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContextConfiguration(builder.Configuration);
builder.Services.AddRepositoryConfiguration();

builder.Services.AddHostedService<Worker>();
builder.Services.AddHangfireConfiguration(builder.Configuration);

var host = builder.Build();

host.Run();