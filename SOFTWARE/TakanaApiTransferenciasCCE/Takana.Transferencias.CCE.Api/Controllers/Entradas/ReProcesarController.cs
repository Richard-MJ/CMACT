using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Takana.Transferencias.CCE.Api.Atributos;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using Takana.Transferencias.CCE.Api.Common.ReprocesarTransaccion;
using static Takana.Transferencias.CCE.Api.Common.SwaggerExamplesDTO;

namespace Takana.Transferencias.CCE.Api.Controllers.Entradas
{
    /// <summary>
    /// Clase que recibe el requermiento de Consulta Cuenta
    /// </summary>
    [ApiController]
    [GenerarSesion]
    [Impersonalizacion]
    [ApiVersion("3.5")]
    [Authorize(Policy = "ModuloTesoreria")]
    [Route("api/transferencias-cce/v{version:apiVersion}.0")]
    public class ReProcesarController : BaseController<ReProcesarController>
        {
        private readonly IServicioAplicacionTransferenciaEntrada _servicioAplicacionTransferenciaEntrada;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="servicioAplicacionTransferenciaEntrada"></param>
        public ReProcesarController(
            IBitacora<ReProcesarController> bitacora,
            IServicioAplicacionTransferenciaEntrada servicioAplicacionTransferenciaEntrada) : base(bitacora)
        {
             _servicioAplicacionTransferenciaEntrada = servicioAplicacionTransferenciaEntrada;
        }

        /// <summary>
        /// Metodo que procesa la operación de reprocesamiento de transacciones 
        /// </summary>
        /// <param name="transacciones">transacciones en proceso</param>
        [HttpPost]
        [Route("reprocesar")]
        [SwaggerOperation(
            Tags = new[] { "END POINT ENTRADA: Reprocesar Transacciones" }, 
            Summary = "Reprocesa Transacciones de Tin Inmediata", 
            OperationId = "DevolucionEntrada")]
        [SwaggerRequestExample(typeof(List<ReprocesarTransaccionDTO>), typeof(ReprocesarTransaccionDTOExample))]
        public async Task ReprocesarTransaccion([FromBody] List<ReprocesarTransaccionDTO> transacciones)
        {
            _bitacora.Trace("Iniciando el Reprocesamiento de transacciones por Transferencias Interbancarias Inmediatas - CCE.");
            await _servicioAplicacionTransferenciaEntrada.ReprocesarTransaccion(transacciones);
        }
    }
}
