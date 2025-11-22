using MusicOrchestraPlatform.ApiGateway.Middleware;
using Serilog;
using Serilog.Formatting.Compact;

var builder = WebApplication.CreateBuilder(args);

// Serilog конфіг
builder.Host.UseSerilog((context, services, loggerConfig) =>
{
    loggerConfig
        .MinimumLevel.Information()
        .Enrich.FromLogContext()
        .Enrich.WithProperty("ServiceName", "ApiGateway")
        .Enrich.WithEnvironmentName()
        .Enrich.WithMachineName()
        .WriteTo.Console(new CompactJsonFormatter());
});

// YARP reverse proxy
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// CorrelationId middleware
app.UseCorrelationId();

// Reverse proxy mapping
app.MapReverseProxy();

app.Run();
