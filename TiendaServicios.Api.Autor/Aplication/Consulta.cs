using System;
using System.Collections.Generic;
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
    public class Consulta
    {
        public class ListaAutor : IRequest<List<AutorDTO>>
        {
            // no se pone autor porque queremos que devuelva el dato completo
        }

        public class Manejador : IRequestHandler<ListaAutor, List<AutorDTO>>
        {
            private readonly ContextoAutor dbContext;
            private readonly IMapper mapper;

            public Manejador(ContextoAutor dbContext,
                             IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<List<AutorDTO>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {
                var autores = await this.dbContext.AutorLibros.ToListAsync();

                var autoresDTO = mapper.Map<List<AutorLibro>, List<AutorDTO>>(autores);

                return autoresDTO;
            }
        }
    }
}

