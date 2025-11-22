using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;


namespace MusicOrchestraPlatform.ServiceDefaults
{
    public static class OpenTelemetryExtensions
    {
        public static IServiceCollection AddServiceDefaultsTelemetry(
            this IServiceCollection services, string serviceName)
        {
            services.AddOpenTelemetry() 
                .WithTracing(tracing =>
                {
                    tracing
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddOtlpExporter(opt =>
                        {
                            opt.Endpoint = new Uri("http://localhost:4317");
                        })
                        .SetSampler(new AlwaysOnSampler());
                });

            return services;
        }
    }
}
