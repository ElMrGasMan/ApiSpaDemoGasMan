using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;
using AutoMapper;

namespace ApiSpaDemo.Controllers.MappingProfiles
{
    public class NotificacionMapProfile : Profile
    {
        public NotificacionMapProfile()
        {
            CreateMap<Notificacion, NotificacionDTO>().ReverseMap();
        }
    }
}
