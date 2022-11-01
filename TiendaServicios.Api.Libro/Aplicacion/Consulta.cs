using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.Api.Libro.Persistencia
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<LibroMaterialDTO>>
        {
        }

        public class Manejador : IRequestHandler<Ejecuta, List<LibroMaterialDTO>>
        {
            private readonly ContextoLibreria dbContext;
            private readonly IMapper mapper;

            public Manejador(ContextoLibreria dbContext,
                             IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }
            
            public async Task<List<LibroMaterialDTO>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros =
                    this.mapper.Map<List<LibreriaMaterial>, List<LibroMaterialDTO>>
                         (await this.dbContext.LibreriaMaterial.ToListAsync());

                return libros;
            }
        }
    }
}