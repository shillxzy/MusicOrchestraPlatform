using RewievsService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Queries.Comments
{
    public class GetCommentsByAuthorIdQuery : IQuery<IReadOnlyList<Comment>>
    {
        public string AuthorId { get; }

        public GetCommentsByAuthorIdQuery(string authorId)
        {
            AuthorId = authorId;
        }
    }
}
