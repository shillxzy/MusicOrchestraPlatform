using CatalogService.BLL.DTOs.ConcertProgram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL.Services.Interfaces
{
    public interface IConcertProgramService
    {
        Task<IEnumerable<ConcertProgramDto>> GetAllAsync();
        Task<ConcertProgramDto?> GetByIdAsync(int id);
        Task<ConcertProgramDto> CreateAsync(ConcertProgramCreateDto dto);
        Task<ConcertProgramDto> UpdateAsync(ConcertProgramUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
