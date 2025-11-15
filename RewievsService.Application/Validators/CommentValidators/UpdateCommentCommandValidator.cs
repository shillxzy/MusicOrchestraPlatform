using FluentValidation;
using RewievsService.Application.Commands.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Validators.CommentValidators
{
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(x => x.CommentId)
                .NotEmpty().WithMessage("Comment Id is required");

            RuleFor(x => x.NewText)
                .NotNull().WithMessage("Text is required");
        }
    }
}
