using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Commands.Comments
{
    public class DeleteCommentCommand : ICommand
    {
        public string CommentId { get; }
        public string RequestedBy { get; }

        public DeleteCommentCommand(string commentId, string requestedBy)
        {
            CommentId = commentId;
            RequestedBy = requestedBy;
        }
    }
}
