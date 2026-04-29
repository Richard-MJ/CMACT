using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Takana.Transferencias.CCE.Api.Atributos;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using static Takana.Transferencias.CCE.Api.Common.SwaggerExamplesDTO;

namespace Takana.Transferencias.CCE.Api.Controllers.Salidas
{
    [ApiController]
    [ApiVersion("3.5")]
    [Route("api/transferencias-cce/v{version:apiVersion}.0")]
    public class SolicitudEstadoPagoController : BaseController<SolicitudEstadoPagoController>
    {
        private readonly static SemaphoreSlim _semaforo = new SemaphoreSlim(1);
        private readonly IServicioAplicacionTransferenciaEntrada _servicioAplicacionTransferenciaEntrada;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="servicioAplicacionTransferenciaEntrada"></param>
        public SolicitudEstadoPagoController(
            IBitacora<SolicitudEstadoPagoController> bitacora,
            IServicioAplicacionTransferenciaEntrada servicioAplicacionTransferenciaEntrada) : base(bitacora)
        {
            _servicioAplicacionTransferenciaEntrada = servicioAplicacionTransferenciaEntrada;
        }

        /// <summary>
        /// Metodo que procesa la operación de Solicitud de Pago Automatico
        /// </summary>
        /// <param name="identificadorInstruccion">Identificador Unico generado por la CCE</param>
        /// <returns>Retorna true</returns>
        [HttpPost]
        [Route("PSR1")]
        [SwaggerOperation(
            Tags = new[] { "END POINT ENTRADA Solicitud de Estado de Pago" },
            Summary = "Procesa solucion para transacciones pendientes o confimadas de manera automatica",
            OperationId = "PRSAutomaticoEntrada")]
        [SwaggerRequestExample(typeof(string), typeof(stringExample))]
        public async Task<ActionResult<bool>> AutomaticoSolicitudEstadoPago([FromBody] string identificadorInstruccion)
        {
            await _semaforo.WaitAsync();
            try
            {
                _bitacora.Trace("Iniciando proceso de Solicitud de Estado de Pago Automatico por Transferencias Interbancarias Inmediatas - CCE.");
                return await InvocarOperacionDesdeServicios(async () => await
                    _servicioAplicacionTransferenciaEntrada.SolicitudEstadoPagoParaCCE(identificadorInstruccion, true));
            }
            finally
            {
                _semaforo.Release();
            }
        }

        /// <summary>
        /// Metodo que procesa la operación de Solicitud de Pago Manual
        /// </summary>
        /// <param name="identificadorInstruccion">Identificador Unico generado por la CCE</param>
        /// <returns>Retorna true</returns>
        [HttpPost]
        [GenerarSesion]
        [Impersonalizacion]
        [Route("solicitud-estado-pago")]
        [Authorize(Policy = "ModuloTesoreria")]
        [SwaggerOperation(
            Tags = new[] { "END POINT ENTRADA Solicitud de Estado de Pago" },
            Summary = "Procesa solucion para transacciones pendientes o confimadas de manera manual",
            OperationId = "PRSManualEntrada")]
        public async Task<ActionResult<bool>> ManualSolicitudEstadoPago([FromBody] string identificadorInstruccion)
        {
            await _semaforo.WaitAsync();
            try
            {
                _bitacora.Trace("Iniciando proceso de Solicitud de Estado de Pago Manual por Transferencias Interbancarias Inmediatas - CCE.");
                return await InvocarOperacionDesdeServicios(async () => await
                    _servicioAplicacionTransferenciaEntrada.SolicitudEstadoPagoParaCCE(identificadorInstruccion, false));
            }
            finally
            {
                _semaforo.Release();
            }
        }
    }
}