using MediatR;
using RewievsService.Domain.Entities;
using RewievsService.Domain.Interfaces.Repositories;

namespace RewievsService.Application.Queries.Comments
{
    public class GetCommentsByAuthorIdQueryHandler :
        IRequestHandler<GetCommentsByAuthorIdQuery, IReadOnlyList<Comment>>
    {
        private readonly ICommentRepository _repository;

        public GetCommentsByAuthorIdQueryHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<Comment>> Handle(GetCommentsByAuthorIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByAuthorIdAsync(request.AuthorId, cancellationToken);
        }
    }
}
