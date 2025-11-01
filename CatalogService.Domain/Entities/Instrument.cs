using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Entities
{
    public class Instrument
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public decimal Price { get; set; }

        // 1:N → Instrument → Performer
        public ICollection<Performer> Performers { get; set; } = new List<Performer>();

        // 1:1 → Instrument ↔ InstrumentImage
        public InstrumentImage? InstrumentImage { get; set; }
    }
}
