namespace Takana.Transferencias.CCE.Api.Common.DTOs.CC
{
    /// <summary>
    /// DTO de afiliacion
    /// </summary>
    public class AfiliacionServicioDTO
    {
        /// <summary>
        /// Número de tarjeta a afiliar
        /// </summary>
        public string NumeroTarjeta { get; set; }
        /// <summary>
        /// Número de cuenta efectivo a afiliar
        /// </summary>
        public string NumeroCuenta { get; set; }
        /// <summary>
        /// Codigo de servicio a afiliar
        /// </summary>
        public string CodigoServicio { get; set; }
        /// <summary>
        /// Fecha de operacion
        /// </summary>
        public DateTime FechaOperacion { get; set; }
        /// <summary>
        /// Correo electronico
        /// </summary>
        public string CorreoElectronico { get; set; }
        /// <summary>
        /// Nombre de cliente
        /// </summary>
        public string NombreCliente { get; set; }
        /// <summary>
        /// Solo el nombre del cliente
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// Apellido paterno del cliente
        /// </summary>
        public string? ApellidoPaterno { get; set; }
        /// <summary>
        /// Apellido materno del cliente
        /// </summary>
        public string? ApellidoMaterno { get; set; }
        /// <summary>
        /// Nombre de producto de cuenta afiliada
        /// </summary>
        public string NombreProductoCuentaAfiliada { get; set; }
        /// <summary>
        /// Correo electronico del remitente
        /// </summary>
        public string CorreoElectronicoRemitente { get; set; }
        /// <summary>
        /// Codigo de moneda de la cuenta
        /// </summary>
        public string CodigoMonedaCuenta { get; set; }
        /// <summary>
        /// Numero de afiliacion
        /// </summary>
        public int NumeroAfiliacion { get; set; }
    }
}
