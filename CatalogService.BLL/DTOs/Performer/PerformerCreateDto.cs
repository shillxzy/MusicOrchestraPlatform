using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL.DTOs.Performer
{
    public class PerformerCreateDto
    {
        public string Name { get; set; } = null!;
        public int InstrumentId { get; set; }
    }
}
