using ApiSpaDemo.Models.DTO;
using ApiSpaDemo.Models;
using AutoMapper;

namespace ApiSpaDemo.Controllers.MappingProfiles
{
    public class ReservaMapProfile : Profile
    {
        public ReservaMapProfile()
        {
            CreateMap<Reserva, ReservaDTO>()
                .ForMember(dest => dest.Turnos, opt => opt.MapFrom(src => src.Turnos))
                .ForMember(dest => dest.Pago, opt => opt.MapFrom(src => src.Pago))
                .ReverseMap();
            CreateMap<Reserva, ReservaSimpleDTO>().ReverseMap();
        }
    }
}
