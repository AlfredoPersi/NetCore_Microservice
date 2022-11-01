using System;
using System.Collections.Generic;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class CarritoDTO
    {
        public int CarritoId { get; set; }
        public DateTime? FechaCreacionSesion { get; set; }
        public List<CarritoDetalleDTO> ListaProductos { get; set; }
    }
}

