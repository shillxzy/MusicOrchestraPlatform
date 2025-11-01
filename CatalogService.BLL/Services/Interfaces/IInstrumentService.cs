using CatalogService.BLL.DTOs.Instrument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL.Services.Interfaces
{
    public interface IInstrumentService
    {
        Task<IEnumerable<InstrumentDto>> GetAllAsync();
        Task<InstrumentDto?> GetByIdAsync(int id);
        Task<InstrumentDto> CreateAsync(InstrumentCreateDto dto);
        Task<InstrumentDto> UpdateAsync(InstrumentUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
