using RewievsService.Domain.Entities;
using RewievsService.Domain.ValueObjects;
using MediatR;

namespace RewievsService.Application.Commands.Reviews
{
    public class CreateReviewCommand : ICommand<Review>
    {
        public Review Review { get; }

        public CreateReviewCommand(Review review)
        {
            Review = review;
        }
    }
}
