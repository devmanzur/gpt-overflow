using Serilog;
using Serilog.Events;

namespace GPTOverflow.API.Modules.CrossCuttingConcerns.Configurations;

public static class ConfigureSerilog
{
    public static IHostBuilder AddSerilog(this IHostBuilder builder, IConfiguration configuration)
    {
        var logDirectory = configuration.GetSection("Serilog").GetValue<string>("FileSink:LogDirectory");
        return builder.UseSerilog((context, loggerConfiguration) =>
        {
            loggerConfiguration
                .MinimumLevel.Warning()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithProcessName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithThreadId()
                .WriteTo.Console()
                .WriteTo.File($"{logDirectory}/error-log.txt",
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Error, retainedFileCountLimit: 30)
                .ReadFrom.Configuration(context.Configuration);
        });
    }
}