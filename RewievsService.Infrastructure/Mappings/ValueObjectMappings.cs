using MongoDB.Bson.Serialization;
using RewievsService.Domain.Entities;
using RewievsService.Domain.ValueObjects;


namespace RewievsService.Infrastructure.Mappings
{
    public static class ValueObjectMappings
    {
        public static void Register()
        {
            BsonClassMap.RegisterClassMap<RatingValue>(cm =>
            {
                cm.AutoMap();
                cm.MapCreator(v => new RatingValue(v.Value));
            });

            BsonClassMap.RegisterClassMap<CommentText>(cm =>
            {
                cm.AutoMap();
                cm.MapCreator(v => new CommentText(v.Value));
            });

            BsonClassMap.RegisterClassMap<Review>(cm => cm.AutoMap());
            BsonClassMap.RegisterClassMap<Comment>(cm => cm.AutoMap());
            BsonClassMap.RegisterClassMap<Discussion>(cm => cm.AutoMap());
        }
    }
}
