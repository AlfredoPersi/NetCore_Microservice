using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.Autor.Aplication;
using TiendaServicios.Api.Autor.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaServicios.Api.Autor.Controllers
{
    [Route("api/autor")]
    public class AutorController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IValidator<Nuevo.Ejecuta> validator;

        public AutorController(IMediator mediator,
                               IValidator<Nuevo.Ejecuta> validator)
        {
            this.mediator = mediator;
            this.validator = validator;
        }


        [HttpPost]
        public async Task<ActionResult<Unit>> Crear([FromBody]Nuevo.Ejecuta data)
        {
            ValidationResult result = await this.validator.ValidateAsync(data);

            if (result.IsValid)
            {
                return await mediator.Send(data);
            }

            return BadRequest(result.Errors);

        }

        [HttpGet]
        public async Task<ActionResult<List<AutorDTO>>> GetAutores()
        {
            return await mediator.Send(new Consulta.ListaAutor());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutorDTO>> GetAutorLibro(string id)
        {
            return await mediator.Send(new ConsultaFiltro.AutorUnico() { GuidId = id });
        }
        
    }
}

