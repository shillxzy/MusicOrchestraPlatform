using CatalogService.Domain.Entities;
using System.Linq.Expressions;


namespace CatalogService.DAL.Specifications
{
    public class InstrumentWithImageSpecification : ISpecification<Instrument>
    {
        public Expression<Func<Instrument, bool>> ToExpression()
        {
            return i => i.InstrumentImage != null;
        }
    }
}
