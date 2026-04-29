namespace Takana.Transferencias.CCE.Api.Common.DTOs.CF
{
    public class LimiteTransferenciaDTO
    {
        /// <summary>
        /// Código del tipo de transferencia
        /// </summary>
        public string CodigoTipoTransferencia { get; set; }
        /// <summary>
        /// Codigo de moneda
        /// </summary>
        public string CodigoMoneda { get; set; }
        /// <summary>
        /// Monto maximo del limite
        /// </summary>
        public decimal MontoMaximo { get; set; }
        /// <summary>
        /// Monto minimo del limite
        /// </summary>
        public decimal MontoMinimo { get; set; }
    }
}
