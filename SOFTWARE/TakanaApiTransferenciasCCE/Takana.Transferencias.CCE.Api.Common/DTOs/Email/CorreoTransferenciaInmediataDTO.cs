namespace Takana.Transferencias.CCE.Api.Common.DTOs.Email
{
    public class CorreoTransferenciaInmediataDTO : CorreoGeneralDTO
    {
        /// <summary>
        /// Indicador de transaccion si es entrante o saliente
        /// </summary>
        public string IndicadorTransaccion { get; set; }
        /// <summary>
        /// Nombre de cliente de origen
        /// </summary>
        public string NombreClienteOrigen { get; set; }
        /// <summary>
        /// Número de cuenta de origen
        /// </summary>
        public string NumeroCuentaOrigen { get; set; }
        /// <summary>
        /// Símbolo de moneda de origen
        /// </summary>
        public string SimboloMoneda { get; set; }
        /// <summary>
        /// Símbolo de moneda de origen
        /// </summary>
        public string DescripcionMoneda { get; set; }
        /// <summary>
        /// Codigo del canal
        /// </summary>
        public string CodigoCanal { get; set; }
        /// <summary>
        /// Monto de origen
        /// </summary>
        public decimal MontoTransferencia { get; set; }
        /// <summary>
        /// Monto ITF
        /// </summary>
        public decimal MontoItf { get; set; }
        /// <summary>
        /// Monto de comisión
        /// </summary>
        public decimal MontoComision { get; set; }
        /// <summary>
        /// Nombre cliente de destino
        /// </summary>
        public string NombreClienteDestino { get; set; }
        /// <summary>
        /// Tipo de documento del cliente destino
        /// </summary>
        public string TipoDocumentoDestino { get; set; }
        /// <summary>
        /// Número de documento del cliente destino
        /// </summary>
        public string NumeroDocumentoDestino { get; set; }
        /// <summary>
        /// Numero de cuenta de interbancaria de origen
        /// </summary>
        public string CuentaInterbancariaOrigen { get; set; }
        /// <summary>
        /// Cuenta de destino
        /// </summary>
        public string CuentaInterbancariaDestino { get; set; }
        /// <summary>
        /// Número de tarjeta
        /// </summary>
        public string NumeroTarjetaDestino { get; set; }
        /// <summary>
        /// Código del tipo de transferencia
        /// </summary>
        public string TipoTransferencia { get; set; }
        /// <summary>
        /// Nombre de banco de origen
        /// </summary>
        public string NombreBancoOrigen { get; set; }
        /// <summary>
        /// Nombre de banco de destino
        /// </summary>
        public string NombreBancoDestino { get; set; }
        /// <summary>
        /// Numero de Celular de origen
        /// </summary>
        public string? CelularOrigen { get; set; }
        /// <summary>
        /// Numero de Celular de destino
        /// </summary>
        public string? CelularDestino { get; set; }
    }
}
