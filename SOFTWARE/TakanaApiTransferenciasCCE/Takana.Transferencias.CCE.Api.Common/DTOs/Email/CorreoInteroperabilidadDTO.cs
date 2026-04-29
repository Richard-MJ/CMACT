namespace Takana.Transferencias.CCE.Api.Common.DTOs.Email
{
    public class CorreoInteroperabilidadDTO : CorreoGeneralDTO
    {
        /// <summary>
        /// Establece la moneda utilizada en la transacción.
        /// </summary>
        public string Moneda { get; set; }

        /// <summary>
        /// Establece una descripción de la transacción.
        /// </summary>
        public string DescripcionTransaccion { get; set; }

        /// <summary>
        /// Establece el estado actual de la transacción.
        /// </summary>
        public string Estado { get; set; }

        /// <summary>
        /// Establece la billetera de destino para la transacción.
        /// </summary>
        public string BilleteraDestino { get; set; }

        /// <summary>
        /// Establece el contacto asociado con la transacción.
        /// </summary>
        public string Contacto { get; set; }

        /// <summary>
        /// Establece el monto total de la operación.
        /// </summary>
        public decimal MontoOperacion { get; set; }

        /// <summary>
        /// Establece el monto de la comisión aplicada a la transacción.
        /// </summary>
        public decimal MontoComision { get; set; }

        /// <summary>
        /// Establece el monto de la ITF (Impuesto a las Transacciones Financieras) aplicado a la transacción.
        /// </summary>
        public decimal MontoItf { get; set; }

        /// <summary>
        /// Establece la ubicación relacionada con la transacción.
        /// </summary>
        public string Ubicacion { get; set; }
    }
}
