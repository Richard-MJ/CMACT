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
    /// <summary>
    /// Clase que recibe el requermiento de Consulta Cuenta
    /// </summary>
    [ApiController]
    [ApiVersion("3.5")]
    [Route("ssv2/payment-api/v{version:apiVersion}.0")]    
    public class ConsultasController : BaseController<ConsultasController>
        {
        private readonly IServicioAplicacionTransferenciaEntrada _servicioAplicacionTransferenciaEntrada;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="servicioAplicacionTransferenciaEntrada"></param>
        public ConsultasController(
            IBitacora<ConsultasController> bitacora,
            IServicioAplicacionTransferenciaEntrada servicioAplicacionTransferenciaEntrada) : base(bitacora)
        {
             _servicioAplicacionTransferenciaEntrada = servicioAplicacionTransferenciaEntrada;
        }

        /// <summary>
        /// Metodo que procesa la operación de Consulta Cuenta de la Camara de Compensación Electronica
        /// </summary>
        /// <param name="datosRecibidos">Datos recepcionados enviados por la CCE</param>
        /// <param name="datosEncabezado">Datos del encabezado enviados por la CCE</param>
        /// <returns>Retorna datos del cliente receptor hacia la CCE</returns>
        [HttpPost]
        [Route("AV2")]
        [SwaggerOperation(
            Tags = new[] { "END POINT ENTRADA Consulta Cuenta" }, 
            Summary = "Obtiene datos del cliente receptor", 
            OperationId = "ConsultaCuentaEntrada")]
        [SwaggerRequestExample(typeof(EstructuraContenidoAV2), typeof(EstructuraContenidoAV2Example))]
        [SwaggerResponseExample(200, typeof(EstructuraContenidoAV3Example))]
        [EncryptacionResponseBody]
        public async Task<ActionResult<EstructuraContenidoAV3>> ConsultaCuentaEntrada(
           [FromBody] EstructuraContenidoAV2 datosRecibidos, [FromHeader] EstructuraEncabezado datosEncabezado)
        {
            _bitacora.Trace("Iniciando proceso de consulta de cuenta por Transferencias Interbancarias Inmediatas - CCE.",
                $"Identificador de la Solicitud: {datosEncabezado.IdentificadorSolicitud}"); 
            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionTransferenciaEntrada.ConsultaCuentaDeCCE(datosRecibidos.AV2, datosRecibidos.codigoValidacionFirma));
        }
    }
}
