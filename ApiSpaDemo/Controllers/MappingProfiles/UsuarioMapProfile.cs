using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiSpaDemo.Models;
using ApiSpaDemo.Models.DTO;
using AutoMapper;

namespace ApiSpaDemo.Controllers.MappingProfiles
{
    public class UsuarioMapProfile : Profile
    {
        public UsuarioMapProfile()
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
        }
    }
}