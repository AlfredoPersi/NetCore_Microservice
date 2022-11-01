using System;
namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class CarritoDetalleDTO
    {
        public string LibroId { get; set; }
        public string TituloLibro { get; set; }
        public string AutorLibro { get; set; }
        public DateTime? FechaPublicacion { get; set; }
    }
}

