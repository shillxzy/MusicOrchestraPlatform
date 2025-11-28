using MediatR;
using RewievsService.Domain.Entities;
using RewievsService.Domain.Interfaces.Repositories;

namespace RewievsService.Application.Queries.Comments
{
    public class GetCommentsByReviewIdQueryHandler :
        IRequestHandler<GetCommentsByReviewIdQuery, IReadOnlyList<Comment>>
    {
        private readonly ICommentRepository _repository;

        public GetCommentsByReviewIdQueryHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<Comment>> Handle(GetCommentsByReviewIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByReviewIdAsync(request.ReviewId, cancellationToken);
        }
    }
}
