using RewievsService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Commands.Comments
{
    public class UpdateCommentCommand : ICommand
    {
        public string CommentId { get; }
        public CommentText? NewText { get; }
        public string UpdatedBy { get; }

        public UpdateCommentCommand(string commentId, string updatedBy, CommentText? newText = null)
        {
            CommentId = commentId;
            UpdatedBy = updatedBy;
            NewText = newText;
        }
    }
}
