using RewievsService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Queries.Reviews
{
    public class GetReviewWithCommentsQuery : IQuery<Review?>
    {
        public string ReviewId { get; }

        public GetReviewWithCommentsQuery(string reviewId)
        {
            ReviewId = reviewId;
        }
    }
}
