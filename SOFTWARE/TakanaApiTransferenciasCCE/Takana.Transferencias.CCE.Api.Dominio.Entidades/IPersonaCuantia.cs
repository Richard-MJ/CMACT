using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades
{
    /// <summary>
    /// Interfaz de dominio de persona cuantia
    /// </summary>
    public interface IPersonaCuantia
    {
        /// <summary>
        /// Nombres de persona
        /// </summary>
        string Nombres { get; }
        /// <summary>
        /// Apellido d epersona
        /// </summary>
        string ApellidoPaterno { get; }
        /// <summary>
        /// Apellido de materno
        /// </summary>
        string ApellidoMaterno { get; }
        /// <summary>
        /// Codigo de cliente
        /// </summary>
        string CodigoCliente { get; }
        /// <summary>
        /// Tipo de cliente
        /// </summary>
        string TipoCliente { get; }
        /// <summary>
        /// Datos del cliente
        /// </summary>
        Cliente Cliente { get; }
    }
}
