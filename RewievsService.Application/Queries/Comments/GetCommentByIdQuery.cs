using RewievsService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Queries.Comments
{
    public class GetCommentByIdQuery : IQuery<Comment?>
    {
        public string CommentId { get; }

        public GetCommentByIdQuery(string commentId)
        {
            CommentId = commentId;
        }
    }
}
