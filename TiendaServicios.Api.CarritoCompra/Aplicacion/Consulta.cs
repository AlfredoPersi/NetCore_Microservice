using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarritoCompra.Persistencia;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;
using TiendaServicios.Api.CarritoCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<CarritoDTO>
        {
            public int CarritoSesionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDTO>
        {
            private readonly CarritoContext dbContext;
            private readonly ILibroService libroService;

            public Manejador(CarritoContext dbContext,
                             ILibroService libroService)
            {
                this.dbContext = dbContext;
                this.libroService = libroService;
            }

            public async Task<CarritoDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = await this.dbContext.CarritoSesions.SingleOrDefaultAsync(x => x.CarritoSesionId == request.CarritoSesionId);
                var carritoSesionDetalle = await this.dbContext.CarritoSesionDetalles.Where(x => x.CarritoSesionId == request.CarritoSesionId).ToListAsync();

                List<CarritoDetalleDTO> listaDetallesDTOs = new List<CarritoDetalleDTO>();

                foreach (var libro in carritoSesionDetalle)
                {
                    var response = await this.libroService.GetLibro(new Guid(libro.ProductoSeleccionado));

                    if (response.Resultado)
                    {
                        var objLibro = response.Libro;

                        var detalleDTO = new CarritoDetalleDTO()
                        {
                            TituloLibro = objLibro.Titulo,
                            FechaPublicacion = objLibro.FechaPublicacion,
                            LibroId = objLibro.LibreriaMaterialId,
                        };

                        listaDetallesDTOs.Add(detalleDTO);
                    }
                }

                var carritoSesionDTO = new CarritoDTO()
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion,
                    ListaProductos = listaDetallesDTOs
                };

                return carritoSesionDTO;
            }
        }
    }
}

