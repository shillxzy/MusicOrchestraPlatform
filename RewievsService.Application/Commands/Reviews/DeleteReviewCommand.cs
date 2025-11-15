using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Commands.Reviews
{
    public class DeleteReviewCommand : ICommand
    {
        public string ReviewId { get; }
        public string RequestedBy { get; }

        public DeleteReviewCommand(string reviewId, string requestedBy)
        {
            ReviewId = reviewId;
            RequestedBy = requestedBy ?? throw new ArgumentNullException(nameof(requestedBy));
        }
    }
}
