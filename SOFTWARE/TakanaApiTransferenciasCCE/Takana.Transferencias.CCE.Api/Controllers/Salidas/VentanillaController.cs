using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using Takana.Transferencias.CCE.Api.Atributos;
using Takana.Transferencias.CCE.Api.Common.DTOs.CL;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;
using static Takana.Transferencias.CCE.Api.Common.SwaggerExamplesDTO;

namespace Takana.Transferencias.CCE.Api.Controllers.Salidas
{
    /// <summary>
    /// Clase que redirecciona los datos recibidor del canal hacia los servicios requeridos
    /// </summary>
    [ApiController]
    [GenerarSesion]
    [Impersonalizacion]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}")]
    [Authorize(Policy = "ModuloVentanilla")]
    public class VentanillaController : BaseController<VentanillaController>
    {
        private readonly IServicioAplicacionCliente _servicioAplicacionCliente;
        private readonly IServicioAplicacionTransaccionOperacion _servicioAplicacionTransaccionOperacion;

        /// <summary>
        /// Método constructor
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="servicioAplicacionTransaccionOperacion"></param>
        /// <param name="servicioAplicacionCliente"></param>
        public VentanillaController(
            IBitacora<VentanillaController> bitacora,
            IServicioAplicacionTransaccionOperacion servicioAplicacionTransaccionOperacion,
            IServicioAplicacionCliente servicioAplicacionCliente) : base(bitacora)
        {
            _servicioAplicacionCliente = servicioAplicacionCliente;
            _servicioAplicacionTransaccionOperacion = servicioAplicacionTransaccionOperacion;
        }

        /// <summary>
        /// Metodo que obtiene los datos para inicializar la ventanilla
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("inicializar")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones Ventanilla" },
            Summary = "Inicializa los datos de ventanilla",
            OperationId = "Inicializar")]
        public async Task<ActionResult<InicializarVentanillaDTO>> ObtenerDatosIniciales()
        {
            _bitacora.Trace("Iniciando proceso de inicializacion para canal ventanilla");
            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionCliente.ObtenerDatosInicialesVentanilla());
        }

        /// <summary>
        /// Obtiene los datos del cliente originante
        /// </summary>
        /// <param name="numeroCuenta">Numero cuenta del cliente originante</param>
        /// <returns>Retorna la cuenta efectivo</returns>
        [HttpPost]
        [Route("obtener_cliente_originante")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones Ventanilla" },
            Summary = "Obtiene los datos del cliente originante",
            OperationId = "ObtenerDatosClienteOriginante")]
        [SwaggerRequestExample(typeof(string), typeof(ClienteOriginanteDTOExample))]
        [SwaggerResponseExample(200, typeof(RespuetaConsultaCuentaDTOExample))]
        public async Task<ActionResult<CuentaEfectivoDTO>> ObtenerDatosClienteOriginante(
            [FromBody] string numeroCuenta)
        {
            _bitacora.Trace("Iniciando proceso de Obtener cuente de cliente originante");
            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionCliente.ObtenerDatosCuentaOrigen(numeroCuenta));
        }

        /// <summary>
        /// Obtiene los datos del cliente Receptor
        /// </summary>
        /// <param name="datosConsulta">Datos de la consulta de cuenta de operacion</param>
        /// <returns>Datos del cliente originante y cliente receptor</returns>
        [HttpPost]
        [Route("obtener_cliente_receptor")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones Ventanilla" },
            Summary = "Obtiene los datos del cliente Receptor",
            OperationId = "ConsultaCuentaReceptorPorCCI")]
        [SwaggerRequestExample(typeof(ConsultaCuentaOperacionDTO), typeof(ConsultaCuentaOperacionVDTOExample))]
        [SwaggerResponseExample(200, typeof(ResultadoConsultaCuentaCCEExample))]
        public async Task<ActionResult<ResultadoConsultaCuentaCCE>> ConsultaCuentaReceptorPorCCE(
            [FromBody] ConsultaCuentaOperacionDTO datosConsulta)
        {
            _bitacora.Trace("Iniciando proceso de consulta cuenta Receptor");
            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionTransaccionOperacion.ConsultaCuentaReceptorPorCCE(datosConsulta));
        }

        /// <summary>
        /// Calcula la comision de la operacion.
        /// </summary>
        /// <param name="datosComision">Datos de calculo</param>
        /// <returns>Retorna datos calculados de la comision</returns>
        [HttpPost]
        [Route("cacular_comision")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones Ventanilla" },
            Summary = "Calcula la comision de la operacion.",
            OperationId = "CalcularComision")]
        [SwaggerRequestExample(typeof(CalculoComisionDTO), typeof(CalculoComisionDTOxample))]
        [SwaggerResponseExample(200, typeof(CalculoComisionRespuestaDTOxample))]
        public async Task<ActionResult<CalculoComisionDTO>> CalcularMontosTotales([FromBody] CalculoComisionDTO datosComision)
        {
            _bitacora.Trace("Iniciando proceso de calculo de Totales");
            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionTransaccionOperacion.CalcularMontosTotales(datosComision));
        }

        /// <summary>
        /// Obtener detalles validados para la operacion
        /// </summary>
        /// <param name="detalleTransferencia">Datos de la operacion a validar</param>
        /// <returns>True si pasa las validaciones</returns>
        [HttpPost]
        [Route("validar_transaccion")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones Ventanilla" },
            Summary = "Obtener detalles validados para la operacion",
            OperationId = "ObtenerDetallesValidadosMontosYLimites")]
        [SwaggerRequestExample(typeof(CalculoComisionDTO), typeof(CalculoComisionDTOxample))]
        [SwaggerResponseExample(200, typeof(CalculoComisionRespuestaDTOxample))]
        public async Task<ActionResult<string>> ObtenerDetallesValidadosMontosYLimites([FromBody] ValidarTransaccionInmediataDTO detalleTransferencia)
        {
            _bitacora.Trace("Iniciando proceso de validacion de transaccion");
            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionTransaccionOperacion.ObtenerDetallesValidadosMontosYLimites(detalleTransferencia));
        }

        /// <summary>
        /// Realizar ek debito de la operacion y envia la orden de transferencia
        /// </summary>
        /// <param name="ordenTransferencia">Datos e la operacion</param>
        /// <returns>Retorna datos si la operacion sale biem</returns>
        [HttpPost]
        [Route("realizar_transferencia")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones Ventanilla" },
            Summary = "Realizar ek debito de la operacion y envia la orden de transferencia",
            OperationId = "RealizarTransferencia")]
        [SwaggerRequestExample(typeof(OrdenTransferenciaVentanillaDTO), typeof(TransferenciaVentanillaExample))]
        [SwaggerResponseExample(200, typeof(TransferenciaVentanillarRespuestaExample))]
        public async Task<ActionResult<string>> RealizarTransferencia([FromBody] 
            OrdenTransferenciaVentanillaDTO ordenTransferencia)
        {
            _bitacora.Trace("Iniciando proceso de RealizarTransferencia");
            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionTransaccionOperacion.RealizarTransferenciaVentanilla(ordenTransferencia));
        }
    }
}