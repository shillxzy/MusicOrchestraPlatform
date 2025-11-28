using MediatR;
using RewievsService.Domain.Entities;
using RewievsService.Domain.Interfaces.Repositories;

namespace RewievsService.Application.Commands.Comments
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Comment>
    {
        private readonly ICommentRepository _repository;

        public CreateCommentCommandHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Comment> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(request.Comment, cancellationToken);
            return request.Comment;
        }
    }
}
