using MediatR;
using RewievsService.Domain.Interfaces.Repositories;

namespace RewievsService.Application.Commands.Comments
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand>
    {
        private readonly ICommentRepository _repository;

        public UpdateCommentCommandHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.CommentId, cancellationToken);
            if (existing == null)
                return;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
    }
}
