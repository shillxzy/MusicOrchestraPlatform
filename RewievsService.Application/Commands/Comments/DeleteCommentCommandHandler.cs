using MediatR;
using RewievsService.Domain.Interfaces.Repositories;

namespace RewievsService.Application.Commands.Comments
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
    {
        private readonly ICommentRepository _repository;

        public DeleteCommentCommandHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.CommentId, cancellationToken);
            if (existing == null)
                return;

            await _repository.DeleteAsync(request.CommentId, cancellationToken);
        }
    }
}
