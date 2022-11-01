using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using TiendaServicios.Api.Autor.Models;
using TiendaServicios.Api.Autor.Persistences;

namespace TiendaServicios.Api.Autor.Aplication
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        { 
            // SETEO QUE LOS SIGUIENTES CAMPOS NO PUEDEN SER VACIOS
            public EjecutaValidacion()
            {
                RuleFor(x => x.Nombre).NotEmpty().NotNull().WithMessage("Nombre es requerido");
                RuleFor(x => x.Apellido).NotEmpty().NotNull().WithMessage("Apellido es requerido");         
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoAutor contexto;

            public Manejador(ContextoAutor contexto)
            {
                this.contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var autorLibro = new AutorLibro()
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    FechaNacimiento = request.FechaNacimiento,
                    AutorLibroGuid = Guid.NewGuid().ToString()
                };

                contexto.AutorLibros.Add(autorLibro);

                var valor = await contexto.SaveChangesAsync();

                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar autor");
            }
        }
    }
}

