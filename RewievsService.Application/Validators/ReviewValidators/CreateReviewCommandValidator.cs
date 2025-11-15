using FluentValidation;
using RewievsService.Application.Commands.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Validators.ReviewValidators
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(x => x.Review.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(200);

            RuleFor(x => x.Review.Content)
                .NotEmpty().WithMessage("Content is required");

            RuleFor(x => x.Review.Rating)
                .NotNull().WithMessage("Rating is required");

            RuleFor(x => x.Review.TargetId)
                .NotEmpty().WithMessage("TargetId is required");
        }
    }
}
