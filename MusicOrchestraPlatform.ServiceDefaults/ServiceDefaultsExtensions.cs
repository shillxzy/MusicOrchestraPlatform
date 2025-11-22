using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MusicOrchestraPlatform.ServiceDefaults;

namespace MusicOrchestraPlatform.ServiceDefaults
{
    public static class ServiceDefaultsExtensions
    {
        public static WebApplicationBuilder AddServiceDefaults(this WebApplicationBuilder builder, string serviceName = "AggregatorService")
        {
            // Logging через Serilog
            builder.Host.AddServiceDefaultsLogging(serviceName);

            // OpenTelemetry
            builder.Services.AddServiceDefaultsTelemetry(serviceName);

            return builder;
        }

        public static WebApplication UseServiceDefaults(this WebApplication app)
        {
            // Middleware CorrelationId
            app.UseCorrelationId();

            // Тут можна додати health checks або інші middleware, якщо потрібно
            return app;
        }
    }
}
