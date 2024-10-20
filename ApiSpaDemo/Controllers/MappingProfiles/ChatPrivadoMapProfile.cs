using ApiSpaDemo.Models.DTO;
using ApiSpaDemo.Models;
using AutoMapper;

namespace ApiSpaDemo.Controllers.MappingProfiles
{
    public class ChatPrivadoMapProfile : Profile
    {
        public ChatPrivadoMapProfile()
        {
            CreateMap<ChatPrivado, ChatPrivadoDTO>()
                .ForMember(dest => dest.Mensajes, opt => opt.MapFrom(src => src.Mensajes))
                .ReverseMap();
        }
    }
}
