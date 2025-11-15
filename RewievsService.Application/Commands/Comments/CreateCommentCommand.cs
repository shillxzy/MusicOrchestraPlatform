using RewievsService.Domain.Entities;
using MediatR;


namespace RewievsService.Application.Commands.Comments
{
    public class CreateCommentCommand : ICommand<Comment>
    {
        public Comment Comment { get; }

        public CreateCommentCommand(Comment comment)
        {
            Comment = comment;
        }
    }
}
