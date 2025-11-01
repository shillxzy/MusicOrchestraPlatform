using CatalogService.BLL.DTOs.InstrumentImage;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL.Validators
{
    public class InstrumentImageCreateDtoValidator : AbstractValidator<InstrumentImageCreateDto>
    {
        public InstrumentImageCreateDtoValidator()
        {
            RuleFor(x => x.InstrumentId)
                .GreaterThan(0).WithMessage("InstrumentId is required.");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Image URL is required.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("Invalid URL format.");
        }
    }

    public class InstrumentImageUpdateDtoValidator : AbstractValidator<InstrumentImageUpdateDto>
    {
        public InstrumentImageUpdateDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Url)
                .NotEmpty()
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute));
        }
    }
}
