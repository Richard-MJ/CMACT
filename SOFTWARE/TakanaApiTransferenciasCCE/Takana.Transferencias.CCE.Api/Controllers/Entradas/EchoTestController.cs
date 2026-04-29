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
    [Route("ssv2/payment-api/v{version:apiVersion}.0")]
    public class EchoTestController : BaseController<EchoTestController>
    {
        private readonly IServicioAplicacionTransferenciaEntrada _servicioAplicacionTransferenciaEntrada;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="servicioAplicacionTransferenciaEntrada"></param>
        public EchoTestController(
            IBitacora<EchoTestController> bitacora,
            IServicioAplicacionTransferenciaEntrada servicioAplicacionTransferenciaEntrada) : base(bitacora)
        {
            _servicioAplicacionTransferenciaEntrada = servicioAplicacionTransferenciaEntrada;
        }

        /// <summary>
        /// Metodo que procesa la operación de Echo Test de la Camara de Compensación Electronica
        /// </summary>
        /// <param name="datosEchoTest">Datos de Echo Test enviados por la CCE</param>
        /// <param name="datosEncabezado"></param>
        /// <returns>Retorna estructura de echo test de respuesta</returns>
        [HttpPost]
        [Route("ET1")]
        [SwaggerOperation(
            Tags = new[] { "END POINT ENTRADA Echo Test" }, 
            Summary = "Comprueba la conexión", 
            OperationId = "EchotestEntrada")]
        [SwaggerRequestExample(typeof(EstructuraContenidoET1), typeof(EstructuraContenidoET1Example))]
        [SwaggerResponseExample(200, typeof(EstructuraContenidoET2Example))]
        [EncryptacionResponseBody]
        public async Task<ActionResult<EstructuraContenidoET2>> EchoTestEntrada(
            [FromBody] EstructuraContenidoET1 datosEchoTest, [FromHeader] EstructuraEncabezado datosEncabezado)
        {
            _bitacora.Trace("Iniciando proceso de Echo Test por Transferencias Interbancarias Inmediatas - CCE.",
                $"Identificador de la Solicitud: {datosEncabezado.IdentificadorSolicitud}");
            return await InvocarOperacionDesdeServicios(async () => await
                _servicioAplicacionTransferenciaEntrada.EchoTestDeCCE(datosEchoTest.ET1, 
                    datosEchoTest.codigoValidacionFirma));
        }
    }
}
