using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    public class ConfiguracionComisionMoneda : Empresa
    {
        /// <summary>
        /// Codigo de configuración moneda
        /// </summary>
        public long CodigoConfiguracionMoneda { get; private set; }
        /// <summary>
        /// Codigo de configuración
        /// </summary>
        public long CodigoConfiguracion { get; private set; }
        /// <summary>
        /// Código de la moneda
        /// </summary>
        public string CodigoMoneda { get; private set; }
        /// <summary>
        /// Monto base de la transacción
        /// </summary>
        public decimal MontoBase { get; private set; }
        /// <summary>
        /// Monto fijo de la transacción
        /// </summary>
        public decimal MontoFijo { get; private set; }
        /// <summary>
        /// Monto máximo de la transacción
        /// </summary>
        public decimal MontoMaximo { get; private set; }
        /// <summary>
        /// Porcentaje de transacción
        /// </summary>
        public decimal PorcentajeTransaccion { get; private set; }
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
        public string CodigoUsuarioModificacion { get; private set; }
        /// <summary>
        /// Propiedad virtual que define la moneda
        /// </summary>
        public virtual Moneda Moneda { get; private set; }
    }
}
