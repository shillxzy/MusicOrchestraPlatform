using CatalogService.BLL.DTOs.ConcertProgram;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL.Validators
{
    public class ConcertProgramCreateDtoValidator : AbstractValidator<ConcertProgramCreateDto>
    {
        public ConcertProgramCreateDtoValidator()
        {
            RuleFor(x => x.ConcertId).GreaterThan(0);
            RuleFor(x => x.CompositionId).GreaterThan(0);
        }
    }

    public class ConcertProgramUpdateDtoValidator : AbstractValidator<ConcertProgramUpdateDto>
    {
        public ConcertProgramUpdateDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.CompositionId).GreaterThan(0);
        }
    }
}
