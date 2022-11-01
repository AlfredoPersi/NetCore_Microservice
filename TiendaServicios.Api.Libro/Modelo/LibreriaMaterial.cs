using System;
using System.Collections.Generic;
using MediatR;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Modelo
{
    public class LibreriaMaterial : IRequest<List<Consulta.Ejecuta>>
    {
        public string LibreriaMaterialId { get; set; }
        public string Titulo { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public Guid? AutorLibro { get; set; }

        public LibreriaMaterial()
        {
        }
    }
}

