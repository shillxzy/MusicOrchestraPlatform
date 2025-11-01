using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL.DTOs.ConcertProgram
{
    public class ConcertProgramDto
    {
        public int Id { get; set; }
        public int ConcertId { get; set; }
        public int CompositionId { get; set; }
        public string? CompositionTitle { get; set; }
    }
}
