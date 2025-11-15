using RewievsService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Queries.Comments
{
    public class GetCommentsByReviewIdQuery : IQuery<IReadOnlyList<Comment>>
    {
        public string ReviewId { get; }

        public GetCommentsByReviewIdQuery(string reviewId)
        {
            ReviewId = reviewId;
        }
    }
}
