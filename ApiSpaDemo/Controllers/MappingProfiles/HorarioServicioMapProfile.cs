using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;
using AutoMapper;

namespace ApiSpaDemo.Controllers.MappingProfiles
{
    public class HorarioServicioMapProfile : Profile
    {
        public HorarioServicioMapProfile()
        {
            CreateMap<HorarioServicio, HorarioServicioDTO>().ReverseMap();
        }
    }
}
