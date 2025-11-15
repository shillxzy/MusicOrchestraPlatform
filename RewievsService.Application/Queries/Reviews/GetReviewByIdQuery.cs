using RewievsService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Queries.Reviews
{
    public class GetReviewByIdQuery : IQuery<Review?>
    {
        public string ReviewId { get; }

        public GetReviewByIdQuery(string reviewId)
        {
            ReviewId = reviewId;
        }
    }
}
