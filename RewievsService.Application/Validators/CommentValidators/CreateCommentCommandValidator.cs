using FluentValidation;
using RewievsService.Application.Commands.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Validators.CommentValidators
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.Comment.AuthorId)
                .NotEmpty().WithMessage("AuthorId is required");

            RuleFor(x => x.Comment.Text)
                .NotNull().WithMessage("Text is required");
        }
    }
}
