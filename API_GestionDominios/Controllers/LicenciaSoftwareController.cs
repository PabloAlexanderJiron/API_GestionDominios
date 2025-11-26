using API_GestionDominios.Controllers.Interfaces;
using Aplicacion.Caracteristicas.DominioInternet;
using Aplicacion.Caracteristicas.LicenciaSoftware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_GestionDominios.Controllers
{
    [Authorize]
    public class LicenciaSoftwareController: ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult> ObtenerDominiosPorUsuario()
        {
            var data = await Mediator.Send(new ObtenerTodasLasLicencias.Consulta());
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult> CrearLicencia(CrearLicenciaSoftware.DatosCrearLicenciaSoftware request)
        {
            var data = await Mediator.Send(new CrearLicenciaSoftware.Comando(request));
            return Ok(data);
        }

        [HttpPatch]
        public async Task<ActionResult> ActualizarLicencia(EditarLicenciaSoftware.DatosEditarLicenciaSoftware request)
        {
            var data = await Mediator.Send(new EditarLicenciaSoftware.Comando(request));
            return Ok(data);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> EliminarLicencia(int id)
        {
            var data = await Mediator.Send(new EliminarLicenciaSoftware.Comando(id));
            return Ok(data);
        }
    }
}
