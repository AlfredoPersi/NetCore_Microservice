using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.Api.Libro.Persistencia
{
    public class ConsultaFiltro
    {
        public class LibroUnico : IRequest<LibroMaterialDTO>
        {
            public Guid? LibroId { get; set; }
        }

        public class Manejador : IRequestHandler<LibroUnico, LibroMaterialDTO>
        {
            private readonly ContextoLibreria dbContext;
            private readonly IMapper mapper;

            public Manejador(ContextoLibreria dbContext,
                             IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<LibroMaterialDTO> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await this.dbContext.LibreriaMaterial.SingleOrDefaultAsync(x => x.LibreriaMaterialId == request.LibroId.ToString());

                if (libro == null)
                {
                    throw new Exception("No se encontro el libro");
                }

                return this.mapper.Map<LibreriaMaterial, LibroMaterialDTO>(libro);
            }
        }
    }
}

