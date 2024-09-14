using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;
using ApiSpaDemo.Models.DTO.PatchDTOs;
using AutoMapper;

namespace APIWeb_SPASentirseBien.Controllers.MappingProfiles
{
    public class PreguntaMapProfile : Profile
    {
        public PreguntaMapProfile()
        {
            CreateMap<Pregunta, PreguntaDTO>();
            CreateMap<PreguntaDTO, Pregunta>();
            CreateMap<PreguntaPatchDTO, Pregunta>()
            .ForMember(n => n.PreguntaId, option => option.Ignore())
            .ForMember(n => n.FechaPublicacion, option => option.Ignore())
            .ForMember(n => n.UsuarioClass, option => option.Ignore())
            .ForMember(n => n.UsuarioId, option => option.Ignore());
            CreateMap<Pregunta, PreguntaPatchDTO>();
        }
    }
}