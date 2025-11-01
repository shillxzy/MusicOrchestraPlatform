using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL.DTOs.ConcertProgram
{
    public class ConcertProgramCreateDto
    {
        public int ConcertId { get; set; }
        public int CompositionId { get; set; }
    }
}
