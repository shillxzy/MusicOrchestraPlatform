using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MusicOrchestraOrder.API.Middleware;
using Npgsql;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OrderService.API.GrpcServices;
using OrderService.BLL.Mappings;
using OrderService.BLL.Services;
using OrderService.BLL.Services.Interfaces;
using OrderService.DAL.Infrastructure;
using OrderService.DAL.Repositories;
using OrderService.DAL.Repositories.Interfaces;
using OrderService.DAL.UOW;
using System.Data;
using BllOrderService = OrderService.BLL.Services.OrderService;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<CustomerMappingProfile>());

// Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? "Host=p-muddy-rice-a4125q9t-pooler.us-east-1.aws.neon.tech;Database=music_orchestra_orders;Username=neondb_owner;Password=npg_hsRQ3OtXoUG4";

builder.Services.AddScoped<IDbConnection>(provider =>
{
    var connection = new NpgsqlConnection(connectionString);
    connection.Open();
    return connection;
});

// Connection Factory
builder.Services.AddSingleton<IConnectFactory>(provider =>
{
    return new NpgsqlConnectFactory(connectionString);
});

// Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();

// Unit of Work
builder.Services.AddScoped<IUnitOfWork>(provider =>
{
    var connectionFactory = provider.GetRequiredService<IConnectFactory>();
    var customerRepo = provider.GetRequiredService<ICustomerRepository>();
    var productRepo = provider.GetRequiredService<IProductRepository>();
    var orderRepo = provider.GetRequiredService<IOrderRepository>();
    var orderItemRepo = provider.GetRequiredService<IOrderItemRepository>();

    return new UnitOfWork(connectionFactory, customerRepo, productRepo, orderRepo, orderItemRepo);
});

// Services


builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, BllOrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();

// Memory
builder.Services.AddGrpc();
builder.Services.AddMemoryCache(options =>
{
    options.SizeLimit = 1024;
});
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("redis");
});
builder.Services.AddScoped<OrderGrpcService>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("OrdersService"))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation(options =>
            {
                options.EnrichWithHttpWebRequest = (activity, request) =>
                {
                    activity.SetTag("grpc.method", request.RequestUri?.AbsolutePath);
                };
            })
            .AddConsoleExporter();
    });


// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.MapGrpcService<OrderGrpcService>();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
