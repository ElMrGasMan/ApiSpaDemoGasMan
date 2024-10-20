using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;
using ApiSpaDemo.Models.DTO.PatchDTOs;
using AutoMapper;

namespace APIWeb_SPASentirseBien.Controllers.MappingProfiles
{
    public class ServicioMapProfile : Profile
    {
        public ServicioMapProfile()
        {
            CreateMap<Servicio, ServicioDTO>()
                .ForMember(dest => dest.Turnos, opt => opt.MapFrom(src => src.Turnos));
            CreateMap<ServicioDTO, Servicio>()
                .ForMember(dest => dest.Turnos, opt => opt.MapFrom(src => src.Turnos));
            CreateMap<ServicioPatchDTO, Servicio>()
            .ForMember(n => n.ServicioId, option => option.Ignore())
            .ForMember(n => n.TipoServicio, option => option.Ignore())
            .ForMember(n => n.UsuarioClass, option => option.Ignore())
            .ForMember(n => n.UsuarioId, option => option.Ignore())
            .ForMember(n => n.ChatsPrivados, option => option.Ignore())
            .ForMember(n => n.Turnos, option => option.Ignore());
            CreateMap<Servicio, ServicioPatchDTO>();
        }
    }
}