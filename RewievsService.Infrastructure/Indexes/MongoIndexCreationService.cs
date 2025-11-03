using MongoDB.Driver;
using RewievsService.Domain.Entities;
using RewievsService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Infrastructure.Indexes
{
    public class MongoIndexCreationService : IIndexCreationService
    {
        private readonly MongoDbContext _context;

        public MongoIndexCreationService(MongoDbContext context)
        {
            _context = context;
        }

        public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
        {
            await CreateReviewIndexesAsync(cancellationToken);
            await CreateCommentIndexesAsync(cancellationToken);
            await CreateDiscussionIndexesAsync(cancellationToken);
        }

        private async Task CreateReviewIndexesAsync(CancellationToken cancellationToken)
        {
            var reviewIndexes = new[]
            {
                new CreateIndexModel<Review>(
                    Builders<Review>.IndexKeys.Ascending(r => r.TargetId)
                ),

                new CreateIndexModel<Review>(
                    Builders<Review>.IndexKeys
                        .Ascending(r => r.TargetId)
                        .Ascending(r => r.Rating.Value)
                ),

                new CreateIndexModel<Review>(
                    Builders<Review>.IndexKeys
                        .Text(r => r.Title)
                        .Text(r => r.Content)
                )
            };

            await _context.Reviews.Indexes.CreateManyAsync(reviewIndexes, cancellationToken);
        }

        private async Task CreateCommentIndexesAsync(CancellationToken cancellationToken)
        {
            var commentIndexes = new[]
            {

                new CreateIndexModel<Comment>(
                    Builders<Comment>.IndexKeys.Text(c => c.Text.Value)
                ),

                new CreateIndexModel<Comment>(
                    Builders<Comment>.IndexKeys.Ascending(c => c.AuthorId)
                )
            };

            await _context.Comments.Indexes.CreateManyAsync(commentIndexes, cancellationToken);
        }

        private async Task CreateDiscussionIndexesAsync(CancellationToken cancellationToken)
        {
            var discussionIndexes = new[]
            {
                new CreateIndexModel<Discussion>(
                    Builders<Discussion>.IndexKeys.Ascending(d => d.RelatedEntityId)
                ),

                new CreateIndexModel<Discussion>(
                    Builders<Discussion>.IndexKeys.Text(d => d.Topic)
                ),

                new CreateIndexModel<Discussion>(
                    Builders<Discussion>.IndexKeys.Ascending(d => d.CreatedAt),
                    new CreateIndexOptions { ExpireAfter = TimeSpan.FromDays(90) }
                )
            };

            await _context.Discussions.Indexes.CreateManyAsync(discussionIndexes, cancellationToken);
        }
    }
}
