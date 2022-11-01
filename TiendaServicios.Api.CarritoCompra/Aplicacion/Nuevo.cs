using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TiendaServicios.Api.CarritoCompra.Modelo;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest 
        {
            public DateTime FechaCreacionSesion { get; set; }
            public List<string> ProductoLista { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CarritoContext dbContext;

            public Manejador(CarritoContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                CarritoSesion csObj = new CarritoSesion()
                {
                    FechaCreacion = request.FechaCreacionSesion
                };

                dbContext.CarritoSesions.Add(csObj);

                var result = await dbContext.SaveChangesAsync();

                if (result == 0)
                {
                    throw new Exception("Errores en la insercion del carrito de compras");
                }


                foreach (var obj in request.ProductoLista)
                {
                    var detalleSesionObj = new CarritoSesionDetalle()
                    {
                        CarritoSesionId = csObj.CarritoSesionId,
                        FechaCreacion = DateTime.Now,
                        ProductoSeleccionado = obj
                    };

                    dbContext.CarritoSesionDetalles.Add(detalleSesionObj);
                }

                result = await dbContext.SaveChangesAsync();

                if (result == 0)
                {
                    throw new Exception("Errores en la insercion del detalle de carrito de compras");
                }

                return Unit.Value;
            }
        }

    }
}

