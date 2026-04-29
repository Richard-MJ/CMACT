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
    public class CancelacionesController : BaseController<CancelacionesController>
    {
        private readonly static SemaphoreSlim semaphore = new SemaphoreSlim(1);
        private readonly IServicioAplicacionTransferenciaEntrada _servicioAplicacionTransferenciaEntrada;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="servicioAplicacionTransferenciaEntrada"></param>
        public CancelacionesController(
            IBitacora<CancelacionesController> bitacora,
            IServicioAplicacionTransferenciaEntrada servicioAplicacionTransferenciaEntrada) : base(bitacora)
        {
            _servicioAplicacionTransferenciaEntrada = servicioAplicacionTransferenciaEntrada;
        }

        /// <summary>
        /// Metodo que procesa la operación de Cancelacion de una Orden de Transferencia de la Camara de Compensación Electronica
        /// </summary>
        /// <param name="datosCancelacion">Datos de cancelacion enviados por la CCE</param>
        /// <param name="datosEncabezado">Datos del encabezado enviados por la CCE</param>
        /// <returns>Retorna respuesta de estructura de cancelacion</returns>
        [HttpPost]
        [Route("ssv2/payment-api/v{version:apiVersion}.0/CTC1")]
        [SwaggerOperation(
            Tags = new[] { "END POINT ENTRADA Cancelacion orden de transferencia" }, 
            Summary = "Procesa la cancelacion de la transaccion de orden de transferencia", 
            OperationId = "CancelarOrdenTransferenciaEntrada")]
        [SwaggerRequestExample(typeof(EstructuraContenidoCTC1), typeof(EstructuraContenidoCTC1Example))]
        [SwaggerResponseExample(200, typeof(EstructuraContenidoCTC2Example))]
        [EncryptacionResponseBody]
        public async Task<ActionResult<EstructuraContenidoCTC2>> CancelacionOrdenTransferenciaEntrada(
            [FromBody] EstructuraContenidoCTC1 datosCancelacion, [FromHeader] EstructuraEncabezado datosEncabezado)
        {
            _bitacora.Trace("Iniciando proceso de cancelacion por Transferencias Interbancarias Inmediatas - CCE.",
                $"Identificador de la Solicitud: {datosEncabezado.IdentificadorSolicitud}");
            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionTransferenciaEntrada.CancelacionOrdenTransferenciaDeCCE(datosCancelacion.CTC1,
                    datosCancelacion.codigoValidacionFirma));        
        }

        /// <summary>
        /// Metodo que procesa la operación de Consulta Cuenta de la Camara de Compensación Electronica
        /// </summary>
        /// <param name="identificadorInstruccion">Identificador Unico generado por la CCE</param>
        /// <returns>Retorna</returns>
        [HttpPost]
        [Route("api/transferencias-cce/v{version:apiVersion}.0/procesarRechazoTransferencia")]
        [SwaggerOperation(
            Tags = new[] { "END POINT ENTRADA Cancelacion orden de transferencia" }, 
            Summary = "Procesa el cancelacion de la transaccion de orden de transferencia y procesa la comisión", 
            OperationId = "CancelarOrdenTransferenciaComisionEntrada")]
        [SwaggerRequestExample(typeof(string), typeof(stringExample))]
        [SwaggerResponseExample(200, typeof(bool))]
        public async Task<ActionResult<bool>> ProcesarRechazoTransferenciaEntrante(
            [FromBody] string identificadorInstruccion)
        {
            await semaphore.WaitAsync();
            try
            {
                _bitacora.Trace("Iniciando Procesamiento de Rechazo de Transaccion de Transferencias Interbancarias Inmediatas - CCE.");
                return await InvocarOperacionDesdeServicios(async () => await
                    _servicioAplicacionTransferenciaEntrada.ProcesarRechazoTransferenciaEntranteCCE(identificadorInstruccion));
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
