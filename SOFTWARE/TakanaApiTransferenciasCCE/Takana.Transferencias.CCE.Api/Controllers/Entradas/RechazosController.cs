using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Annotations;
using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Atributos;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using static Takana.Transferencias.CCE.Api.Common.SwaggerExamplesDTO;

namespace Takana.Transferencias.CCE.Api.Controllers.Entradas
{
    [ApiController]
    [ApiVersion("3.5")]
    [Route("ssv2/payment-api/v{version:apiVersion}.0")]
    public class RechazosController : BaseController<RechazosController>
    {
        private readonly IServicioAplicacionTransferenciaEntrada _servicioAplicacionTransferenciaEntrada;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="servicioAplicacionTransferenciaEntrada"></param>
        public RechazosController(
            IBitacora<RechazosController> bitacora,
            IServicioAplicacionTransferenciaEntrada servicioAplicacionTransferenciaEntrada) : base(bitacora)
        {
            _servicioAplicacionTransferenciaEntrada = servicioAplicacionTransferenciaEntrada;
        }

        /// <summary>
        /// Metodo que procesa la operación el Rechazo de la Camara de Compensación Electronica      
        /// </summary>
        /// <param name="datosRechazos">Datos de rechazo enviados por la CCE</param>
        /// <param name="datosEncabezado">Datos del encabezado enviados por la CCE</param>
        /// <returns>retorna true</returns>
        [HttpPost]
        [Route("REJECT")]
        [SwaggerOperation(
            Tags = new[] { "END POINT ENTRADA Rechazo de Trama" }, 
            Summary = "La CCE rechaza una trama enviada por CMACT por estructura", 
            OperationId = "RechazoEntrada")]
        [SwaggerRequestExample(typeof(EstructuraContenidoReject), typeof(EstructuraContenidoRejectExample))]
        [EncryptacionResponseBody]
        public async Task RechazoEstructuraEntrada(
            [FromBody] EstructuraContenidoReject datosRechazos, [FromHeader] EstructuraEncabezado datosEncabezado)
        {
            _bitacora.Trace("Iniciando proceso de Rechazo por Transferencias Interbancarias Inmediatas - CCE.",
                $"Identificador de la Solicitud: {datosEncabezado.IdentificadorSolicitud}");
            await _servicioAplicacionTransferenciaEntrada.RechazoDeCCE(datosRechazos.Reject);      
        }
    }
}
