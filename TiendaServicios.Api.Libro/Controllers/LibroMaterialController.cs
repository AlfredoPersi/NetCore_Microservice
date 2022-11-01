using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.Libro.Persistencia;


namespace TiendaServicios.Api.Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroMaterialController : Controller
    {
        private readonly IMediator mediator;

        public LibroMaterialController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await mediator.Send(data);
        }

        [HttpGet]
        public async Task<List<LibroMaterialDTO>> GetLibros()
        {
            return await mediator.Send(new Consulta.Ejecuta());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroMaterialDTO>> GetLibroUnico(Guid id)
        {
            return await mediator.Send(new ConsultaFiltro.LibroUnico() { LibroId = id });
        }
    }
}

