using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;
using ApiSpaDemo.Models.DTO.PatchDTOs;
using AutoMapper;

namespace APIWeb_SPASentirseBien.Controllers.MappingProfiles
{
    public class RespuestaMapProfile : Profile
    {
        public RespuestaMapProfile()
        {
            CreateMap<Respuesta, RespuestaDTO>();
            CreateMap<RespuestaDTO, Respuesta>();
            CreateMap<RespuestaPatchDTO, Respuesta>()
            .ForMember(n => n.RespuestaId, option => option.Ignore())
            .ForMember(n => n.PreguntaId, option => option.Ignore())
            .ForMember(n => n.PreguntaClass, option => option.Ignore())
            .ForMember(n => n.UsuarioClass, option => option.Ignore())
            .ForMember(n => n.UsuarioId, option => option.Ignore());
            CreateMap<Respuesta, RespuestaPatchDTO>();
        }
    }
}