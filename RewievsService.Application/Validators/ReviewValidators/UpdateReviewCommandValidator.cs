using FluentValidation;
using RewievsService.Application.Commands.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Validators.ReviewValidators
{
    public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
    {
        public UpdateReviewCommandValidator()
        {
            RuleFor(x => x.ReviewId)
                .NotEmpty().WithMessage("Review Id is required");

            RuleFor(x => x.NewTitle)
                .NotEmpty().When(x => x.NewTitle != null)
                .MaximumLength(200).When(x => x.NewTitle != null);

            RuleFor(x => x.NewContent)
                .NotEmpty().When(x => x.NewContent != null);

            RuleFor(x => x.NewRating)
                .NotNull().When(x => x.NewRating != null);
        }
    }
}
