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
    public class ConfirmacionesController : BaseController<ConfirmacionesController>
    {
        private readonly static SemaphoreSlim _semaforo = new SemaphoreSlim(1);
        private readonly IServicioAplicacionTransferenciaEntrada _servicioAplicacionTransferenciaEntrada;

        /// <summary>
        /// Construtor 
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="servicioAplicacionTransferenciaEntrada"></param>
        public ConfirmacionesController(IBitacora<ConfirmacionesController> bitacora,
            IServicioAplicacionTransferenciaEntrada servicioAplicacionTransferenciaEntrada) : base(bitacora)
        {
            _servicioAplicacionTransferenciaEntrada = servicioAplicacionTransferenciaEntrada;
        }


        /// <summary>
        /// Metodo que procesa la operación de Consulta Cuenta de la Camara de Compensación Electronica
        /// </summary>
        /// <param name="datosConfirmacion">Datos de confirmacion de la orden de transferencia enviados por la CCE</param>
        /// <param name="datosEncabezado">Datos del encabezado enviados por la CCE</param>
        /// <returns>Retorna status 200</returns>
        [HttpPost]
        [Route("ssv2/payment-api/v{version:apiVersion}.0/CT5")]
        [SwaggerOperation(
            Tags = new[] { "END POINT ENTRADA Confirmación de orden de transferencia" }, 
            Summary = "Mensaje de confirmacion de la orden de transferencia", 
            OperationId = "ConfirmacionOrdenTransferenciaEntrada")]
        [SwaggerRequestExample(typeof(EstructuraContenidoCT5), typeof(EstructuraContenidoCT5Example))]
        [EncryptacionResponseBody]
        public async Task ConfirmacionOrdenTransferenciaEntrada(
            [FromBody] EstructuraContenidoCT5 datosConfirmacion, [FromHeader] EstructuraEncabezado datosEncabezado)
        {
            _bitacora.Trace("Iniciando proceso de Confirmacion (Abono) por Transferencias Interbancarias Inmediatas - CCE.",
                $"Identificador de la Solicitud: {datosEncabezado.IdentificadorSolicitud}");
            await _servicioAplicacionTransferenciaEntrada.ConfirmaOrdenTransferenciaDeCCE(datosConfirmacion.CT5, 
                    datosConfirmacion.codigoValidacionFirma); 
        }

        /// <summary>
        /// Metodo que procesa la operación de Consulta Cuenta de la Camara de Compensación Electronica
        /// </summary>
        /// <param name="identificadorInstruccion">Identificador Unico generado por la CCE</param>
        /// <returns>Retorna</returns>
        [HttpPost]
        [Route("api/transferencias-cce/v{version:apiVersion}.0/procesarTransferencia")]
        [SwaggerOperation(
            Tags = new[] { "END POINT ENTRADA Confirmación de orden de transferencia" }, 
            Summary = "Procesa la transacion de la orden de transferencia", 
            OperationId = "procesarTransferenciaEntrada")]
        [SwaggerRequestExample(typeof(string), typeof(stringExample))]
        [SwaggerResponseExample(200, typeof(bool))]
        public async Task<ActionResult<bool>> ProcesarTransferenciaEntrante(
            [FromBody] string identificadorInstruccion)
        {
            await _semaforo.WaitAsync();
            try
            {
                _bitacora.Trace("Iniciando Procesamiento de Transaccion de Transferencias Interbancarias Inmediatas - CCE.");
                return await InvocarOperacionDesdeServicios(async () => await
                    _servicioAplicacionTransferenciaEntrada.ProcesarTransferenciaEntranteCCE(identificadorInstruccion));
            }
            finally
            {
                _semaforo.Release();
            }
        }
    }

}
