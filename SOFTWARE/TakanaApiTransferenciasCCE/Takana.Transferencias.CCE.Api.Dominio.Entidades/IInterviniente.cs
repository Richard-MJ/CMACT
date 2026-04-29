namespace Takana.Transferencias.CCE.Api.Dominio.Entidades
{
    /// <summary>
    /// Interfaz de dominio de un interviniente
    /// </summary>
    public interface IInterviniente
    {
        /// <summary>
        /// Codigo de tipo interviniente
        /// </summary>
        int CodigoTipoInterviniente { get; }
        /// <summary>
        /// Codigo de tipo de documento
        /// </summary>
        string CodigoTipoDocumento { get; }
        /// <summary>
        /// Numero de documento
        /// </summary>
        string NumeroDocumento { get; }
        /// <summary>
        /// Valida si es cliente o no
        /// </summary>
        bool EsCliente { get; }
        /// <summary>
        /// Codigo de cliente
        /// </summary>
        string CodigoCliente { get; }
        /// <summary>
        /// Apellido paterno
        /// </summary>
        string ApellidoPaterno { get; }
        /// <summary>
        /// Apellido materno
        /// </summary>
        string ApellidoMaterno { get; }
        /// <summary>
        /// Nombres del interviniente
        /// </summary>
        string Nombres { get; }
    }
}
