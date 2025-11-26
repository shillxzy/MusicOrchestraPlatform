using CatalogService.API.Middlewares;
using CatalogService.BLL.Services;
using CatalogService.BLL.Services.Interfaces;
using CatalogService.BLL.MappingProfiles;
using CatalogService.BLL.Validators;
using CatalogService.DAL.Data;
using CatalogService.DAL.Repositories;
using CatalogService.DAL.Repositories.Interfaces;
using CatalogService.DAL.UOW;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FluentValidation;
using Npgsql.EntityFrameworkCore.PostgreSQL;


var builder = WebApplication.CreateBuilder(args);

// ----------------- DbContext -----------------
builder.Services.AddDbContext<CatalogDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ----------------- Repositories & UoW -----------------
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IInstrumentRepository, InstrumentRepository>();
builder.Services.AddScoped<IPerformerRepository, PerformerRepository>();
builder.Services.AddScoped<ICompositionRepository, CompositionRepository>();
builder.Services.AddScoped<IConcertProgramRepository, ConcertProgramRepository>();
builder.Services.AddScoped<IInstrumentImageRepository, InstrumentImageRepository>();

// ----------------- Services -----------------
builder.Services.AddScoped<IInstrumentService, InstrumentService>();
builder.Services.AddScoped<IPerformerService, PerformerService>();
builder.Services.AddScoped<ICompositionService, CompositionService>();
builder.Services.AddScoped<IConcertProgramService, ConcertProgramService>();
builder.Services.AddScoped<IInstrumentImageService, InstrumentImageService>();

// ----------------- AutoMapper -----------------
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<CatalogMappingProfile>();
});

// ----------------- FluentValidation -----------------
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CatalogMappingProfile>();

// ----------------- Controllers -----------------
builder.Services.AddControllers();

// ----------------- Swagger -----------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache(options =>
{
    options.SizeLimit = 1024; 
});


var app = builder.Build();

// ----------------- Middleware -----------------
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// ----------------- Swagger -----------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    db.Database.Migrate(); 
}

// ----------------- Run -----------------
app.Run();
