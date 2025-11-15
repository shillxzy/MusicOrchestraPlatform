using RewievsService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Commands.Reviews
{
    public class UpdateReviewCommand : ICommand
    {
        public string ReviewId { get; set; }
        public string? NewTitle { get; }
        public string? NewContent { get; }
        public RatingValue? NewRating { get; }
        public string UpdatedBy { get; }

        public UpdateReviewCommand(string reviewId, string updatedBy, string? newTitle = null, string? newContent = null, RatingValue? newRating = null)
        {
            ReviewId = reviewId;
            UpdatedBy = updatedBy;
            NewTitle = newTitle;
            NewContent = newContent;
            NewRating = newRating;
        }
    }
}
