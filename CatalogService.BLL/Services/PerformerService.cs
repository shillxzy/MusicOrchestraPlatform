using AutoMapper;
using CatalogService.BLL.DTOs.Performer;
using CatalogService.BLL.Services.Interfaces;
using CatalogService.DAL.UOW;
using CatalogService.Domain.Entities;

namespace CatalogService.BLL.Services
{
    public class PerformerService : IPerformerService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PerformerService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PerformerDto>> GetAllAsync()
        {
            var performers = await _uow.Performers.GetAllAsync();
            return _mapper.Map<IEnumerable<PerformerDto>>(performers);
        }

        public async Task<PerformerDto?> GetByIdAsync(int id)
        {
            var performer = await _uow.Performers.GetByIdAsync(id);
            return _mapper.Map<PerformerDto>(performer);
        }

        public async Task<PerformerDto> CreateAsync(PerformerCreateDto dto)
        {
            var entity = _mapper.Map<Performer>(dto);
            await _uow.Performers.AddAsync(entity);
            await _uow.SaveChangesAsync();
            return _mapper.Map<PerformerDto>(entity);
        }

        public async Task<PerformerDto> UpdateAsync(PerformerUpdateDto dto)
        {
            var entity = await _uow.Performers.GetByIdAsync(dto.Id);
            if (entity == null) throw new KeyNotFoundException($"Performer with ID {dto.Id} not found");

            _mapper.Map(dto, entity);
            _uow.Performers.Update(entity);
            await _uow.SaveChangesAsync();

            return _mapper.Map<PerformerDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Performers.GetByIdAsync(id);
            if (entity == null) return false;

            _uow.Performers.Delete(entity);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
