using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Models;
using TiendaServicios.Api.Autor.Persistences;

namespace TiendaServicios.Api.Autor.Aplication
{
    public class ConsultaFiltro
    {
        public class AutorUnico : IRequest<AutorDTO>
        {
            public string GuidId { get; set; }

        }

        public class Manejador : IRequestHandler<AutorUnico, AutorDTO>
        {
            private readonly ContextoAutor dbContext;
            private readonly IMapper mapper;

            public Manejador(ContextoAutor dbContext,
                             IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<AutorDTO> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                AutorLibro autor = await this.dbContext.AutorLibros.SingleOrDefaultAsync(x => x.AutorLibroGuid == request.GuidId);

                if (autor is null)
                {
                    throw new Exception("No se encontro autor");
                }

                return this.mapper.Map<AutorLibro, AutorDTO>(autor);
            }
        }
    }
}

