using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Entities
{
    public class Performer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int InstrumentId { get; set; }
        public Instrument? Instrument { get; set; }
    }
}
