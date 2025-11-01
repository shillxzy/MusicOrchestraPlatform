using AutoMapper;
using CatalogService.BLL.DTOs.ConcertProgram;
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
    public class ConcertProgramService : IConcertProgramService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ConcertProgramService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ConcertProgramDto>> GetAllAsync()
        {
            var programs = await _uow.ConcertPrograms.GetAllAsync();
            return _mapper.Map<IEnumerable<ConcertProgramDto>>(programs);
        }

        public async Task<ConcertProgramDto?> GetByIdAsync(int id)
        {
            var program = await _uow.ConcertPrograms.GetByIdAsync(id);
            return _mapper.Map<ConcertProgramDto>(program);
        }

        public async Task<ConcertProgramDto> CreateAsync(ConcertProgramCreateDto dto)
        {
            var entity = _mapper.Map<ConcertProgram>(dto);
            await _uow.ConcertPrograms.AddAsync(entity);
            await _uow.SaveChangesAsync();
            return _mapper.Map<ConcertProgramDto>(entity);
        }

        public async Task<ConcertProgramDto> UpdateAsync(ConcertProgramUpdateDto dto)
        {
            var entity = await _uow.ConcertPrograms.GetByIdAsync(dto.Id);
            if (entity == null) throw new KeyNotFoundException($"ConcertProgram with ID {dto.Id} not found");

            _mapper.Map(dto, entity);
            _uow.ConcertPrograms.Update(entity);
            await _uow.SaveChangesAsync();

            return _mapper.Map<ConcertProgramDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.ConcertPrograms.GetByIdAsync(id);
            if (entity == null) return false;

            _uow.ConcertPrograms.Delete(entity);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
