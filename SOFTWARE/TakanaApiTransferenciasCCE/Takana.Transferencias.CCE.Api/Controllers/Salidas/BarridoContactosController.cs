using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using Takana.Transferencias.CCE.Api.Atributos;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad;
using static Takana.Transferencias.CCE.Api.Common.SwaggerExamplesDTO;

namespace Takana.Transferencias.CCE.Api.Controllers.Salidas
{
    /// <summary>
    /// Clase que redirecciona los datos recibidor del canal hacia los servicios requeridos
    /// </summary>    
    [ApiController]
    [GenerarSesion]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}.0")]
    [Authorize(Policy = "ModuloAppMovil")]
    public class BarridoContactosController : BaseController<BarridoContactosController>
    {
        private readonly IServicioAplicacionInteroperabilidad _servicioAplicacionInteroperabilidad;

        /// <summary>
        /// Método constructor
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="interoperabilidadServicio"></param>
        public BarridoContactosController(
            IBitacora<BarridoContactosController> bitacora,
            IServicioAplicacionInteroperabilidad interoperabilidadServicio) : base(bitacora)
        {
            _servicioAplicacionInteroperabilidad = interoperabilidadServicio;
        }

        /// <summary>
        /// Barrido de contactos de interoperabilidad
        /// </summary>
        /// <param name="contactos">Contactos a mandar al barrido</param>
        /// <returns>Entidad o directorios a los que los contactos esten afiliados</returns>
        [HttpPost]
        [Route("barrido")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operacion Barrido de contactos de interoperabilidad" },
            Summary = "Buscalos numeros celulares afiliados en los directorios de la CCE, Yape, plin, entre otros.",
            OperationId = "BarridoContacto")]
        [SwaggerRequestExample(typeof(EntradaBarridoDTO), typeof(EntradaBarridoDTOExample))]
        [SwaggerResponseExample(200, typeof(ResultadoPrincipalBarridoDTOExample))]
        public async Task<ActionResult<ResultadoPrincipalBarridoDTO>> BarridoContacto(
            [FromBody] EntradaBarridoDTO contactos)
        {
            _bitacora.Trace("Iniciando proceso de Barrido de contactos - CCE.");
            return await InvocarOperacionDesdeServicios(async () => await
                 _servicioAplicacionInteroperabilidad.BarridoContacto(contactos));
        }

    }
}