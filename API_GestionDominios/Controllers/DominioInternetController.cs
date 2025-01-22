using API_GestionDominios.Controllers.Interfaces;
using Aplicacion.Caracteristicas.DominioInternet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_GestionDominios.Controllers
{
    [Authorize]
    public class DominioInternetController : ApiBaseController
    {
        [HttpGet]        
        public async Task<ActionResult> ObtenerDominiosPorUsuario()
        {
            var data = await Mediator.Send(new ObtenerDominiosInternet.Consulta());
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult> CrearDominio(CrearDominioInternet.DatosCrearDominioInternet request)
        {
            var data = await Mediator.Send(new CrearDominioInternet.Comando(request));
            return Ok(data);
        }

        [HttpPatch]
        public async Task<ActionResult> ActualizarDominio(ActualizarDominioInternet.DatosActualizarDominioInternet request)
        {
            var data = await Mediator.Send(new ActualizarDominioInternet.Comando(request));
            return Ok(data);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> EliminarDominio(int id)
        {
            var data = await Mediator.Send(new EliminarDominioInternet.Comando(id));
            return Ok(data);
        }
    }
}
