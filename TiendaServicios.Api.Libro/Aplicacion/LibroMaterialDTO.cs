using System;

namespace TiendaServicios.Api.Libro.Persistencia
{
    public class LibroMaterialDTO
    {
        public Guid? LibreriaMaterialId { get; set; }
        public string Titulo { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public Guid? AutorLibro { get; set; }
    }
}