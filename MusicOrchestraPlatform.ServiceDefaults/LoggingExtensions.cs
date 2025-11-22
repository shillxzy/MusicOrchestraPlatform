using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;


namespace MusicOrchestraPlatform.ServiceDefaults
{
    public static class LoggingExtensions
    {
        public static IHostBuilder AddServiceDefaultsLogging(this IHostBuilder hostBuilder, string serviceName)
        {
            hostBuilder.UseSerilog((context, services, loggerConfig) =>
            {
                loggerConfig
                    .MinimumLevel.Information()  // Production
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .Enrich.WithProperty("ServiceName", serviceName)
                    .Enrich.FromLogContext()
                    .Enrich.WithEnvironmentName()
                    .Enrich.WithMachineName()
                    .WriteTo.Console(new CompactJsonFormatter())
                    .WriteTo.File(new CompactJsonFormatter(), "logs/log.json", rollingInterval: RollingInterval.Day);
            });

            return hostBuilder;
        }
    }
}
