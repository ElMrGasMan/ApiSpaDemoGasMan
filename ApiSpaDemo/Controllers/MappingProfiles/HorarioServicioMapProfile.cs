using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;
using AutoMapper;

namespace ApiSpaDemo.Controllers.MappingProfiles
{
    public class HorarioServicioMapProfile : Profile
    {
        public HorarioServicioMapProfile()
        {
            // Mapear de HorarioServicioDTO a HorarioServicio
            CreateMap<HorarioServicioDTO, HorarioServicio>()
                .ForMember(dest => dest.HoraInicio, opt => opt.MapFrom(src => TimeOnly.FromTimeSpan(src.HoraInicio)))
                .ForMember(dest => dest.HoraFinal, opt => opt.MapFrom(src => TimeOnly.FromTimeSpan(src.HoraFinal)));

            // Mapear de HorarioServicio a HorarioServicioDTO
            CreateMap<HorarioServicio, HorarioServicioDTO>()
                .ForMember(dest => dest.HoraInicio, opt => opt.MapFrom(src => src.HoraInicio.ToTimeSpan()))
                .ForMember(dest => dest.HoraFinal, opt => opt.MapFrom(src => src.HoraFinal.ToTimeSpan()));
        }
    }
}
