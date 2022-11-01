using System;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarritoCompra.Modelo;

namespace TiendaServicios.Api.CarritoCompra.Persistencia
{
    public class CarritoContext : DbContext
    {
        public CarritoContext(DbContextOptions<CarritoContext> options) : base(options)
        {

        }

        public DbSet<CarritoSesion> CarritoSesions { get; set; }
        public DbSet<CarritoSesionDetalle> CarritoSesionDetalles { get; set; }
    }
}

