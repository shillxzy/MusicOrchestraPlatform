using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Entities
{
    public class Composition
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public double Duration { get; set; } // у хвилинах
        public string Genre { get; set; } = null!;

        public ICollection<ConcertProgram> ConcertPrograms { get; set; } = new List<ConcertProgram>();
    }
}
