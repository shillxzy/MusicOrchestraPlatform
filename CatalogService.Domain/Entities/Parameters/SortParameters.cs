using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Entities.Parameters
{
    public class SortParameters
    {
        public string? SortField { get; set; }
        public bool Descending { get; set; } = false;
    }
}
