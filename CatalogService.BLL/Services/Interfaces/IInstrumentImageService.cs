using CatalogService.BLL.DTOs.InstrumentImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL.Services.Interfaces
{
    public interface IInstrumentImageService
    {
        Task<IEnumerable<InstrumentImageDto>> GetAllAsync();
        Task<InstrumentImageDto?> GetByIdAsync(int id);
        Task<InstrumentImageDto> CreateAsync(InstrumentImageCreateDto dto);
        Task<InstrumentImageDto> UpdateAsync(InstrumentImageUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
