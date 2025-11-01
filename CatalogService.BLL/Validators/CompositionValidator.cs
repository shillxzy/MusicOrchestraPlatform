using CatalogService.BLL.DTOs.Composition;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL.Validators
{
    public class CompositionCreateDtoValidator : AbstractValidator<CompositionCreateDto>
    {
        public CompositionCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(150);

            RuleFor(x => x.Duration)
             .GreaterThan(0).WithMessage("Duration must be greater than zero.");

            RuleFor(x => x.Genre)
                .NotEmpty().WithMessage("Genre is required.");
        }
    }

    public class CompositionUpdateDtoValidator : AbstractValidator<CompositionUpdateDto>
    {
        public CompositionUpdateDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Title).NotEmpty().MaximumLength(150);
        }
    }
}
