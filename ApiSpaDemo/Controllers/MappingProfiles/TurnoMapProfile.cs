using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;

using AutoMapper;

namespace APIWeb_SPASentirseBien.Controllers.MappingProfiles
{
    public class TurnoMapProfile : Profile
    {
        public TurnoMapProfile()
        {
            CreateMap<Turno, TurnoDTO>();
            CreateMap<TurnoDTO, Turno>();
        }
    }
}