using FluentValidation;
using RewievsService.Application.Commands.Discussions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Validators.DiscussionValidators
{
    public class CreateDiscussionCommandValidator : AbstractValidator<CreateDiscussionCommand>
    {
        public CreateDiscussionCommandValidator()
        {
            RuleFor(x => x.Discussion.Topic)
                .NotEmpty().WithMessage("Topic is required");

            RuleFor(x => x.Discussion.RelatedEntityId)
                .NotEmpty().WithMessage("RelatedEntityId is required");
        }
    }
}
