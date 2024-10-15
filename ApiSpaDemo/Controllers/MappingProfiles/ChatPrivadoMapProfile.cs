﻿using ApiSpaDemo.Models.DTO;
using ApiSpaDemo.Models;
using AutoMapper;

namespace ApiSpaDemo.Controllers.MappingProfiles
{
    public class ChatPrivadoMapProfile : Profile
    {
        public ChatPrivadoMapProfile()
        {
            CreateMap<ChatPrivado, ChatPrivadoDTO>().ReverseMap();
        }
    }
}