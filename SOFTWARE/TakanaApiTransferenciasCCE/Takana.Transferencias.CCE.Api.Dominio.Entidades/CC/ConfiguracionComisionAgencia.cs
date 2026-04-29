using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    public class ConfiguracionComisionAgencia : Empresa
    {
        /// <summary>
        /// Codigo de configuración de la agencia
        /// </summary>
        public long CodigoConfiguracionAgencia { get; private set; }
        /// <summary>
        /// Codigo de configuración del producto
        /// </summary>
        public long CodigoConfiguracionProducto { get; private set; }
        /// <summary>
        /// Código de la agencia
        /// </summary>
        public string CodigoAgencia { get; private set; }
        /// <summary>
        /// Indocador de aplicar iperaciones libres
        /// </summary>
        public bool IndicadorAplicaOperacionesLibres { get; private set; }
        /// <summary>
        /// Codigo del estado
        /// </summary>
        public bool IndicadorEstado { get; private set; }
        /// <summary>
        /// fecha del registro
        /// </summary>
        public DateTime FechaRegistro { get; private set; }
        /// <summary>
        /// usuario que registro
        /// </summary>
        public string CodigoUsuarioRegistro { get; private set; }
        /// <summary>
        /// fecha de la ultima modificación
        /// </summary>
        public DateTime? FechaModificacion { get; private set; }
        /// <summary>
        /// ultimo usuario en modificar
        /// </summary>
        public string? CodigoUsuarioModificacion { get; private set; }
        /// <summary>
        /// Propiedad virtual que define la agencia
        /// </summary>
        public virtual Agencia Agencia { get; private set; }
    }
}
