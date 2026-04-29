namespace Takana.Transferencias.CCE.Api.Dominio.Entidades
{
    /// <summary>
    /// Interfaz de dominio de lavado de activos
    /// </summary>
    public interface ILavadoInterviniente
    {
        /// <summary>
        /// Representa el apellido paterno o razón social
        /// </summary>
        string ApellidoPaterno { get; }

        /// <summary>
        /// Representa el apellido materno
        /// </summary>
        string ApellidoMaterno { get; }

        /// <summary>
        /// Representa el nombre
        /// </summary>
        string Nombres { get; }

        /// <summary>
        /// Representa el número de operación de lavado
        /// </summary>
        decimal? NumeroMovimientoLavado { get; }

        /// <summary>
        /// Código del cliente
        /// </summary>
        string CodigoCliente { get; }

        /// <summary>
        /// Tipo de documento
        /// </summary>
        string TipoDocumento { get; }
    }
}
