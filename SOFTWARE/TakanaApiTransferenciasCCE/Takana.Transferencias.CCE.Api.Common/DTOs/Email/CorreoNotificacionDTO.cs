namespace Takana.Transferencias.CCE.Api.Common.DTOs.Email
{
    public class CorreoNotificacionDTO : CorreoGeneralDTO
    {
        #region Propiedades
        /// <summary>
        /// Identificador de mensaje
        /// </summary>
        public int IdentificadorMensaje { get; set; }
        /// <summary>
        /// Identificador de trama
        /// </summary>
        public int IdentificadorTrama { get; set; }
        /// <summary>
        /// Codigo de mensaje
        /// </summary>
        public string? CodigoMensaje { get; set; }
        /// <summary>
        /// Fecha de mensaje
        /// </summary>
        public DateTime? FechaMensaje { get; set; }
        /// <summary>
        /// Fecha de conciliacion
        /// </summary>
        public DateTime? FechaConciliacion { get; set; }
        /// <summary>
        /// Fecha nueva de liquidacion
        /// </summary>
        public DateTime? FechaNuevaLiquidacion { get; set; }
        /// <summary>
        /// Fecha anterior de liquidacion
        /// </summary>
        public DateTime? FechaAnteriorLiquidacion { get; set; }
        /// <summary>
        /// Descripcion de mensaje de identificacion
        /// </summary>
        public string? DescripcionMensajeIdentificacion { get; set; }
        /// <summary>
        /// Clase de mensaje
        /// </summary>
        public string? ClaseMensaje { get; set; }
        /// <summary>
        /// Codigo de moneda
        /// </summary>
        public string? CodigoMoneda { get; set; }
        /// <summary>
        /// Codigo de entidad
        /// </summary>
        public string? CodigoEntidad { get; set; }
        /// <summary>
        /// Nombre de entidad
        /// </summary>
        public string? NombreEntidad { get; set; }
        /// <summary>
        /// Saldo de balance
        /// </summary>
        public decimal? SaldoBalance { get; set; }
        /// <summary>
        /// Saldo Minimo
        /// </summary>
        public decimal? SaldoMinimo { get; set; }
        /// <summary>
        /// Saldo Normal
        /// </summary>
        public decimal? SaldoNormal { get; set; }
        /// <summary>
        /// SaldoUmbral
        /// </summary>
        public decimal? SaldoUmbral { get; set; }
        /// <summary>
        /// Saldo nuevo
        /// </summary>
        public decimal? SaldoNuevo { get; set; }
        /// <summary>
        /// Saldo anterior
        /// </summary>
        public decimal? SaldoAnterior { get; set; }
        /// <summary>
        /// Descripcion de mensaje
        /// </summary>
        public string? DescripcionMensaje { get; set; }
        /// <summary>
        /// Estado de nueva liquidacion
        /// </summary>
        public string? EstadoNuevoLiquidacion { get; set; }
        /// <summary>
        /// Estado de anterior liquidacion
        /// </summary>
        public string? EstadoAnteriorLiquidacion { get; set; }
        /// <summary>
        /// Numero credito recibidas aceptadas
        /// </summary>
        public string? NumeroCreditosRecibidasAceptadas { get; set; }
        /// <summary>
        /// Total creditos recibidos aceptados
        /// </summary>
        public decimal? TotalCreditosRecibidasAceptadas { get; set; }
        /// <summary>
        /// Numero de creditos recibidos rechazados
        /// </summary>
        public string? NumeroCreditosRecibidasRechazadas { get; set; }
        /// <summary>
        /// Total de creditos recibidos rechazados
        /// </summary>
        public decimal? TotalCreditosRecibidasRechazadas { get; set; }
        /// <summary>
        /// Numero de creditos enviados aceptados
        /// </summary>
        public string? NumeroCreditosEnviadasAceptadas { get; set; }
        /// <summary>
        /// Total de creditos enviados aceptados
        /// </summary>
        public decimal? TotalCreditosEnviadasAceptadas { get; set; }
        /// <summary>
        /// Numero de creditos enviados rechazados
        /// </summary>
        public string? NumeroCreditosEnviadasRechazadas { get; set; }
        /// <summary>
        /// Total de creditos enviados rechazados
        /// </summary>
        public decimal? TotalCreditosEnviadasRechazadas { get; set; }
        /// <summary>
        /// Valor de concilicacion
        /// </summary>
        public decimal? ValorConciliacion { get; set; }
        /// <summary>
        /// Numero anterior de conciliacion realizada
        /// </summary>
        public string? NumeroAnteriorConciliacionRealizada { get; set; }
        /// <summary>
        /// valor bruto realizado
        /// </summary>
        public decimal? ValorBrutoRealizada { get; set; }
        /// <summary>
        /// Numero de anterior conciliacion reduccion
        /// </summary>
        public string? NumeroAnteriorConciliacionReduccion { get; set; }
        /// <summary>
        /// Valor de bruto reduccion
        /// </summary>
        public decimal? ValorBrutoReduccion { get; set; }
        /// <summary>
        /// Estado de mensaje
        /// </summary>
        public string? EstadoMensaje { get; set; }
        /// <summary>
        /// Codigo de sistema
        /// </summary>
        public string? CodigoSistema { get; set; }
        /// <summary>
        /// Indicador de estado
        /// </summary>
        public string? IndicadorEstado { get; set; }
        /// <summary>
        /// Codigo de usuario registro
        /// </summary>
        public string? CodigoUsuarioRegistro { get; set; }
        /// <summary>
        /// Codigo de usuario modifico
        /// </summary>
        public string? CodigoUsuarioModifico { get; set; }
        /// <summary>
        /// Fecha de registro
        /// </summary>
        public DateTime FechaRegistro { get; set; }
        /// <summary>
        /// Fecha de modificacion
        /// </summary>
        public DateTime? FechaModifico { get; set; }

        #endregion Propiedades
    }
}