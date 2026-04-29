using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades
{
    /// <summary>
    /// Interfaz de dominio de Entidad producto
    /// </summary>
    public interface IEntidadProducto
    {
        /// <summary>
        /// Numero de producto
        /// </summary>
        string NumeroProducto { get; }
        /// <summary>
        /// Codigo de agencia
        /// </summary>
        string CodigoAgencia { get; }
        /// <summary>
        /// Codigo de tipo
        /// </summary>
        string CodigoTipo { get; }
        /// <summary>
        /// Descripcion
        /// </summary>
        string Descripcion { get; }
        /// <summary>
        /// Simbolo de la moneda
        /// </summary>
        string SimboloMoneda { get; }
        /// <summary>
        /// Codigo de la moneda
        /// </summary>
        string CodigoMoneda { get; }
        /// <summary>
        /// Codigo de cliente
        /// </summary>
        string CodigoCliente { get; }
        /// <summary>
        /// Datos de la moneda
        /// </summary>
        Moneda Moneda { get; }
        /// <summary>
        /// Datos de la agencia
        /// </summary>
        Agencia Agencia { get; }
        /// <summary>
        /// Datos del cliente
        /// </summary>
        Cliente Cliente { get; }
    }
}
