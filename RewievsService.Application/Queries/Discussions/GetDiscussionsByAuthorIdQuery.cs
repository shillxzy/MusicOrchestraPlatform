using RewievsService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Queries.Discussions
{
    public class GetDiscussionsByAuthorIdQuery : IQuery<IReadOnlyList<Discussion>>
    {
        public string AuthorId { get; }

        public GetDiscussionsByAuthorIdQuery(string authorId)
        {
            AuthorId = authorId;
        }
    }
}
