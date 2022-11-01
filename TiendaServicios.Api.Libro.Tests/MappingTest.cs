using System;
using AutoMapper;
using TiendaServicios.Api.Libro.Persistencia;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.Api.Libro.Tests
{
    public class MappingTest : Profile
    {


        public MappingTest()
        {
            CreateMap<LibreriaMaterial, LibroMaterialDTO>().ReverseMap();
        }
    }
}

