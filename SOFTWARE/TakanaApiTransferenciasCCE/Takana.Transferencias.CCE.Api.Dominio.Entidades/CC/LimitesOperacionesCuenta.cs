namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    /// <summary>
    /// Entidad que representa los limites de operaciones definidos por usuario para cuenta
    /// </summary>
    public class LimitesOperacionesCuenta
    {
        /// <summary>
        /// Enum de los tipos de operacion de limite para cuentas por canales electronicos
        /// </summary>
        public enum TipoOperacionLimite : int
        {
            ReinicioLimites = 0,
            LimiteTransacciones = 1,
            MontoLimite = 2,
            MontoMaxSemanal = 3
        }

        /// <summary>
        /// Identificador de Limite de cuenta de cliente
        /// </summary>
        public int IdLimite { get; private set; }
        /// <summary>
        /// Codigo de empresa
        /// </summary>
        public string CodigoEmpresa { get; private set; }
        /// <summary>
        /// Numero de cuenta
        /// </summary>
        public string NumeroCuenta { get; private set; }
        /// <summary>
        /// Identificador de tipo de limite para canales
        /// </summary>
        public int IdTipoLimite { get; private set; }
        /// <summary>
        /// Valor de limite
        /// </summary>
        public decimal ValorLimite { get; private set; }
        /// <summary>
        /// Indicador de canal
        /// </summary>
        public string IndicadorCanal { get; private set; }
        /// <summary>
        /// Fecha en la que se registro el limite
        /// </summary>
        public DateTime FechaRegistro { get; private set; }
        /// <summary>
        /// Codigo del usuario que agrego el limite
        /// </summary>
        public string IndicadorEstado { get; private set; }
        /// <summary>
        /// Datos de cuenta efectivo
        /// </summary>
        public virtual CuentaEfectivo Cuenta { get; set; }
    }
}
