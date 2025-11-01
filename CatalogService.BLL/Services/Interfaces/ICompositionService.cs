using CatalogService.BLL.DTOs.Composition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL.Services.Interfaces
{
    public interface ICompositionService
    {
        Task<IEnumerable<CompositionDto>> GetAllAsync();
        Task<CompositionDto?> GetByIdAsync(int id);
        Task<CompositionDto> CreateAsync(CompositionCreateDto dto);
        Task<CompositionDto> UpdateAsync(CompositionUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
