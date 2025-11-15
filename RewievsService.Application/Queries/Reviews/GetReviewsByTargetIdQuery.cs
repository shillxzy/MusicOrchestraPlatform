using RewievsService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Queries.Reviews
{
    public class GetReviewsByTargetIdQuery : IQuery<IReadOnlyList<Review>>
    {
        public string TargetId { get; }

        public GetReviewsByTargetIdQuery(string targetId)
        {
            TargetId = targetId;
        }
    }
}
