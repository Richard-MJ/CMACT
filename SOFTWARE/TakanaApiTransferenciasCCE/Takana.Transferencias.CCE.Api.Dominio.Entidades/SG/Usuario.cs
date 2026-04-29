using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.SG
{
    /// <summary>
    /// Clase que representa a la entidad de dominio de un Usuario
    /// </summary>
    public class Usuario : Empresa
    {
        /// <summary>
        /// Codigo del usuario
        /// </summary>
        public string CodigoUsuario { get; private set; }
        /// <summary>
        /// Codigo de la agencia
        /// </summary>
        public string CodigoAgencia { get; private set; }
        /// <summary>
        /// Indicador habilitado
        /// </summary>
        public string IndicadorHabilitado { get; private set; }
        /// <summary>
        /// Indicador activo
        /// </summary>
        public string IndicadorActivo { get; private set; }
        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string NombreUsuario { get; private set; }
        /// <summary>
        /// Codigo del cliente
        /// </summary>
        public string CodigoCliente { get; private set; }
        /// <summary>
        /// Clave del usuario
        /// </summary>
        public string Clave { get; private set; }
        /// <summary>
        /// Código de Puesto
        /// </summary>
        public string CodigoPuesto { get; private set; }
        /// <summary>
        /// Instancia de la entidad Agencia
        /// </summary>
        public virtual Agencia Agencia { get; private set; }
    }
}
