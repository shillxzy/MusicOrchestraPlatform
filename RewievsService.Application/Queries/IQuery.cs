using MediatR;


namespace RewievsService.Application.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
