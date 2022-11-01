using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.CarritoCompra.Aplicacion;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaServicios.Api.CarritoCompra.Controllers
{
    [Route("api/[controller]")]
    public class CarritoComprasController : ControllerBase
    {
        private readonly IMediator mediator;

        public CarritoComprasController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear([FromBody]Nuevo.Ejecuta data)
        {
            return await this.mediator.Send(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoDTO>> GetCarrito(int id)
        {
            return await this.mediator.Send(new Consulta.Ejecuta() { CarritoSesionId = id });
        }
    }
}

