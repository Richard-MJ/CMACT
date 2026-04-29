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
    /// Clase que recibe el requermiento de Orden de Transferencia
    /// </summary>
    [ApiController]
    [ApiVersion("3.5")]
    [Route("ssv2/payment-api/v{version:apiVersion}.0")] 
    public class OrdenesController : BaseController<OrdenesController>
    {
        private readonly IServicioAplicacionTransferenciaEntrada _servicioAplicacionTransferenciaEntrada;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="servicioAplicacionTransferenciaEntrada"></param>
        public OrdenesController(
            IBitacora<OrdenesController> bitacora,
            IServicioAplicacionTransferenciaEntrada servicioAplicacionTransferenciaEntrada) : base(bitacora)
        {
            _servicioAplicacionTransferenciaEntrada = servicioAplicacionTransferenciaEntrada;
        }

        /// <summary>
        /// Metodo que procesa la operación de Orden de Transferencia de la Camara de Compensación Electronica
        /// </summary>
        /// <param name="datosOrden">Datos de la orden de transferencia enviados por la CCE</param>
        /// <param name="datosEncabezado">Datos del encabezado enviados por la CCE</param>
        /// <returns>Retorna datos  de respuesta y autorizacion de la Orden de Transferencia</returns>
        [HttpPost]
        [Route("CT2")]
        [SwaggerOperation(
            Tags = new[] { "END POINT ENTRADA Orden de Transferencia" }, 
            Summary = "Autoriza la orden de transferencia", 
            OperationId = "OrdenTransferenciaEntrada")]
        [SwaggerRequestExample(typeof(EstructuraContenidoCT2), typeof(EstructuraContenidoCT2Example))]
        [SwaggerResponseExample(200, typeof(EstructuraContenidoCT3Example))]
        [EncryptacionResponseBody]
        public async Task<ActionResult<EstructuraContenidoCT3>> OrdenTransferenciaEntrada(
            [FromBody] EstructuraContenidoCT2 datosOrden, [FromHeader] EstructuraEncabezado datosEncabezado)
        {
            _bitacora.Trace("Iniciando proceso de Autorizacion de Orden de Transferencia por Transferencias Interbancarias Inmediatas - CCE.",
                $"Identificador de la Solicitud: {datosEncabezado.IdentificadorSolicitud}");
            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionTransferenciaEntrada.AutorizaOrdenTransferenciaDeCCE(datosOrden.CT2, 
                    datosOrden.codigoValidacionFirma)); 
        }
        
    }
}
