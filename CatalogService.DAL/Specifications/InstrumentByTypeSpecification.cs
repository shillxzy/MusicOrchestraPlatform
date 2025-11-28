using CatalogService.Domain.Entities;
using System.Linq.Expressions;


namespace CatalogService.DAL.Specifications
{
    public class InstrumentByTypeSpecification : ISpecification<Instrument>
    {
        private readonly string _type;

        public InstrumentByTypeSpecification(string type)
        {
            _type = type;
        }

        public Expression<Func<Instrument, bool>> ToExpression()
        {
            return i => i.Type == _type;
        }
    }
}
