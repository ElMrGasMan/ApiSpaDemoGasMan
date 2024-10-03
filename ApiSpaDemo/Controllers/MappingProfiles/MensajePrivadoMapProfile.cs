using ApiSpaDemo.Models.DTO;
using ApiSpaDemo.Models;
using AutoMapper;

namespace ApiSpaDemo.Controllers.MappingProfiles
{
    public class MensajePrivadoMapProfile : Profile
    {
        public MensajePrivadoMapProfile()
        {
            CreateMap<MensajePrivado, MensajePrivadoDTO>().ReverseMap();
        }
    }
}
