using AutoMapper;
using CatalogService.BLL.DTOs.InstrumentImage;
using CatalogService.BLL.Services.Interfaces;
using CatalogService.DAL.UOW;
using CatalogService.Domain.Entities;

namespace CatalogService.BLL.Services
{
    public class InstrumentImageService : IInstrumentImageService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public InstrumentImageService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InstrumentImageDto>> GetAllAsync()
        {
            var images = await _uow.InstrumentImages.GetAllAsync();
            return _mapper.Map<IEnumerable<InstrumentImageDto>>(images);
        }

        public async Task<InstrumentImageDto?> GetByIdAsync(int id)
        {
            var image = await _uow.InstrumentImages.GetByIdAsync(id);
            return _mapper.Map<InstrumentImageDto>(image);
        }

        public async Task<InstrumentImageDto> CreateAsync(InstrumentImageCreateDto dto)
        {
            var entity = _mapper.Map<InstrumentImage>(dto);
            await _uow.InstrumentImages.AddAsync(entity);
            await _uow.SaveChangesAsync();
            return _mapper.Map<InstrumentImageDto>(entity);
        }

        public async Task<InstrumentImageDto> UpdateAsync(InstrumentImageUpdateDto dto)
        {
            var entity = await _uow.InstrumentImages.GetByIdAsync(dto.Id);
            if (entity == null) throw new KeyNotFoundException($"InstrumentImage with ID {dto.Id} not found");

            _mapper.Map(dto, entity);
            _uow.InstrumentImages.Update(entity);
            await _uow.SaveChangesAsync();

            return _mapper.Map<InstrumentImageDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.InstrumentImages.GetByIdAsync(id);
            if (entity == null) return false;

            _uow.InstrumentImages.Delete(entity);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
