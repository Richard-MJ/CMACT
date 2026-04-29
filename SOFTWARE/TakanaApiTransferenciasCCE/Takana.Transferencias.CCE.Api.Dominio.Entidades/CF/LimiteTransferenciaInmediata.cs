namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF
{
    public class LimiteTransferenciaInmediata 
    {
        /// <summary>
        /// Identificador de limite de transferencia
        /// </summary>
        public int IdLimiteTransferencia { get; private set; }
        /// <summary>
        /// Codigo de canal
        /// </summary>
        public string CodigoCanal { get; private set; }
        /// <summary>
        /// Identificador de tipo de transferencia
        /// </summary>
        public int IdTipoTransferencia { get; private set; }
        /// <summary>
        /// Codigo de moneda
        /// </summary>
        public string CodigoMoneda { get; private set; }
        /// <summary>
        /// Moneda limite minimo
        /// </summary>
        public decimal MontoLimiteMinimo { get; private set; }
        /// <summary>
        /// Monto de limite maximo
        /// </summary>
        public decimal MontoLimiteMaximo { get; private set; }
        /// <summary>
        /// Estado de limite
        /// </summary>
        public string EstadoLimite { get; private set; }
        /// <summary>
        /// Entidad de tipo de transferencia
        /// </summary>
        public virtual TipoTransferencia TipoTransferencia { get; private set; }
    }
}