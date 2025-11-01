using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL.DTOs.InstrumentImage
{
    public class InstrumentImageCreateDto
    {
        public int InstrumentId { get; set; }
        public string Url { get; set; } = null!;
    }
}
