using RewievsService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Queries.Reviews
{
    public class GetReviewsByAuthorIdQuery : IQuery<IReadOnlyList<Review>>
    {
        public string AuthorId { get; }

        public GetReviewsByAuthorIdQuery(string authorId)
        {
            AuthorId = authorId;
        }
    }
}
