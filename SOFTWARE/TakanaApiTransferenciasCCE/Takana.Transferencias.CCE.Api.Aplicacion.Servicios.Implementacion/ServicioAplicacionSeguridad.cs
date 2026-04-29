using Polly;
using Polly.Wrap;
using System.Text;
using Polly.Timeout;
using Newtonsoft.Json;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Takana.Transferencias.CCE.Api.Common;
using System.Security.Cryptography.X509Certificates;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Extensiones;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion
{
    /// <summary>
    /// Clase capa Aplicacion encargado de la seguridad 
    /// </summary>
    public class ServicioAplicacionSeguridad : ServicioBase, IServicioAplicacionSeguridad
    {
        private readonly IConfiguration _configuration;
        private readonly IRepositorioGeneral _repositorioGeneral;
        private readonly IRepositorioOperacion _repositorioOperaciones;
        private readonly IServicioAplicacionColas _servicioAplicacionColas;
        private readonly IBitacora<ServicioAplicacionSeguridad> _bitacora;
        private readonly IConfiguracionPollyWrapOptions _configuracionPollyWrapOptions;
        public ServicioAplicacionSeguridad(
            IContextoAplicacion contexto,
            IConfiguration configuration,
            IRepositorioGeneral repositorioGeneral,
            IRepositorioOperacion repositorioOperaciones,
            IServicioAplicacionColas servicioAplicacionColas,
            IConfiguracionPollyWrapOptions configuracionPollyWrapOptions,
            IBitacora<ServicioAplicacionSeguridad> bitacora) : base(contexto)
        {
            _bitacora = bitacora;
            _configuration = configuration;
            _repositorioGeneral = repositorioGeneral;
            _repositorioOperaciones = repositorioOperaciones;
            _servicioAplicacionColas = servicioAplicacionColas;
            _configuracionPollyWrapOptions = configuracionPollyWrapOptions;
        }

        /// <summary>
        /// Carga el contexto de Respositorio General
        /// </summary>
        public async Task CargarContextoRepositorioGeneral()
            => _repositorioGeneral.ObtenerPorCodigo<Usuario>(Empresa.CodigoPrincipal, Agencia.Principal, General.UsuarioPorDefecto);

        /// <summary>
        /// Carga el contexto de Respositorio operacion
        /// </summary>
        public async Task CargarContextoRepositorioOperacion()
            => _repositorioOperaciones.ObtenerPorCodigo<Usuario>(Empresa.CodigoPrincipal, Agencia.Principal, General.UsuarioPorDefecto);

        /// <summary>
        /// Encripta el mensaje de la CCE sin el HSM
        /// </summary>
        /// <param name="datos"></param>
        /// <returns>Retorna el mensaje encriptado</returns>
        public async Task<object> EncriptarMensajeCCE(object datos)
        {
            string payloadBase64Url = ShortGuid.Base64UrlEncode(JsonConvert.SerializeObject(datos));
            string protectedHeaderBase64Url = ObtenerHeaderHuellaDigitalCertificadoCCE();
            string dataToSign = $"{protectedHeaderBase64Url}.{payloadBase64Url}";

            var rutaClavePrivada = "C:/CERTIFICADOS/CMACT/PrivKey.txt";
            string clavePrivadaTxt = File.ReadAllText(rutaClavePrivada).Trim();

            using var rsa = RSA.Create();
            var signatureBytes = rsa.SignData(Encoding.UTF8.GetBytes(dataToSign), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            string signatureBase64Url = ShortGuid.Base64UrlEncode(signatureBytes);

            var mensajeFirmado = new
            {
                payload = payloadBase64Url,
                @protected = protectedHeaderBase64Url,
                signature = signatureBase64Url
            };

            return mensajeFirmado;
        }

        /// <summary>
        /// Método que valida y desencripta el mensaje de la CCE.
        /// </summary>
        /// <param name="estructuraSeguridadCCE">Estructura de seguridad CCE con firma y cuerpo en base64</param>
        /// <returns>Objeto deserializado del tipo T</returns>
        public async Task<T> DesencriptarMensajeCCE<T>(EstructuraSeguridadCCE estructuraSeguridadCCE)
        {
            var nombreCertificado = _configuration["NOMBRE_CERTIFICADO_FIRMA_CCE"]!;
            var datosSerializados = JsonConvert.SerializeObject(estructuraSeguridadCCE);

            VerificarCertificadoYFirmaDigital(nombreCertificado, datosSerializados);

            var desencriptarBase64 = ShortGuid.Base64UrlDecode(estructuraSeguridadCCE!.Cuerpo);
            return JsonConvert.DeserializeObject<T>(desencriptarBase64)!;
        }

        /// <summary>
        /// Valida el certificado digital y la firma del mensaje de la CCE.
        /// </summary>
        /// <param name="datosSerializados">Estructura de seguridad que contiene la firma y el cuerpo</param>
        public void VerificarCertificadoYFirmaDigital(string nombreCertificado, string datosSerializados)
        {
            var codigoRespuesta = ValidarCertificadoDigital(nombreCertificado);
            if (codigoRespuesta != RazonRespuesta.codigo0000)
            {
                _bitacora.Trace($"Certificado de firma digital inválido: {nombreCertificado}");
                throw new Exception($"Certificado de firma digital inválido: {nombreCertificado}");
            }

            ValidarContenidoFirmaDigital(datosSerializados, nombreCertificado);
        }

        /// <summary>
        /// Obtener Header de la Huella digital del certificado
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns>Retorna Cadena en base 64</returns>
        public string ObtenerHeaderHuellaDigitalCertificadoCCE()
        {
            var nombreCertificado = _configuration["NOMBRE_CERTIFICADO_FIRMA_CMACT"]!;

            var certificadoPublico = ObtenerCertificadoCCEDelAlmacenDeConfianza(nombreCertificado);

            var protectedHeader = new EstructuraProtectedCCE
            {
                alg = "RS256",
                x5t = ShortGuid.Base64UrlEncode(certificadoPublico.GetCertHash())
            };

            return ShortGuid.Base64UrlEncode(JsonConvert.SerializeObject(protectedHeader));
        }

        /// <summary>
        /// Validar el Certificado de la CCE
        /// </summary>
        /// <returns></returns>
        public string ValidarCertificadoDigital(string nombreCertificado)
        {
            var certificadoFirmaCCE = ObtenerCertificadoCCEDelAlmacenDeConfianza(nombreCertificado);

            _bitacora.Trace($"Inicio de la Validación del Certificado: {certificadoFirmaCCE.Subject}");

            if (DateTime.Now < certificadoFirmaCCE.NotBefore || DateTime.Now > certificadoFirmaCCE.NotAfter)
                return RazonRespuesta.codigoDS0D;

            var codigoRespuesta = EsEmitidoPorCAConfiable(certificadoFirmaCCE);

            if (codigoRespuesta == RazonRespuesta.codigo0000)
                _bitacora.Trace($"Fin de la Validacion: Certificado de la CCE es Valido");

            return codigoRespuesta;
        }

        /// <summary>
        /// Obtiene el certificado de la CCE del Almacen de Confianza
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private X509Certificate2 ObtenerCertificadoCCEDelAlmacenDeConfianza(string nombreCertificado)
        {
            using var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certificados = store.Certificates.Find(X509FindType.FindBySubjectName, nombreCertificado, validOnly: true);
            store.Close();

            if (certificados.Count > 0) return certificados[0];

            throw new Exception($"Certificado confiable no encontrado en el truststore {nombreCertificado}");
        }

        /// <summary>
        /// Verificar si el certificado es emitido por una Entidad Certificadora Autorizada es confiable
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns>Retorna true o false</returns>
        /// <exception cref="Exception">Excepcion de error de la cadena</exception>
        private string EsEmitidoPorCAConfiable(X509Certificate2 certificate)
        {
            var verificar = _configuration.GetValue<bool>("TAK_VERIFICAR_REVOCACION_CERTIFICADO");
            var codigoRespuesta = RazonRespuesta.codigo0000;

            if (!verificar) return codigoRespuesta;

            using (var chain = new X509Chain())
            {
                chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                chain.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag;
                chain.ChainPolicy.VerificationTime = DateTime.Now;
                chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 0, 30);

                bool esValidado = chain.Build(certificate);

                if (!esValidado)
                {
                    foreach (X509ChainStatus chainStatus in chain.ChainStatus)
                    {
                        if (chainStatus.Status != X509ChainStatusFlags.NoError)
                        {
                            _bitacora.Trace($"Error en la cadena de certificados: {chainStatus.StatusInformation}");
                            codigoRespuesta = RazonRespuesta.codigoDS0D;
                        }
                    }
                }
                return codigoRespuesta;
            }
        }

        /// <summary>
        /// Validar la Firma de la CCE
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string ValidarContenidoFirmaDigital(string datos, string nombreCertificado)
        {
            try
            {
                var certificadoFirmaCCE = ObtenerCertificadoCCEDelAlmacenDeConfianza(nombreCertificado);

                var estructuraSeguridadCCE = JsonConvert.DeserializeObject<EstructuraSeguridadCCE>(datos);

                var codigoRespuesta = ValidarHuellaDigital(estructuraSeguridadCCE!.Proteccion, certificadoFirmaCCE);
                if (codigoRespuesta != RazonRespuesta.codigo0000)
                    return codigoRespuesta;

                byte[] signatureBytes = ShortGuid.Base64UrlDecodeBytes(estructuraSeguridadCCE.Firma);
                string message = estructuraSeguridadCCE.Proteccion + "." + estructuraSeguridadCCE.Cuerpo;
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);

                using (RSA rsa = certificadoFirmaCCE.GetRSAPublicKey()!)
                {
                    var validado = rsa.VerifyData(messageBytes, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                    if (!validado)
                    {
                        _bitacora.Trace($"Fin de la Validación: No se pudo validar la firma del certificado {nombreCertificado}");
                        return RazonRespuesta.codigoDS0A;
                    }
                }

                _bitacora.Trace($"Fin de la Validación de Firma: El Certificado de Firma si coincide con la Firma del Mensaje {nombreCertificado}");

                return RazonRespuesta.codigo0000;
            }
            catch (Exception excepcion)
            {
                _bitacora.Trace($"Error al validar la firma del certificado {nombreCertificado}: " + excepcion.Message);
                return RazonRespuesta.codigoDS0D;
            }
        }

        /// <summary>
        /// Validar Huella Digital del Certificado
        /// </summary>
        /// <param name="datos"></param>
        /// <param name="certificadoFirmaCCE"></param>
        /// <exception cref="Exception">Excepcion de no validacion de huella digital</exception>
        public string ValidarHuellaDigital(string datos, X509Certificate2 certificadoFirmaCCE)
        {
            byte[] protectedHeaderBytes = ShortGuid.Base64UrlDecodeBytes(datos);
            string protectedHeaderJson = Encoding.UTF8.GetString(protectedHeaderBytes);
            var protectedHeader = JsonConvert.DeserializeObject<EstructuraProtectedCCE>(protectedHeaderJson);
            byte[] x5tHash = ShortGuid.Base64UrlDecodeBytes(protectedHeader!.x5t);

            if (!certificadoFirmaCCE.GetCertHash().SequenceEqual(x5tHash))
            {
                _bitacora.Trace($"El valor x5t del encabezado protegido no coincide con la huella digital (hash) del certificado.");
                return RazonRespuesta.codigoDS0D;
            }

            return RazonRespuesta.codigo0000;
        }

        #region Validacion de certificado de Caja Tacna
        /// <summary>
        /// Metodo para validar la firma digital del mensaje de la CMAC
        /// </summary>
        /// <param name="datos"></param>
        /// <param name="datos"></param>
        /// <exception cref="Exception"></exception>
        public async Task ValidarFirmaDigitalCMAC(PinPropiedades datos, string contenido)
        {
            var estructura = JsonConvert.DeserializeObject<EstructuraSeguridadCCE>(datos.Mensaje);
            if (!string.Equals(estructura!.Cuerpo, contenido, StringComparison.Ordinal))
                await EnviarFirmaInvalidaAsync(datos.HostName, "El cuerpo del mensaje no coincide con el contenido.");

            var nombreCertificado = _configuration.GetValue<string>("NOMBRE_CERTIFICADO_FIRMA_CMACT");
            if (string.IsNullOrWhiteSpace(nombreCertificado))
                throw new InvalidOperationException("Falta configurar NOMBRE_CERTIFICADO_FIRMA_CMACT.");

            var codigo = ValidarContenidoFirmaDigital(datos.Mensaje, nombreCertificado);
            if (!string.Equals(codigo, RazonRespuesta.codigo0000, StringComparison.Ordinal))
                await EnviarFirmaInvalidaAsync(datos.HostName, $"Validación de firma devolvió: {codigo}");

            _repositorioOperaciones.GuardarCambios();
        }

        /// <summary>
        /// Metodo para manejar el caso de firma digital inválida, registra en bitácora y envía correo de error por RabitMQ
        /// </summary>
        /// <param name="hostName"></param>
        /// <param name="detalle"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task EnviarFirmaInvalidaAsync(string hostName, string? detalle = null)
        {
            _bitacora.Warn("Firma digital inválida. Host={Host}. Detalle={Detalle}", hostName, detalle);
            await EnviarCorreoErrorPorRabitAsync(hostName).ConfigureAwait(false);
            throw new Exception(RazonRespuesta.codigoERRFIRMA);
        }
        #endregion

        #region MyRegion
        /// <summary>
        /// Metodo para enviar correo de error de firma digital por RabitMQ
        /// </summary>
        /// <param name="nombreServidor"></param>
        /// <returns></returns>
        private async Task EnviarCorreoErrorPorRabitAsync(string nombreServidor)
        {
            var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo().FechaHoraSistema;

            var correoRemitente = _repositorioGeneral
                .ObtenerPorExpresionConLimite<ParametroGeneralTransferencia>(x =>
                    x.CodigoParametro == ParametroGeneralTransferencia.CorreoElectronicoAdministrador)
                .First().ValorParametro;

            var correosDestinatarios = _repositorioGeneral
                .ObtenerPorExpresionConLimite<ParametroGeneralTransferencia>(x =>
                    x.CodigoParametro == ParametroGeneralTransferencia.CorreoElectronicoDestinariosERRFIRMA)
                .First().ValorParametro;

            var correosElectronicosDestinatarios = correosDestinatarios.Split(";");

            string temaMensaje = "Error en Validación de Firma digital – Transferencias Inmediatas (CCE)";
            string descripcion = $"Se detectó un error en la validación de la firma digital durante el proceso de Transferencias Interbancarias Inmediatas.";
            string servidor = $"El proceso de firmado digital se ejecutó en el servidor {nombreServidor} correspondiente al servicio PinOperacionApi";

            foreach (var correoDestino in correosElectronicosDestinatarios)
            {
                var formatoCorreo = _contextoAplicacion.AdtoFormatoCorreo(fechaSistema, correoRemitente,
                    correoDestino, servidor, descripcion, temaMensaje, new List<ArchivoAdjuntoDTO>());

                _ = Task.Run(async () => await _servicioAplicacionColas.EnviarCorreoAsync(CorreoGeneralDTO.CodigoErrorFirmaDigital, formatoCorreo));
            }
        }
        #endregion


        #region Politicas de Reintento
        /// <summary>
        /// Configuracion de la politica de reintentos
        /// </summary>
        /// <param name="cantidadReintento">Numero de reintentos</param>
        /// <returns>Retorna politica de reintentos</returns>
        public AsyncPolicyWrap PoliticasReintento(int cantidadReintento)
        {
            var timeoutPolitica = Policy
                .TimeoutAsync(
                    TimeSpan.FromSeconds(_configuracionPollyWrapOptions.TimeOut),
                    TimeoutStrategy.Optimistic);

            var reintentoPolitica = Policy
                .Handle<Exception>()
                .Or<TimeoutRejectedException>()
                .WaitAndRetryAsync(
                    retryCount: cantidadReintento,
                    sleepDurationProvider: _ =>
                        TimeSpan.FromSeconds((double)_configuracionPollyWrapOptions.RetryWaitSeconds),
                    onRetry: (exception, waitTime, intento, context) =>
                    {
                        context["RetryAttempt"] = intento;
                    });

            var circuitoPolitica = Policy
                .Handle<Exception>()
                .Or<TimeoutRejectedException>()
                .CircuitBreakerAsync(
                    exceptionsAllowedBeforeBreaking: cantidadReintento + 1,
                    durationOfBreak:
                        TimeSpan.FromSeconds(_configuracionPollyWrapOptions.CircuitBreakSeconds));

            return Policy.WrapAsync(
                circuitoPolitica,
                reintentoPolitica,
                timeoutPolitica);
        }

        #endregion Politica
    }
}
