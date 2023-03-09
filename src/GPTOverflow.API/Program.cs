using GPTOverflow.API.Modules.CrossCuttingConcerns.Configurations;
using GPTOverflow.API.Modules.CrossCuttingConcerns.Middlewares;
using GPTOverflow.API.Modules.CrossCuttingConcerns.Utils;
using GPTOverflow.API.Modules.StackExchange.BackgroundJobs;
using GPTOverflow.API.Modules.StackExchange.Configurations;
using GPTOverflow.API.Modules.UserManagement.BackgroundJobs;
using GPTOverflow.API.Modules.UserManagement.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.UserManagementModule(builder.Configuration);
builder.Services.AddCrossCuttingConcerns(builder.Configuration);
builder.Services.AddStackExchangeModule(builder.Configuration);

builder.Services.AddHostedService<UserManagementDataSeedBackgroundService>();
builder.Services.AddHostedService<StackExchangeDataSeedBackgroundService>();


// Configure dependencies on host
builder.Host.AddSerilog(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseFactoryMiddleware<ExceptionFormattingMiddleware>();
app.UseFactoryMiddleware<RequestResponseLoggingMiddleware>();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/system/health");

app.Run();

namespace GPTOverflow.API
{
    public class Program { }
}