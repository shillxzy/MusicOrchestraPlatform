using CatalogService.BLL.DTOs.Performer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL.Validators
{
    public class PerformerCreateDtoValidator : AbstractValidator<PerformerCreateDto>
    {
        public PerformerCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Performer name is required.");

            RuleFor(x => x.InstrumentId)
                .GreaterThan(0).WithMessage("Valid InstrumentId required.");
        }
    }

    public class PerformerUpdateDtoValidator : AbstractValidator<PerformerUpdateDto>
    {
        public PerformerUpdateDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
