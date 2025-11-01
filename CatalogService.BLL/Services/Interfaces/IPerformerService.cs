using CatalogService.BLL.DTOs.Performer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL.Services.Interfaces
{
    public interface IPerformerService
    {
        Task<IEnumerable<PerformerDto>> GetAllAsync();
        Task<PerformerDto?> GetByIdAsync(int id);
        Task<PerformerDto> CreateAsync(PerformerCreateDto dto);
        Task<PerformerDto> UpdateAsync(PerformerUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
