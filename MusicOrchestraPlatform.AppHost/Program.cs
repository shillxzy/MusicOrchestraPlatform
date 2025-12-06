var builder = DistributedApplication.CreateBuilder(args);

// --- Бази даних ---
var ordersDb = builder.AddPostgres("postgres-orders")
    .WithDataVolume("orders-db-data")
    .WithPgAdmin();

var catalogDb = builder.AddPostgres("postgres-catalog")
    .WithDataVolume("catalog-db-data")
    .WithPgAdmin();

var reviewsDb = builder.AddMongoDB("mongodb-reviews")
    .WithDataVolume("reviews-db-data")
    .WithMongoExpress();

var redis = builder.AddRedis("redis")
    .WithDataVolume("redis-data")
    .WithRedisCommander();

// --- створюємо бази ---
var ordersDatabase = ordersDb.AddDatabase("music-orchestra-orders");
var catalogDatabase = catalogDb.AddDatabase("music-orchestra-catalog");
var reviewsDatabase = reviewsDb.AddDatabase("music-orchestra-reviews");

// --- сервіси ---
var ordersService = builder.AddProject("orders-service",
    @"..\..\..\..\source\repos\MusicOrchestraPlatform\MusicOrchestraOrder.API\OrderService.API.csproj")
    .WithReference(ordersDb)
    .WithReference(redis)
    .WithEnvironment("ConnectionStrings__OrdersDb", ordersDb.Resource.ConnectionStringExpression);

var catalogService = builder.AddProject("catalog-service",
    @"..\..\..\..\source\repos\MusicOrchestraPlatform\CatalogService.API\CatalogService.API.csproj")
    .WithReference(catalogDb)
    .WithReference(redis)
    .WithEnvironment("ConnectionStrings__CatalogDb", catalogDb.Resource.ConnectionStringExpression);

var reviewsService = builder.AddProject("reviews-service",
    @"..\..\..\..\source\repos\MusicOrchestraPlatform\RewievsService.API\RewievsService.API.csproj")
    .WithReference(reviewsDb)
    .WithReference(redis)
    .WithEnvironment("MongoDbSettings__ConnectionString", "mongodb://localhost:27017")
    .WithEnvironment("MongoDbSettings__DatabaseName", "music_orchestra_reviews");


// --- Aggregator та Gateway ---
var aggregator = builder.AddProject("aggregator",
    @"..\..\..\..\source\repos\MusicOrchestraPlatform\MusicOrchestraPlatform.Aggregator\MusicOrchestraPlatform.Aggregator.csproj")
    .WithReference(ordersService)
    .WithReference(catalogService)
    .WithReference(reviewsService)
    .WithReference(redis);

var gateway = builder.AddProject("gateway",
    @"..\..\..\..\source\repos\MusicOrchestraPlatform\MusicOrchestraPlatform.ApiGateway\MusicOrchestraPlatform.ApiGateway.csproj")
    .WithExternalHttpEndpoints()
    .WithReference(ordersService)
    .WithReference(catalogService)
    .WithReference(reviewsService)
    .WithReference(aggregator)
    .WithReference(redis);

// --- Старт платформи ---
builder.Build().Run();
