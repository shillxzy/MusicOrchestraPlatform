using MediatR;
using RewievsService.Domain.Entities;
using RewievsService.Domain.Interfaces.Repositories;

namespace RewievsService.Application.Queries.Comments
{
    public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, Comment?>
    {
        private readonly ICommentRepository _repository;

        public GetCommentByIdQueryHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Comment?> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.CommentId, cancellationToken);
        }
    }
}
