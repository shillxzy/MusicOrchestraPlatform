using AutoMapper;
using CatalogService.BLL.DTOs.Composition;
using CatalogService.BLL.DTOs.ConcertProgram;
using CatalogService.BLL.DTOs.Instrument;
using CatalogService.BLL.DTOs.InstrumentImage;
using CatalogService.BLL.DTOs.Performer;
using CatalogService.Domain.Entities;


namespace CatalogService.BLL.MappingProfiles
{
    public class CatalogMappingProfile : Profile
    {
        public CatalogMappingProfile()
        {
            // 🔹 Instrument
            CreateMap<Instrument, InstrumentDto>().ReverseMap();
            CreateMap<InstrumentCreateDto, Instrument>();
            CreateMap<InstrumentUpdateDto, Instrument>();

            // 🔹 Performer
            CreateMap<Performer, PerformerDto>().ReverseMap();
            CreateMap<PerformerCreateDto, Performer>();
            CreateMap<PerformerUpdateDto, Performer>();

            // 🔹 Composition
            CreateMap<Composition, CompositionDto>().ReverseMap();
            CreateMap<CompositionCreateDto, Composition>();
            CreateMap<CompositionUpdateDto, Composition>();

            // 🔹 ConcertProgram
            CreateMap<ConcertProgram, ConcertProgramDto>().ReverseMap();
            CreateMap<ConcertProgramCreateDto, ConcertProgram>();
            CreateMap<ConcertProgramUpdateDto, ConcertProgram>();

            // 🔹 InstrumentImage
            CreateMap<InstrumentImage, InstrumentImageDto>().ReverseMap();
            CreateMap<InstrumentImageCreateDto, InstrumentImage>();
            CreateMap<InstrumentImageUpdateDto, InstrumentImage>();
        }
    }
}
