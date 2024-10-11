using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;
using AutoMapper;

namespace ApiSpaDemo.Controllers.MappingProfiles
{
    public class TurnoMapProfile : Profile
    {
        public TurnoMapProfile()
        {
            CreateMap<Turno, TurnoDTO>().ReverseMap();
        }
    }
}
