using CatalogService.BLL.DTOs.Instrument;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL.Validators
{
    public class InstrumentCreateDtoValidator : AbstractValidator<InstrumentCreateDto>
    {
        public InstrumentCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Instrument name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Instrument type is required.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }

    public class InstrumentUpdateDtoValidator : AbstractValidator<InstrumentUpdateDto>
    {
        public InstrumentUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.Name)
                .NotEmpty().MaximumLength(100);

            RuleFor(x => x.Price)
                .GreaterThan(0);
        }
    }
}
