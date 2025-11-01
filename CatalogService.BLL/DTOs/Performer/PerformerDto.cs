using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL.DTOs.Performer
{
    public class PerformerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int InstrumentId { get; set; }
        public string? InstrumentName { get; set; }
    }
}
