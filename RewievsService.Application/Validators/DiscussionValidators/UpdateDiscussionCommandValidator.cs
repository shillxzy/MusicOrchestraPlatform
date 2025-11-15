using FluentValidation;
using RewievsService.Application.Commands.Discussions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Validators.DiscussionValidators
{
    public class UpdateDiscussionCommandValidator : AbstractValidator<UpdateDiscussionCommand>
    {
        public UpdateDiscussionCommandValidator()
        {
            RuleFor(x => x.DiscussionId)
                .NotEmpty().WithMessage("Discussion Id is required");

            RuleFor(x => x.NewTopic)
                .NotEmpty().WithMessage("Topic is required");

            RuleFor(x => x.UpdatedBy)
                .NotEmpty().WithMessage("UpdatedBy is required");
        }
    }

}
