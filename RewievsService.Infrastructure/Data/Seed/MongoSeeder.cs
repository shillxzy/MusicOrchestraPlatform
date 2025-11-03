using MongoDB.Driver;
using RewievsService.Domain.Entities;
using RewievsService.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace RewievsService.Infrastructure.Seed
{
    public static class MongoSeeder
    {
        public static void SeedReviews(IMongoCollection<Review> collection)
        {
            if (collection.CountDocuments(_ => true) > 0)
                return;

            var reviews = new List<Review>
            {
                new Review(
                    title: "Неймовірно атмосферно",
                    content: "Дуже потужна композиція, викликає мурахи.",
                    rating: new RatingValue(5),
                    targetId: "instrument-001"
                ),
                new Review(
                    title: "Добре, але є нюанси",
                    content: "Гарна робота, проте не вистачає емоційності.",
                    rating: new RatingValue(3),
                    targetId: "instrument-002"
                ),
            };

            collection.InsertMany(reviews);
        }

        public static void SeedComments(IMongoCollection<Comment> collection)
        {
            if (collection.CountDocuments(_ => true) > 0)
                return;

            var comments = new List<Comment>
            {
                new Comment("user-001", new CommentText("Погоджуюсь, чудова робота!")),
                new Comment("user-002", new CommentText("Мені сподобалось не настільки сильно, але все одно гідно.")),
            };

            collection.InsertMany(comments);
        }

        public static void SeedDiscussions(IMongoCollection<Discussion> collection)
        {
            if (collection.CountDocuments(_ => true) > 0)
                return;

            var discussions = new List<Discussion>
            {
                new Discussion("Обговорення класичних творів", "category-001"),
                new Discussion("Модерн у симфонічній музиці", "category-002"),
            };

            collection.InsertMany(discussions);
        }
    }
}
