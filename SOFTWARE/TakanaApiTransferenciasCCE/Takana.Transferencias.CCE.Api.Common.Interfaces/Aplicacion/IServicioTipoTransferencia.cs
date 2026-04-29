using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;

namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    public interface IServicioTipoTransferencia
    {
        /// <summary>
        /// Método que construye el cuerpo de consulta cuenta
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        Task<ConsultaCuentaOperacionDTO> ConstruirCuerpoConsulta(
            ConsultaCuentaReceptorDTO datos);

        /// <summary>
        /// Obtener el tipo de mensaje para el correo
        /// </summary>
        /// <returns></returns>
        string ObtenerTemaMensajeParaCorreo();
        /// <summary>
        /// Obtener el tipo de mensaje para el correo
        /// </summary>
        /// <returns></returns>
        string ObtenerTipoMensajeParaCorreo();
        /// <summary>
        /// Obtener el tipo de mensaje para el correo
        /// </summary>
        /// <returns></returns>
        string ObtenerServicioMensajeParaCorreo(string canal);
        /// <summary>
        /// Método que define la plaza
        /// </summary>
        /// <param name="entidadFinancieraOriginante"></param>
        /// <param name="oficinaDestino"></param>
        /// <param name="codigoCuentaInterbancario"></param>
        /// <returns></returns>
        string DefinirPlaza(
            EntidadFinancieraDiferida entidadFinancieraOriginante,
            OficinaCCE oficinaDestino, string codigoCuentaInterbancario);

        /// <summary>
        /// Método que verifica si es exonerado de Comisión
        /// </summary>
        /// <param name="esMismaPlaza"></param>
        /// <param name="numeroCuenta"></param>
        /// <param name="fechaProceso"></param>
        /// <returns></returns>
        bool VerificarSiEsExoneradoComisión(
            bool esMismaPlaza,
            string numeroCuenta,
            DateTime fechaProceso);
    }
}
