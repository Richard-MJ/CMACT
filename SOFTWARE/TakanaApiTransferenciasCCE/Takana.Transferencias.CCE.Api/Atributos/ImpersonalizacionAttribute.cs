using Polly;
using CsvHelper;
using System.Text;
using Renci.SshNet;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Versioning;
using Org.BouncyCastle.Security;
using System.Security.Principal;
using Microsoft.Win32.SafeHandles;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using Microsoft.Data.SqlClient.Server;
using Microsoft.AspNetCore.Mvc.Filters;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;

namespace Takana.Transferencias.CCE.Api.Atributos
{
    public class ImpersonalizacionAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ImpersonalizacionAttribute() : base(typeof(ImpersonalizacionFilter))
        {
        }
        /// <summary>
        /// Clase que representa la generacion de filtros de la session
        /// </summary>
        private class ImpersonalizacionFilter : IAsyncActionFilter
        {
            /// <summary>
            /// Declaración de la función LogonUser proveniente de la librería "advapi32.dll".
            /// Esta función permite autenticar un usuario mediante un nombre de usuario, dominio y contraseña.
            /// Retorna un token de acceso seguro que representa la sesión del usuario autenticado.
            /// </summary>
            /// <param name="nombreUsuario">Nombre del usuario a autenticar.</param>
            /// <param name="dominio">Nombre del dominio al que pertenece el usuario. Puede ser nulo o vacío si se utiliza una cuenta local.</param>
            /// <param name="conrtasenia">Contraseña del usuario.</param>
            /// <param name="tipoSesion">Tipo de inicio de sesión (por ejemplo, interactivo o de red).</param>
            /// <param name="proveedorSesion">Proveedor de autenticación. Por lo general, se utiliza el valor predeterminado.</param>
            /// <param name="token">Devuelve un token seguro que representa la sesión del usuario autenticado</param>
            /// <returns>true si es autorizado</returns>
            [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern bool LogonUser(
                string nombreUsuario,
                string dominio,
                string conrtasenia,
                int tipoSesion,
                int proveedorSesion,
                out SafeAccessTokenHandle token);

            /// <summary>
            /// Instancia de IContextoAplicacion
            /// </summary>
            private readonly IContextoAplicacion _contexto;
            /// <summary>
            /// Instancia de de IConfiguration
            /// </summary>
            private readonly IConfiguration _configuration;
            /// <summary>
            /// Instancia de IBitacora
            /// </summary>
            private readonly IBitacora<ImpersonalizacionFilter> _bitacora;

            /// <summary>
            /// Constructor
            /// </summary>
            public ImpersonalizacionFilter(
                IConfiguration configuration,
                IContextoAplicacion contexto, 
                IBitacora<ImpersonalizacionFilter> bitacora)
            {
                _contexto = contexto;
                _bitacora = bitacora;
                _configuration = configuration;
            }
            /// <summary>
            /// Impersonaliza la sesión y la autentifica
            /// </summary>
            /// <param name="context"></param>
            /// <param name="next"></param>
            /// <returns></returns>
            [SupportedOSPlatform("windows")]
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                bool esProduccion = _configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT") == "Production";

                if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    _bitacora.Error("No se realiza impersonalización. El sistema operativo no es Windows.");
                    await next();
                    return;
                }

                if (!esProduccion)
                {
                    await next();
                    return;
                }

                try
                {
                    SafeAccessTokenHandle identificadorToken;
                    const int logon32ProviderDefault = 0;
                    const int logon32Interactive = 2;
                    var llave = _configuration["TAK_CLAVE_DESENCRIPTAR"]!;
                    var contrasenaDesencriptada = DesencriptarClave(_contexto.ContrasenaEncriptada, llave);

                    var resultadoLogin = LogonUser(_contexto.CodigoUsuario, _contexto.Dominio,
                        contrasenaDesencriptada, logon32Interactive,
                        logon32ProviderDefault, out identificadorToken);
                    if (!resultadoLogin)
                    {
                        var codigoError = Marshal.GetLastWin32Error();
                        throw new Exception($"LogonUser() falló con código: {codigoError}");
                    }

                    _bitacora.Debug($"Operación Impersonalización (Antes): " + $"{WindowsIdentity.GetCurrent().Name}");
                    CargarAsambleaPolly();
                    CargarAsambleaCsvHelper();
                    CargarAsambleaSshNet();
                    CargarAsambleaSqlServer();
                    await WindowsIdentity.RunImpersonated(identificadorToken, async () =>
                    {
                        _bitacora.Debug($"Operación Impersonalización (Después): " + WindowsIdentity.GetCurrent().Name);
                        await next();
                    });
                }
                catch (Exception excepcion)
                {
                    _bitacora.Fatal("Operación Impersonalización: {mensaje} <|> {stackTrace}",
                        excepcion.Message, excepcion.StackTrace);
                    throw new Exception("Ocurrió un problema al impersonalizar el usuario.");
                }
            }

            #region Métodos privados
            /// <summary>
            /// Método que desencripta la contraseña del usuario
            /// </summary>
            /// <param name="contrasenaEncriptada">Contraseña encriptada</param>
            /// <param name="llaveDesencriptacion">Llave desencriptador</param>
            /// <returns>Contraseña desencriptada</returns>
            private string DesencriptarClave(string contrasenaEncriptada, string llaveDesencriptacion)
            {
                var codificador = new UTF8Encoding();
                var encriptador = Aes.Create();
                try
                {
                    var textoCifrado = Convert.FromBase64String(contrasenaEncriptada);
                    var vector = new byte[encriptador.IV.Length];
                    var textoEncriptado = new byte[textoCifrado.Length - encriptador.IV.Length];
                    encriptador.Key = codificador.GetBytes(llaveDesencriptacion);
                    Array.Copy(textoCifrado, vector, vector.Length);
                    Array.Copy(textoCifrado, encriptador.IV.Length, textoEncriptado,
                        0, textoEncriptado.Length);
                    encriptador.IV = vector;
                    return codificador.GetString(encriptador.CreateDecryptor().TransformFinalBlock(
                        textoEncriptado, 0, textoEncriptado.Length));
                }
                catch (Exception excepcion)
                {
                    throw new Exception($"Error al desencriptar contraseña del usuario: {excepcion.Message}");
                }
            }

            /// <summary>
            /// Método que carga la asamblea de polly para evitar error en el cambio de impersonalización
            /// </summary>
            private void CargarAsambleaPolly()
            {
                _ = typeof(Policy).Assembly;
                Policy.Handle<Exception>()
                    .WaitAndRetryAsync(5,intento => TimeSpan.FromSeconds(30));
                Policy.Handle<Exception>()
                    .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
            }

            /// <summary>
            /// Método que carga la asamblea de CsvHelper para evitar error en el cambio de impersonalización
            /// </summary>
            private void CargarAsambleaCsvHelper()
            {
                _ = typeof(CsvReader).Assembly;
                using var dummyReader = new StringReader(string.Empty);
                using var csv = new CsvReader(dummyReader, System.Globalization.CultureInfo.InvariantCulture);
            }

            /// <summary>
            /// Método que carga la asamblea de CsvHelper para evitar error en el cambio de impersonalización
            /// </summary>
            private void CargarAsambleaSshNet()
            {
                _ = typeof(SftpClient).Assembly;
                _ = typeof(SecureRandom).Assembly;
                _ = typeof(PrivateKeyFile).Assembly;
            }

            /// <summary>
            /// Carga anticipada de Microsoft.SqlServer.Server para evitar errores por impersonación
            /// </summary>
            private void CargarAsambleaSqlServer()
            {
                _ = typeof(SqlDataRecord).Assembly;
                var metadata = new SqlMetaData("dummy", System.Data.SqlDbType.Int);
                var record = new SqlDataRecord(metadata);
                _ = record.GetSqlInt32(0);
            }
            #endregion
        }
    }
}
