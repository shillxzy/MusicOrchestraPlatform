using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL.DTOs.Composition
{
    public class CompositionUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Duration { get; set; }
        public string Genre { get; set; } = null!;
    }
}
