using AutoMapper;
using CatalogService.BLL.DTOs.Instrument;
using CatalogService.BLL.Services.Interfaces;
using CatalogService.DAL.Specifications;
using CatalogService.DAL.UOW;


namespace CatalogService.BLL.Services
{
    public class InstrumentService : IInstrumentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public InstrumentService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InstrumentDto>> GetAllAsync()
        {
            var instruments = await _uow.Instruments.GetAllAsync();
            return _mapper.Map<IEnumerable<InstrumentDto>>(instruments);
        }

        public async Task<InstrumentDto?> GetByIdAsync(int id)
        {
            var instrument = await _uow.Instruments.GetByIdAsync(id);
            return _mapper.Map<InstrumentDto>(instrument);
        }

        public async Task<InstrumentDto> CreateAsync(InstrumentCreateDto dto)
        {
            var entity = _mapper.Map<Domain.Entities.Instrument>(dto);
            await _uow.Instruments.AddAsync(entity);
            await _uow.SaveChangesAsync();
            return _mapper.Map<InstrumentDto>(entity);
        }

        public async Task<InstrumentDto> UpdateAsync(InstrumentUpdateDto dto)
        {
            var entity = await _uow.Instruments.GetByIdAsync(dto.Id);
            if (entity == null) throw new KeyNotFoundException($"Instrument with ID {dto.Id} not found");

            _mapper.Map(dto, entity);
            _uow.Instruments.Update(entity);
            await _uow.SaveChangesAsync();

            return _mapper.Map<InstrumentDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Instruments.GetByIdAsync(id);
            if (entity == null) return false;

            _uow.Instruments.Delete(entity);
            await _uow.SaveChangesAsync();
            return true;
        }

    }
}
