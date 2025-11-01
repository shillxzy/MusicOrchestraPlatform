using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Entities
{
    public class InstrumentImage
    {
        public int Id { get; set; }
        public int InstrumentId { get; set; }
        public string Url { get; set; } = null!;

        public Instrument? Instrument { get; set; }
    }
}
