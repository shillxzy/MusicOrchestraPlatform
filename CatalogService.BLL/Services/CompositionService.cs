using AutoMapper;
using CatalogService.BLL.DTOs.Composition;
using CatalogService.BLL.Services.Interfaces;
using CatalogService.DAL.UOW;
using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL.Services
{
    public class CompositionService : ICompositionService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CompositionService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompositionDto>> GetAllAsync()
        {
            var compositions = await _uow.Compositions.GetAllAsync();
            return _mapper.Map<IEnumerable<CompositionDto>>(compositions);
        }

        public async Task<CompositionDto?> GetByIdAsync(int id)
        {
            var composition = await _uow.Compositions.GetByIdAsync(id);
            return _mapper.Map<CompositionDto>(composition);
        }

        public async Task<CompositionDto> CreateAsync(CompositionCreateDto dto)
        {
            var entity = _mapper.Map<Composition>(dto);
            await _uow.Compositions.AddAsync(entity);
            await _uow.SaveChangesAsync();
            return _mapper.Map<CompositionDto>(entity);
        }

        public async Task<CompositionDto> UpdateAsync(CompositionUpdateDto dto)
        {
            var entity = await _uow.Compositions.GetByIdAsync(dto.Id);
            if (entity == null) throw new KeyNotFoundException($"Composition with ID {dto.Id} not found");

            _mapper.Map(dto, entity);
            _uow.Compositions.Update(entity);
            await _uow.SaveChangesAsync();

            return _mapper.Map<CompositionDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Compositions.GetByIdAsync(id);
            if (entity == null) return false;

            _uow.Compositions.Delete(entity);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
