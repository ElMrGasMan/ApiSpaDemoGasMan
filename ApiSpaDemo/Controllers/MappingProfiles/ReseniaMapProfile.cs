using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;
using ApiSpaDemo.Models.DTO.PatchDTOs;
using AutoMapper;

namespace APIWeb_SPASentirseBien.Controllers.MappingProfiles
{
    public class ReseniaMapProfile : Profile
    {
        public ReseniaMapProfile()
        {
            CreateMap<Resenia, ReseniaDTO>();
            CreateMap<ReseniaDTO, Resenia>();
            CreateMap<ReseniaPatchDTO, Resenia>()
            .ForMember(n => n.ReseniaId, option => option.Ignore())
            .ForMember(n => n.FechaPublicacion, option => option.Ignore())
            .ForMember(n => n.UsuarioClass, option => option.Ignore())
            .ForMember(n => n.UsuarioId, option => option.Ignore());
            CreateMap<Resenia, ReseniaPatchDTO>();
        }
    }
}