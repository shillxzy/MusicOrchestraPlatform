using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Entities
{
    public class ConcertProgram
    {
        public int Id { get; set; }

        public int ConcertId { get; set; }
        public int CompositionId { get; set; }

        // M:N → ConcertProgram ↔ Composition (через ConcertId)
        public Composition? Composition { get; set; }
    }
}
