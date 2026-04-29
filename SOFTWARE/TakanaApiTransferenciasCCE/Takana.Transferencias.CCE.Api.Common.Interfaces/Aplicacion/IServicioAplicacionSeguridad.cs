using Polly.Wrap;

namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    public interface IServicioAplicacionSeguridad
    {
        /// <summary>
        /// Carga el contexto de Respositorio General
        /// </summary>
        Task CargarContextoRepositorioGeneral();
        /// <summary>
        /// Carga el contexto de Respositorio operacion
        /// </summary>
        Task CargarContextoRepositorioOperacion();
        /// <summary>
        /// Encriptar Mensaje de la CCE
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        Task<object> EncriptarMensajeCCE(object datos);
        /// <summary>
        /// Desencriptar mensaje dela CCE
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datos"></param>
        /// <returns></returns>
        Task<T> DesencriptarMensajeCCE<T>(EstructuraSeguridadCCE datos);
        /// <summary>
        /// Validar Certificado Digital
        /// </summary>
        /// <param name="nombreCertificado"></param>
        /// <returns></returns>
        string ValidarCertificadoDigital(string nombreCertificado);
        /// <summary>
        /// Validar Contenido Firma Digital
        /// </summary>
        /// <param name="datos"></param>
        /// <param name="nombreCertificado"></param>
        /// <returns></returns>
        string ValidarContenidoFirmaDigital(string datos, string nombreCertificado);
        /// <summary>
        /// Metodo que obtiene el header de la huella digital del certificado
        /// </summary>
        /// <returns>Retorna datos en base 64</returns>
        string ObtenerHeaderHuellaDigitalCertificadoCCE();
        /// <summary>
        /// Metodo para validar la firma digital del mensaje de la CMAC
        /// </summary>
        /// <param name="datos"></param>
        Task ValidarFirmaDigitalCMAC(PinPropiedades datos, string contenido);
        /// <summary>
        /// Configuracion de la politica de reintentos
        /// </summary>
        /// <param name="cantidadReintento">Numero de reintentos</param>
        /// <returns>Retorna politica de reintentos</returns>
        AsyncPolicyWrap PoliticasReintento(int cantidadReintento);
    }
}
