using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Takana.Transferencias.CCE.Api.Atributos;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
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
    [Route("/api/v{version:apiVersion}")]
    [Authorize(Policy = "ModuloAppMovil")]
    public class InteroperabilidadController : BaseController<InteroperabilidadController>
    {
        private readonly IServicioAplicacionInteroperabilidad _servicioAplicacionInteroperabilidad;

        /// <summary>
        /// Método constructor
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="interoperabilidadServicio"></param>
        public InteroperabilidadController(
            IBitacora<InteroperabilidadController> bitacora,
            IServicioAplicacionInteroperabilidad interoperabilidadServicio) : base(bitacora)
        {
            _servicioAplicacionInteroperabilidad = interoperabilidadServicio;
        }
        /// <summary>
        /// Consulta cuenta del cliente originante
        /// </summary>
        /// <param name="consulta"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("operacion/pago_celular/consulta_cuenta_originante")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones Interoperabilidad CCE" },
            Summary = "Por en numero de cuenta del cliente originante se obtiene sus datos si pasan las validaciones necesarios",
            OperationId = "ConsultaCuentaOriginante")]
        [SwaggerRequestExample(typeof(ConsultaCuentaOriginanteDTO), typeof(ConsultaCuentaOriginanteDTOExample))]
        [SwaggerResponseExample(200, typeof(RespuetaConsultaCuentaDTOExample))]
        public async Task<ActionResult<RespuetaConsultaCuentaDTO>> ConsultaCuentaOriginante(
            [FromBody] ConsultaCuentaOriginanteDTO consulta)
        {
            _bitacora.Trace("Iniciando proceso de Consulta cuenta originante para Interoperabilidad.");
            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionInteroperabilidad.ObtenerDatosClienteOriginante(consulta.NumeroCuenta));
        }

        /// <summary>
        /// Consulta la cuenta del cliente receptor por numero de celular
        /// </summary>
        /// <param name="consulta">Datos necesarios para la consulta</param>
        /// <returns>Datos necesarios para realizar la transferencia</returns>
        [HttpPost]
        [Route("operacion/pago_celular/consulta_cuenta_receptor")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones Interoperabilidad CCE" },
            Summary = "Por el numero de celular del receptor se obtienen los datos necesarios para la transferencia",
            OperationId = "ConsultaCuentaReceptorCelular")]
        [SwaggerRequestExample(typeof(ConsultaCuentaCelularDTO), typeof(RespuetaConsultaCuentaVentanillaDTOExample))]
        [SwaggerResponseExample(200, typeof(ResultadoConsultaCuentaInteroperabilidadDTOExample))]
        public async Task<ActionResult<ResultadoConsultaCuentaInteroperabilidadDTO>> ConsultaCuentaReceptorPorCelular(
            [FromBody] ConsultaCuentaCelularDTO consulta)
        {
            _bitacora.Trace("Iniciando proceso de Consulta cuenta receptor para Interoperabilidad.");
            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionInteroperabilidad.ConsultaCuentaReceptorPorCelular(consulta));
        }

        /// <summary>
        /// Consulta la cuenta del cliente receptor por su QR
        /// </summary>
        /// <param name="datosConsulta">Datos necesarios para la consulta</param>
        /// <returns>Datos necesarios para realizar la transferencia</returns>
        [HttpPost]
        [Route("operacion/pago_qr/consulta_cuenta_qr")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones Interoperabilidad CCE" },
            Summary = "Por el QR del receptor se obtienen los datos necesarios para la transferencia",
            OperationId = "ConsultaCuentaCompletaQR")]
        [SwaggerRequestExample(typeof(ConsultaCuentaCompletaQRDTO), typeof(ConsultaCuentaCompletaQRDTOExample))]
        [SwaggerResponseExample(200, typeof(RespuestaConsultaCompletaQRExample))]
        public async Task<ActionResult<RespuestaConsultaCompletaQR>> ConsultaCuentaCompletaQR(
            ConsultaCuentaCompletaQRDTO datosConsulta)
        {
            _bitacora.Trace("Iniciando proceso de Consulta cuenta completa QR del receptor para interoperabilidad.");
            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionInteroperabilidad.ConsultarCuentaReceptorPorQR(datosConsulta));
        }

        /// <summary>
        /// Calcula los totales de la operacion.
        /// </summary>
        /// <param name="calcular">Datos necesarios para el calculo</param>
        /// <returns>Totales como itf, comision y monto total</returns>
        [HttpPost]
        [Route("operacion/calcular_totales")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones Interoperabilidad CCE" },
            Summary = "Calcula el itf, comision y monto total segun la configuracion de comisiones",
            OperationId = "CalcularMontosTotales")]
        [SwaggerRequestExample(typeof(CalculoComisionDTO), typeof(CalculoComisionDTOExample))]
        [SwaggerResponseExample(200, typeof(ResultadoCalculoMontoExample))]
        public async Task<ActionResult<ResultadoCalculoMonto>> CalcularMontosTotales(
            [FromBody] CalculoComisionDTO calcular)
        {
            _bitacora.Trace("Iniciando proceso de calculo de totales para Interoperabilidad");
            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionInteroperabilidad.CalcularMontosTotales(calcular));
        }

        /// <summary>
        /// Realiza la transferencia para interoperabilidad
        /// </summary>
        /// <param name="ordenTransferencia"></param>
        /// <returns>Resultado de la transferencia</returns>
        [HttpPost]
        [Route("operacion/realizar_transferencia")]
        [SwaggerOperation(
            Tags = new[] { "END POINT: Operaciones Interoperabilidad CCE" },
            Summary = "Realiza la transferencia de interoperabilidad",
            OperationId = "RealizarTransferencia")]
        [SwaggerRequestExample(typeof(OrdenTransferenciaCanalElectronicoDTO), typeof(RealizarTransferenciaDTOExample))]
        [SwaggerResponseExample(200, typeof(ResultadoTransferenciaCelularExample))]
        public async Task<ActionResult<ResultadoTransferenciaCanalElectronico>> RealizarTransferencia(
            [FromBody] OrdenTransferenciaCanalElectronicoDTO ordenTransferencia)
        {
            _bitacora.Trace("Iniciando proceso de Realizar transferencia para interoperabilidad.");
            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionInteroperabilidad.RealizarTransferenciaInteroperabilidad(ordenTransferencia));
        }
    }
}