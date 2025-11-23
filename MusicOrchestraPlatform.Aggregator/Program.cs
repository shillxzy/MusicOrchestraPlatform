using MusicOrchestraPlatform.Aggregator.Clients;
using MusicOrchestraPlatform.Aggregator.Clients.Interfaces;
using MusicOrchestraPlatform.ServiceDefaults;

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
