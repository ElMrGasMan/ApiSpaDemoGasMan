using ApiSpaDemo.Models.DTO;
using ApiSpaDemo.Models;
using AutoMapper;
using ApiSpaDemo.Models.DTO.PatchDTOs;

namespace ApiSpaDemo.Controllers.MappingProfiles
{
    public class NoticiaMapProfile : Profile
    {
        public NoticiaMapProfile()
        {
            // Mapear entre Noticium y NoticiaDTO
            //CreateMap<Noticium, NoticiaDTO>().ReverseMap();


            CreateMap<Noticium, NoticiaDTO>();
            CreateMap<NoticiaDTO, Noticium>();
            CreateMap<NoticiaPatchDTO, Noticium>()
            .ForMember(n => n.NoticiaId, option => option.Ignore())
            .ForMember(n => n.FechaPublicacion, option => option.Ignore());
            CreateMap<Noticium, NoticiaPatchDTO>();
        }
    }
}
