using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrchestraPlatform.ServiceDefaults
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

    // Розширення для pipeline
    public static class CorrelationIdMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorrelationIdMiddleware>();
        }
    }
}
