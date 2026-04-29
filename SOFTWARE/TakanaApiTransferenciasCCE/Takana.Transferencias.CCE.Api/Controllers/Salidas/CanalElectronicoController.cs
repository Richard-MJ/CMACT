using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using Takana.Transferencias.CCE.Api.Atributos;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;
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
    [Authorize(Policy = "ModuloAppMovil")]
    [Route("/api/v{version:apiVersion}.0/operacion")]
    public class CanalElectronicoController : BaseController<CanalElectronicoController>
    {
        private readonly IServicioAplicacionCliente _servicioAplicacionCliente;
        private readonly IServicioAplicacionTransaccionOperacion _servicioAplicacionTransaccionOperacion;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="servicioAplicacionCliente"></param>
        /// <param name="servicioAplicacionTransferenciaSalida"></param>
        public CanalElectronicoController(
            IBitacora<CanalElectronicoController> bitacora,
            IServicioAplicacionCliente servicioAplicacionCliente,
            IServicioAplicacionTransaccionOperacion servicioAplicacionTransferenciaSalida) : base(bitacora)
        {
            _servicioAplicacionCliente = servicioAplicacionCliente;
            _servicioAplicacionTransaccionOperacion = servicioAplicacionTransferenciaSalida;
        }

        /// <summary>
        /// Método que obtiene los datos iniciales para el cliente
        /// </summary>
        /// <returns>Resultado de los datos iniciales</returns>
        [HttpGet]
        [Route("obtener-datos-iniciales")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones Transferencia Inmediata CCE" },
            Summary = "Obtiene los datos iniciales para transferencia inmediata",
            OperationId = "ObtenerDatosInicialesTin")]
        public async Task<ActionResult<InicializarCanalElectronicoDTO>> ObtenerDatosIniciales()
        {
            _bitacora.Trace("Iniciando proceso de obtener datos iniciales de Transferencias Inmediatas CCE.");
            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionCliente.ObtenerDatosInicialesCanalElectronico());
        }

        /// <summary>
        /// Método que realiza la consulta de cuenta y obtiene datos del cliente receptor por la CCE
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("consulta-cuenta-receptor")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones Transferencia Inmediata CCE" },
            Summary = "Envia la consulta de cuenta para obtener datos del cliente receptor a la CCE.",
            OperationId = "ConsultaCuentaReceptorTin")]
        [SwaggerRequestExample(typeof(ConsultaCuentaReceptorDTO), typeof(ConsultaCuentaReceptorDTOExample))]
        [SwaggerResponseExample(200, typeof(ResultadoConsultaCuentaCCEExample))]
        public async Task<ActionResult<ResultadoConsultaCuentaCCE>> ConsultaCuentaReceptor([FromBody] ConsultaCuentaReceptorDTO datos)
        {
            _bitacora.Trace("Iniciando proceso de consulta de cuenta receptor de Transferencias Inmediatas CCE.");
            return await InvocarOperacionDesdeServicios(async () => await 
                _servicioAplicacionTransaccionOperacion.ConsultaCuentaReceptor(datos));
        }

        /// <summary>
        /// Calcula la comision de la operacion.
        /// </summary>
        /// <param name="datosComision">Datos de calculo</param>
        /// <returns>Retorna datos calculados de la comision</returns>
        [HttpPost]
        [Route("calcular-montos-totales")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones Transferencia Inmediata CCE" },
            Summary = "Calcula los montos totales de la operacion de la CCE",
            OperationId = "CalcularMontosTotalesTin")]
        [SwaggerRequestExample(typeof(CalculoComisionDTO), typeof(CalculoComisionDTOxample))]
        [SwaggerResponseExample(200, typeof(CalculoComisionRespuestaDTOxample))]
        public async Task<ActionResult<CalculoComisionDTO>> CalcularMontosTotales([FromBody] CalculoComisionDTO datosComision)
        {
            _bitacora.Trace("Iniciando proceso de calculo de montos Totales de Transferencias Inmediatas CCE");
            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionTransaccionOperacion.CalcularMontosTotales(datosComision));
        }

        /// <summary>
        /// Realiza la transferencia para canales electronicos
        /// </summary>
        /// <param name="ordenTransferencia"></param>
        /// <returns>Resultado de la transferencia</returns>
        [HttpPost]
        [Route("realizar-orden-transferencia")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones Transferencia Inmediata CCE" },
            Summary = "Realiza la transferencia de canal electronico",
            OperationId = "RealizarTransferenciaTin")]
        [SwaggerRequestExample(typeof(OrdenTransferenciaCanalElectronicoDTO), typeof(RealizarTransferenciaDTOExample))]
        [SwaggerResponseExample(200, typeof(ResultadoTransferenciaCelularExample))]
        public async Task<ActionResult<ResultadoTransferenciaCanalElectronico>> RealizarOrdenTransferencia(
            [FromBody] OrdenTransferenciaCanalElectronicoDTO ordenTransferencia)
        {
            _bitacora.Trace("Iniciando proceso de realizar la orden de transferencia de Transferencias Inmediatas CCE.");

            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionTransaccionOperacion.RealizarTransferenciaCanalElectronico(ordenTransferencia));
        }

        /// <summary>
        /// Agrega una operaciones frecuente de la Transferencia Inmediata de Canal Electronico
        /// </summary>
        /// <param name="operacion"></param>
        /// <returns>Resultado de la transferencia</returns>
        [HttpPost]
        [Route("agregar-operacion-frecuente")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones Transferencia Inmediata CCE" },
            Summary = "Agrega una operaciones frecuente de la Transferencia Inmediata de Canal Electronico",
            OperationId = "AgregarOperacionFrecuenteTin")]
        [SwaggerRequestExample(typeof(OperacionFrecuenteDTO), typeof(OperacionFrecuenteDTOExample))]
        [SwaggerResponseExample(200, typeof(bool))]
        public async Task<ActionResult<bool>> AgregarOperacionFrecuente(
            [FromBody] OperacionFrecuenteDTO operacion)
        {
            _bitacora.Trace("Iniciando proceso de Agregar Operacion Frecuente de Transferencias Inmediatas CCE.");
            return await InvocarOperacionDesdeServicios(async () =>
            {
                await _servicioAplicacionTransaccionOperacion.AgregarOperacionFrecuente(operacion);
                return true;
            });
        }

        /// <summary>
        /// Enviá un correo electronico de transferencias inmediatas
        /// </summary>
        /// <param name="datos"></param>
        /// <returns>Resultado de la transferencia</returns>
        [HttpPost]
        [Route("enviar-correo-transferencia")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones Transferencia Inmediata CCE" },
            Summary = "Realiza el envio del correo de la transferencia de canal electronico",
            OperationId = "EnviarCorreoTin")]
        [SwaggerRequestExample(typeof(EnviarCorreoDTO), typeof(EnviarCorreoDTOExample))]
        [SwaggerResponseExample(200, typeof(bool))]
        public async Task<ActionResult<bool>> EnviarCorreoTransferencia(
            [FromBody] EnviarCorreoDTO datos)
        {
            _bitacora.Trace("Iniciando proceso de Enviar Correo Electronico de Transferencias Inmediatas CCE.");
            return await InvocarOperacionDesdeServicios(async () =>
            {
                await _servicioAplicacionCliente.EnviarCorreoClienteTransferenciaInmediata(
                    datos.NumeroMovimiento, datos.NombreDocumentoTerminos, datos.DocumentoTerminos, datos.CorreoDestinatario);
                return true;
            });
        }

    }
}