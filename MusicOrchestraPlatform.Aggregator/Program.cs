using CatalogService.Grpc;    
using MusicOrchestraPlatform.Aggregator.Clients;
using MusicOrchestraPlatform.Aggregator.Clients.Interfaces;
using MusicOrchestraPlatform.ServiceDefaults;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OrderService.Grpc;     
using ReviewsService.Grpc;
using Serilog;
using OpenTelemetry.Metrics;
using System.Diagnostics.Metrics;


var builder = WebApplication.CreateBuilder(args);

var meter = new Meter("AggregatorService.Cache", "1.0");


var cacheHitCounter = meter.CreateCounter<long>("cache_hits");
var cacheMissCounter = meter.CreateCounter<long>("cache_misses");
var cacheEvictionCounter = meter.CreateCounter<long>("cache_evictions");

builder.AddServiceDefaults();


builder.AddRedisClient("redis");


builder.Services.AddHttpClient<IOrdersClient, OrdersClient>(client =>
{
    client.BaseAddress = new Uri("http://orders-service");
});

builder.Services.AddHttpClient<ICatalogClient, CatalogClient>(client =>
{
    client.BaseAddress = new Uri("http://catalog-service");
});

builder.Services.AddHttpClient<IReviewsClient, ReviewsClient>(client =>
{
    client.BaseAddress = new Uri("http://reviews-service");
});

// Grpc Client

builder.Services.AddGrpcClient<OrderGrpc.OrderGrpcClient>(o =>
{
    o.Address = new Uri("https://orders-service:5001");
});

builder.Services.AddGrpcClient<CatalogGrpc.CatalogGrpcClient>(o =>
{
    o.Address = new Uri("https://catalog-service:5001");
});

builder.Services.AddGrpcClient<ReviewsGrpc.ReviewsGrpcClient>(o =>
{
    o.Address = new Uri("https://reviews-service:5001");
});


builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("AggregatorService"))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation() 
            .AddConsoleExporter();


    });

builder.Services.AddOpenTelemetry()
    .WithMetrics(meterProviderBuilder =>
    {
        meterProviderBuilder
            .AddMeter("AggregatorService.Cache")
            .AddConsoleExporter();
    });

// Structured logging для gRPC
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();



builder.Host.UseSerilog();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseServiceDefaults();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aggregator API v1");
    });
}

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
