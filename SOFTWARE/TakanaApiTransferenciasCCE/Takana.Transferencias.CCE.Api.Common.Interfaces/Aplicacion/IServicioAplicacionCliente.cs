using Takana.Transferencias.CCE.Api.Common.DTOs.BA;
using Takana.Transferencias.CCE.Api.Common.DTOs.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;

namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    public interface IServicioAplicacionCliente
    {
        /// <summary>
        /// Metodo de busca la cuenta del cliente por Codigo de Cuenta Interbancaria
        /// </summary>
        /// <param name="codigoCuentaInterbancario">Codigo de Cuenta Interbancaria</param>
        /// <returns>datos del cliente</returns>
        ClienteReceptorDTO ObtenerDatosPorCodigoCuentaInterbancaria(string? codigoCuentaInterbancario);
        /// <summary>
        /// Obttiene todo lo necesario para iniciar el canal de ventanilla
        /// </summary>
        /// <returns>Datos para inicializar ventanilla</returns>
        Task<InicializarVentanillaDTO> ObtenerDatosInicialesVentanilla();
        /// <summary>
        /// Lista la entidades financieras segun la CCE (Expuesto)
        /// </summary>
        /// <returns>Retorna lista de las entidades de la CCE</returns>
        Task<List<EntidadFinancieraTinDTO>> ListarEntidadesFinancieras();

        /// <summary>
        /// Obtiene los tipos de documento
        /// </summary>
        /// <returns>Lista de tipos de documentos</returns>
        Task<List<TipoDocumentoTinDTO>> ListarTiposDocumentoCCE();
        /// <summary>
        /// Método que devuelve el cliente Originante en Transferencia Interbancaria Inmediata Entrante
        /// </summary>
        /// <param name="transaccion">Datos de la transaccion</param>
        /// <returns>Retorna datos del cliente originante</returns>
        ClienteExternoDTO ObtenerClienteOriginante(
            TransaccionOrdenTransferenciaInmediata transaccion);
        /// <summary>
        /// Método que devuelve el cliente Originante en Transferencia Interbancaria Inmediata Entrante
        /// </summary>
        /// <param name="transaccion">Datos del Cliente</param>
        /// <returns>Retorna datos del cliente originante</returns>
        ClienteExternoDTO ObtenerClienteOriginante(
           CuentaEfectivo cuentaEfectivo);
        /// <summary>
        /// Metodo que obtiene los datos del cliente originante para operacion saliente
        /// </summary>
        /// <param name="numeroCuenta">Numero cuenta del cliente originante</param>
        /// <returns>Datos cuenta efectivo</returns>
        Task<CuentaEfectivoDTO> ObtenerDatosCuentaOrigen(string numeroCuenta);
        /// <summary>
        /// Método que obtiene los datos iniciales para el cliente
        /// </summary>
        /// <returns></returns>
        Task<InicializarCanalElectronicoDTO> ObtenerDatosInicialesCanalElectronico();
        /// <summary>
        /// Metodo que enviar el correo de confirmacion de la operacion
        /// </summary>
        /// <param name="numeroMovimiento"></param>
        /// <param name="indicadorTipo"></param>
        /// <param name="nombreDocumentoTerminos"></param>
        /// <param name="documentoTerminos"></param>
        /// <param name="correoDestinatario"></param>
        /// <returns></returns>
        Task EnviarCorreoClienteTransferenciaInmediata(
            int numeroMovimiento,
            string nombreDocumentoTerminos = "",
            byte[]? documentoTerminos = null,
            string correoDestinatario = "");
        /// <summary>
        /// Metodo envio de notificaciones Unibanca
        /// </summary>
        /// <param name="numeroMovimiento">numero movimiento</param>
        /// <param name="codigoTipoOperacion">codigo tipo operacion</param>
        /// <returns></returns>
        Task EnviarNotificacionAntifraudeUNIBANCA(int numeroMovimiento, string codigoTipoOperacion);
    }
}
