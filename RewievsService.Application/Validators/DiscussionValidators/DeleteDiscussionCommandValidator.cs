using FluentValidation;
using RewievsService.Application.Commands.Discussions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Validators.DiscussionValidators
{
    public class DeleteDiscussionCommandValidator : AbstractValidator<DeleteDiscussionCommand>
    {
        public DeleteDiscussionCommandValidator()
        {
            RuleFor(x => x.DiscussionId)
                .NotEmpty().WithMessage("Discussion Id is required");
        }
    }
}
