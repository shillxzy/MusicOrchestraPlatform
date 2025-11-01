using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL.DTOs.InstrumentImage
{
    public class InstrumentImageDto
    {
        public int Id { get; set; }
        public int InstrumentId { get; set; }
        public string Url { get; set; } = null!;
    }
}
