using System;
using AutoMapper;
using TiendaServicios.Api.Autor.Models;

namespace TiendaServicios.Api.Autor.Aplication
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AutorLibro, AutorDTO>().ReverseMap();
        }
    }
}

