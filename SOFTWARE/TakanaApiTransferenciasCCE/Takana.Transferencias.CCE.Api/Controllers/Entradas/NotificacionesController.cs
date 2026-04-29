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
    [EncryptacionResponseBody]
    [Route("ssv2/payment-api/v{version:apiVersion}.0")]
    public class NotificacionesController : BaseController<NotificacionesController>
    {
        private readonly IServicioAplicacionTransferenciaEntrada _servicioAplicacionTransferenciaEntrada;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="servicioAplicacionTransferenciaEntrada"></param>                                
        public NotificacionesController(
            IBitacora<NotificacionesController> bitacora,
            IServicioAplicacionTransferenciaEntrada servicioAplicacionTransferenciaEntrada) : base(bitacora)
        {
            _servicioAplicacionTransferenciaEntrada = servicioAplicacionTransferenciaEntrada;
        }

        /// <summary>
        /// Metodo que procesa la operación de Mensaje de Notificación 971 de la Camara de Compensación Electronica
        /// </summary>
        /// <param name="datosNotificacion">Datos de mensaje de notificación enviada por la CCE</param>
        /// <param name="datosEncabezado">Datos del encabezado</param>
        /// <returns>Retorna booleano true</returns>
        [HttpPost]
        [Route("SNM971")]
        [SwaggerOperation(
            Tags = new[] { "END POINT ENTRADA Notificación" }, 
            Summary = "Notificación de Cambio de Estado del Sistema", 
            OperationId = "Notificacion971Entrada")]
        [SwaggerRequestExample(typeof(EstructuraContenidoSNM971), typeof(EstructuraContenidoSNM971Example))]
        public async Task Notificacion971Entrada(
            [FromBody] EstructuraContenidoSNM971 datosNotificacion, [FromHeader] EstructuraEncabezado datosEncabezado)
        {
            _bitacora.Trace("Iniciando proceso de Notificación 971 por Transferencias Interbancarias Inmediatas - CCE.",
                $"Identificador de la Solicitud: {datosEncabezado.IdentificadorSolicitud}");
            await _servicioAplicacionTransferenciaEntrada.Notificacion971DeCCE(datosNotificacion.SNM971);       
        }

        /// <summary>
        /// Metodo que procesa la operación de Mensaje de Notificación 972 de la Camara de Compensación Electronica
        /// </summary>
        /// <param name="datosNotificacion">Datos de mensaje de notificación enviada por la CCE</param>
        /// <param name="datosEncabezado">Datos del encabezado</param>
        /// <returns>Retorna booleano true</returns>
        [HttpPost]
        [Route("SNM972")]
        [SwaggerOperation(
            Tags = new[] { "END POINT ENTRADA Notificación" }, 
            Summary = "Notificación de Cambio de estado de Bloqueo de la Entidad", 
            OperationId = "Notificacion972Entrada")]
        [SwaggerRequestExample(typeof(EstructuraContenidoSNM972), typeof(EstructuraContenidoSNM972Example))]
        public async Task Notificacion972Entrada(
            [FromBody] EstructuraContenidoSNM972 datosNotificacion, [FromHeader] EstructuraEncabezado datosEncabezado)
        {
            _bitacora.Trace("Iniciando proceso de Notificación 972 por Transferencias Interbancarias Inmediatas - CCE.",
                $"Identificador de la Solicitud: {datosEncabezado.IdentificadorSolicitud}");
            await _servicioAplicacionTransferenciaEntrada.Notificacion972DeCCE(datosNotificacion.SNM972);        
        }

        /// <summary>
        /// Metodo que procesa la operación de Mensaje de Notificación 981 de la Camara de Compensación Electronica
        /// </summary>
        /// <param name="datosNotificacion">Datos de mensaje de notificación enviada por la CCE</param>
        /// <param name="datosEncabezado">Datos del encabezado</param>
        /// <returns>Retorna booleano true</returns>
        [HttpPost]
        [Route("SNM981")]
        [SwaggerOperation(
            Tags = new[] { "END POINT ENTRADA Notificación" }, 
            Summary = "Notificación de Cambio de Estado del Sistema", 
            OperationId = "Notificacion981Entrada")]
        [SwaggerRequestExample(typeof(EstructuraContenidoSNM981), typeof(EstructuraContenidoSNM981Example))]
        public async Task Notificacion981Entrada(
            [FromBody] EstructuraContenidoSNM981 datosNotificacion, [FromHeader] EstructuraEncabezado datosEncabezado)
        {
            _bitacora.Trace("Iniciando proceso de Notificación 981 por Transferencias Interbancarias Inmediatas - CCE.",
                $"Identificador de la Solicitud: {datosEncabezado.IdentificadorSolicitud}");
            await _servicioAplicacionTransferenciaEntrada.Notificacion981DeCCE(datosNotificacion.SNM981);
        }

        /// <summary>
        /// Metodo que procesa la operación de Mensaje de Notificación 982 de la Camara de Compensación Electronica
        /// </summary>
        /// <param name="datosNotificacion">Datos de mensaje de notificación enviada por la CCE</param>
        /// <param name="datosEncabezado">Datos del encabezado</param>
        /// <returns>Retorna booleano true</returns>
        [HttpPost]
        [Route("SNM982")]
        [SwaggerOperation(
            Tags = new[] { "END POINT ENTRADA Notificación" }, 
            Summary = "Notificación de Cambio de Estado de Logueo de la Entidad", 
            OperationId = "Notificacion982Entrada")]
        [SwaggerRequestExample(typeof(EstructuraContenidoSNM982), typeof(EstructuraContenidoSNM982Example))]
        public async Task Notificacion982Entrada(
            [FromBody] EstructuraContenidoSNM982 datosNotificacion, [FromHeader] EstructuraEncabezado datosEncabezado)
        {
            _bitacora.Trace("Iniciando proceso de Notificación 982 por Transferencias Interbancarias Inmediatas - CCE.",
                $"Identificador de la Solicitud: {datosEncabezado.IdentificadorSolicitud}");
            await _servicioAplicacionTransferenciaEntrada.Notificacion982DeCCE(datosNotificacion.SNM982);
        }

        /// <summary>
        /// Metodo que procesa la operación de Mensaje de Notificación 993 de la Camara de Compensación Electronica
        /// </summary>
        /// <param name="datosNotificacion">Datos de mensaje de notificación enviada por la CCE</param>
        /// <param name="datosEncabezado">Datos del encabezado</param>
        /// <returns>Retorna booleano true</returns>
        [HttpPost]
        [Route("SNM993")]
        [SwaggerOperation(
            Tags = new[] { "END POINT ENTRADA Notificación" }, 
            Summary = "Notificación de Ampliación de Garantías", 
            OperationId = "Notificacion993Entrada")]
        [SwaggerRequestExample(typeof(EstructuraContenidoSNM993), typeof(EstructuraContenidoSNM993Example))]
        public async Task Notificacion993Entrada(
            [FromBody] EstructuraContenidoSNM993 datosNotificacion, [FromHeader] EstructuraEncabezado datosEncabezado)
        {
            _bitacora.Trace("Iniciando proceso de Notificación 993 por Transferencias Interbancarias Inmediatas - CCE.",
                $"Identificador de la Solicitud: {datosEncabezado.IdentificadorSolicitud}");
            await _servicioAplicacionTransferenciaEntrada.Notificacion993DeCCE(datosNotificacion.SNM993);
        }

        /// <summary>
        /// Metodo que procesa la operación de Mensaje de Notificación 998 de la Camara de Compensación Electronica
        /// </summary>
        /// <param name="datosNotificacion">Datos de mensaje de notificación enviada por la CCE</param>
        /// <param name="datosEncabezado">Datos del encabezado</param>
        /// <returns>Retorna booleano true</returns>
        [HttpPost]
        [Route("SNM998")]
        [SwaggerOperation(
            Tags = new[] { "END POINT ENTRADA Notificación" }, 
            Summary = "Notificación de Cambio en el Saldo Operativo", 
            OperationId = "Notificacion998Entrada")]
        [SwaggerRequestExample(typeof(EstructuraContenidoSNM998), typeof(EstructuraContenidoSNM998Example))]
        public async Task Notificacion998Entrada(
            [FromBody] EstructuraContenidoSNM998 datosNotificacion, [FromHeader] EstructuraEncabezado datosEncabezado)
        {
            _bitacora.Trace("Iniciando proceso de Notificación 998 por Transferencias Interbancarias Inmediatas - CCE.", 
                $"Identificador de la Solicitud: {datosEncabezado.IdentificadorSolicitud}");
            await _servicioAplicacionTransferenciaEntrada.Notificacion998DeCCE(datosNotificacion.SNM998);
        }

        /// <summary>
        /// Metodo que procesa la operación de Mensaje de Notificación 999 de la Camara de Compensación Electronica
        /// </summary>
        /// <param name="datosNotificacion">Datos de mensaje de notificación enviada por la CCE</param>
        /// <param name="datosEncabezado">Datos del encabezado</param>
        /// <returns>Retorna booleano true</returns>
        [HttpPost]
        [Route("SNM999")]
        [SwaggerOperation(
            Tags = new[] { "END POINT ENTRADA Notificación" }, 
            Summary = "Notificación de Resumen de Ciclo Operativo", 
            OperationId = "Notificacion999Entrada")]
        [SwaggerRequestExample(typeof(EstructuraContenidoSNM999), typeof(EstructuraContenidoSNM999Example))]
        public async Task Notificacion999Entrada(
            [FromBody] EstructuraContenidoSNM999 datosNotificacion, [FromHeader] EstructuraEncabezado datosEncabezado)
        {
            _bitacora.Trace("Iniciando proceso de Notificación 999 por Transferencias Interbancarias Inmediatas - CCE.",
                $"Identificador de la Solicitud: {datosEncabezado.IdentificadorSolicitud}");
            await _servicioAplicacionTransferenciaEntrada.Notificacion999DeCCE(datosNotificacion.SNM999);
        }
    }
}
