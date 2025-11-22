using Serilog.Context;
using System.Diagnostics;


namespace MusicOrchestraPlatform.ApiGateway.Middleware
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        public const string Header = "X-Correlation-Id";

        public CorrelationIdMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            string correlationId = context.Request.Headers[Header].FirstOrDefault() ?? Guid.NewGuid().ToString();
            context.Response.Headers[Header] = correlationId;

            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                Activity.Current?.AddTag("CorrelationId", correlationId);
                await _next(context);
            }
        }
    }

    public static class CorrelationIdMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder builder)
            => builder.UseMiddleware<CorrelationIdMiddleware>();
    }
}
