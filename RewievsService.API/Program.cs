using MediatR;
using MongoDB.Driver;
using RewievsService.Application.Behaviors;
using RewievsService.Application.Services;
using RewievsService.Domain.Interfaces.Repositories;
using RewievsService.Domain.Interfaces.Services;
using RewievsService.Infrastructure.Configuration;
using RewievsService.Infrastructure.Data;
using RewievsService.Infrastructure.Repositories;
using FluentValidation;


var builder = WebApplication.CreateBuilder(args);

// ----------------- Configuration -----------------
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// ----------------- MongoDB -----------------
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton<MongoDbContext>();

// ----------------- Repositories -----------------
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IDiscussionRepository, DiscussionRepository>();

// ----------------- Services -----------------
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IDiscussionService, DiscussionService>();

// ----------------- MediatR -----------------
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

// ----------------- FluentValidation -----------------
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// ----------------- Controllers & Swagger -----------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ----------------- Pipeline -----------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
