using MongoDB.Driver;
using ReviewService.Infrastructure.Seed;
using RewievsService.Domain.Entities;

namespace RewievsService.Infrastructure.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoClient _client;

        public MongoDbContext(string connectionString, string databaseName)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);

            ConfigureCollections();
            SeedDatabase();
        }

        // === Колекції ===
        public IMongoCollection<Review> Reviews => _database.GetCollection<Review>("Reviews");
        public IMongoCollection<Comment> Comments => _database.GetCollection<Comment>("Comments");
        public IMongoCollection<Discussion> Discussions => _database.GetCollection<Discussion>("Discussions");

        // === Налаштування колекцій ===
        private void ConfigureCollections()
        {
            // Text index для пошуку по тексту коментаря
            var commentIndexKeys = Builders<Comment>.IndexKeys.Text(c => c.Text.Value);
            Comments.Indexes.CreateOne(new CreateIndexModel<Comment>(commentIndexKeys));

            // Індекс для пошуку оглядів по TargetId та Rating
            var reviewIndexKeys = Builders<Review>.IndexKeys
                .Ascending(r => r.TargetId)
                .Ascending(r => r.Rating.Value);
            Reviews.Indexes.CreateOne(new CreateIndexModel<Review>(reviewIndexKeys));

            // TTL індекс для старих обговорень (3 місяці)
            var ttlIndexKeys = Builders<Discussion>.IndexKeys.Ascending(d => d.CreatedAt);
            var ttlIndexOptions = new CreateIndexOptions { ExpireAfter = TimeSpan.FromDays(90) };
            Discussions.Indexes.CreateOne(new CreateIndexModel<Discussion>(ttlIndexKeys, ttlIndexOptions));
        }

        // === Сідування бази ===
        private void SeedDatabase()
        {
            MongoSeeder.SeedReviews(Reviews);
            MongoSeeder.SeedComments(Comments);
            MongoSeeder.SeedDiscussions(Discussions);
        }

        // === Unit of Work через сесію ===
        public IClientSessionHandle StartSession() => _client.StartSession();
    }
}
