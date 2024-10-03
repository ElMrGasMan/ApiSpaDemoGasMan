using ApiSpaDemo.Models.DTO;
using ApiSpaDemo.Models;
using AutoMapper;

namespace ApiSpaDemo.Controllers.MappingProfiles
{
    public class PagoMapProfile : Profile
    {
        public PagoMapProfile()
        {
            CreateMap<Pago, PagoDTO>().ReverseMap();
        }
    }
}
