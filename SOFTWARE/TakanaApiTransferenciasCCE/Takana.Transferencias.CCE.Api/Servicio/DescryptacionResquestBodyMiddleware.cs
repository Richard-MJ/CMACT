using System.Text;
using Newtonsoft.Json;
using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Dominio.Servicios;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;

namespace Takana.Transferencias.CCE.Api.Servicio
{
    public class DescryptacionResquestBodyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IServicioAplicacionPeticion _servicioAplicacionPeticion;
        private readonly IBitacora<DescryptacionResquestBodyMiddleware> _bitacora;
        private readonly IServicioAplicacionSeguridad _servicioAplicacionSeguridad;

        /// <summary>
        /// Clase de desencriptacion middleware
        /// </summary>
        /// <param name="next"></param>
        /// <param name="configuration"></param>
        /// <param name="bitacora"></param>
        /// <param name="servicioAplicacionSeguridad"></param>
        /// <param name="servicioAplicacionPeticion"></param>
        public DescryptacionResquestBodyMiddleware(
            RequestDelegate next,
            IConfiguration configuration,
            IBitacora<DescryptacionResquestBodyMiddleware> bitacora,
            IServicioAplicacionPeticion servicioAplicacionPeticion,
            IServicioAplicacionSeguridad servicioAplicacionSeguridad)
        {
            _next = next;
            _bitacora = bitacora;
            _configuration = configuration;
            _servicioAplicacionPeticion = servicioAplicacionPeticion;
            _servicioAplicacionSeguridad = servicioAplicacionSeguridad;
        }

        /// <summary>
        /// Middleware que desencripta los mensajes recibidos
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            bool entornoEncriptacion = _configuration.GetValue<bool>("TAK_USAR_ENTORNO_SEGURIDAD_CCE");

            if (entornoEncriptacion && context.Request.Path.StartsWithSegments("/ssv2/payment-api"))
            {
                if (context.Request.Headers.ContainsKey("request-id"))
                {
                    var identificadorSolicitud = context.Request.Headers["request-id"].ToString();
                    _bitacora.Trace($"Se recibio un mensaje de la CCE con el Resquest-id: {identificadorSolicitud}");
                }

                var nombreCertificado = _configuration["NOMBRE_CERTIFICADO_FIRMA_CCE"]!;
                var codigoRespuesta = _servicioAplicacionSeguridad.ValidarCertificadoDigital(nombreCertificado);

                context.Request.EnableBuffering();

                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
                {
                    var requestBody = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                    var decryptedBodyBytes = await ObtenerDatosDesencriptadoAsync(requestBody, codigoRespuesta);
                    context.Request.Body = new MemoryStream(decryptedBodyBytes);
                    context.Request.Body.Position = 0;
                }
            }

            await _next(context);
        }

        /// <summary>
        /// Obtener datos Desencriptado
        /// </summary>
        /// <param name="requestBody"></param>
        /// <param name="codigoRespuesta"></param>
        /// <returns></returns>
        public async Task<byte[]> ObtenerDatosDesencriptadoAsync(string requestBody, string codigoRespuesta)
        {
            var estructuraSeguridadCCE = JsonConvert.DeserializeObject<EstructuraSeguridadCCE>(requestBody)!;

            string decryptedBody = await ObtenerCuerpoDesencriptado(estructuraSeguridadCCE, codigoRespuesta, requestBody);

            return Encoding.UTF8.GetBytes(decryptedBody);
        }

        /// <summary>
        /// Obtener cuerpo de Desencriptado
        /// </summary>
        /// <param name="estructuraSeguridadCCE"></param>
        /// <param name="codigoRespuesta"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        private async Task<string> ObtenerCuerpoDesencriptado(EstructuraSeguridadCCE estructuraSeguridadCCE, string codigoRespuesta, string requestBody)
        {
            if (string.IsNullOrEmpty(estructuraSeguridadCCE.Firma) || string.IsNullOrEmpty(estructuraSeguridadCCE.Cuerpo))
                return requestBody.MaquetarDatos(RazonRespuesta.codigoDS0A);

            var datos = ShortGuid.Base64UrlDecode(estructuraSeguridadCCE.Cuerpo);
            try
            {
                if (EsRespuestaExitosa(codigoRespuesta))
                    return await DesencriptarConRespuestaExitosa(estructuraSeguridadCCE, requestBody);

                _bitacora.Trace("Fin de la Validacion: Certificado de la CCE es Invalido");
                return datos.MaquetarDatos(codigoRespuesta);
            }
            catch(Exception excepcion)
            {
                _bitacora.Trace($"Ocurrio un error en la Desencripcitación: Código {excepcion.Message}");
                return datos.MaquetarDatos(excepcion.Message);
            }
        }

        /// <summary>
        /// Desencriptar Con codigo de Respuesta Exitosa
        /// </summary>
        /// <param name="estructuraSeguridadCCE"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        private async Task<string> DesencriptarConRespuestaExitosa(EstructuraSeguridadCCE estructuraSeguridadCCE, string requestBody)
        {
            _bitacora.Trace($"Inicio de la Validación de Firma del JSON: {requestBody}");
            return UsarHSM()
                ? await DesencriptarConHSM(estructuraSeguridadCCE)
                : DesencriptarSinHSM(estructuraSeguridadCCE, requestBody);
        }

        /// <summary>
        /// Desencriptar con HSM
        /// </summary>
        /// <param name="estructuraSeguridadCCE"></param>
        /// <returns></returns>
        private async Task<string> DesencriptarConHSM(EstructuraSeguridadCCE estructuraSeguridadCCE)
        {
            var resultado = await _servicioAplicacionPeticion.DesencriptarMensajeCCE<object>(estructuraSeguridadCCE);
            return JsonConvert.SerializeObject(resultado);
        }

        /// <summary>
        /// Desencriptar sin HSM
        /// </summary>
        /// <param name="estructuraSeguridadCCE"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        private string DesencriptarSinHSM(EstructuraSeguridadCCE estructuraSeguridadCCE, string requestBody)
        {
            var nombreCertificado = _configuration["NOMBRE_CERTIFICADO_FIRMA_CCE"]!;
            var codigoRespuesta = _servicioAplicacionSeguridad.ValidarContenidoFirmaDigital(requestBody, nombreCertificado);

            var datos = ShortGuid.Base64UrlDecode(estructuraSeguridadCCE.Cuerpo);

            if (EsRespuestaExitosa(codigoRespuesta)) return datos;

            _bitacora.Trace("Fin de la Validacion: Certificado de la CCE es Invalido");
            return datos.MaquetarDatos(codigoRespuesta);
        }

        /// <summary>
        /// Valor de variable de usar HSM
        /// </summary>
        /// <returns></returns>
        private bool UsarHSM() => _configuration.GetValue<bool>("TAK_USAR_MODULO_SEGURIDAD_HARDWARE");

        /// <summary>
        /// Codigo de Respuesta Exitosa
        /// </summary>
        /// <param name="codigoRespuesta"></param>
        /// <returns></returns>
        private bool EsRespuestaExitosa(string codigoRespuesta) => codigoRespuesta == RazonRespuesta.codigo0000;
    }
}
