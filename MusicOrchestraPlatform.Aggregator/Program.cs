using MusicOrchestraPlatform.Aggregator.Clients;
using MusicOrchestraPlatform.Aggregator.Clients.Interfaces;
using MusicOrchestraPlatform.ServiceDefaults;
using OrderService.Grpc;     
using CatalogService.Grpc;    
using ReviewsService.Grpc;   


var builder = WebApplication.CreateBuilder(args);



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
