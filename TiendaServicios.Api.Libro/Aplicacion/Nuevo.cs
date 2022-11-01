using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.Api.Libro.Persistencia
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Titulo { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public Guid? AutorLibro { get; set; }
        }
        
        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
                RuleFor(x => x.AutorLibro).NotEmpty();
            }
        }
        
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoLibreria dbContext;

            public Manejador(ContextoLibreria dbContext)
            {
                this.dbContext = dbContext;
            }
            
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                LibreriaMaterial obj = new LibreriaMaterial()
                {
                    Titulo = request.Titulo,
                    FechaPublicacion = request.FechaPublicacion,
                    AutorLibro = request.AutorLibro,
                    LibreriaMaterialId = Guid.NewGuid().ToString()
                };

                this.dbContext.LibreriaMaterial.Add(obj);
                var result = await this.dbContext.SaveChangesAsync();

                if (result > 0)
                {
                    return Unit.Value;
                }
                
                throw new Exception("No se pudo guardar el libro");
            }
        }
        
        
    }
}