using ApiSpaDemo.Models.DTO;
using ApiSpaDemo.Models;
using AutoMapper;

namespace ApiSpaDemo.Controllers.MappingProfiles
{
    public class ReservaMapProfile : Profile
    {
        public ReservaMapProfile()
        {
            CreateMap<Reserva, ReservaDTO>().ReverseMap();
        }
    }
}
