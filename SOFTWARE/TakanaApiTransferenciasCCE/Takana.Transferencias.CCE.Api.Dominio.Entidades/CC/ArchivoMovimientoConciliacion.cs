namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    /// <summary>
    /// Se Declaran los objetos y sus tipos de datos de archivo conciliacion
    /// </summary>
    public class ArchivoMovimientoConciliacion 
    {
        #region Propiedades
        /// <summary>
        /// Identificador de archivo de movimiento
        /// </summary>
        public int IdArchivoMovimiento { get; private set; }
        /// <summary>
        /// Identificacion del archivo de lote
        /// </summary>
        public int IdArchivoLote { get; private set; }
        /// <summary>
        /// Codigo de cuenta interbancara del cliente originante
        /// </summary>
        public string CodigoCuentaOrigen { get; private set; }
        /// <summary>
        /// Codigo de cuenta interbancaria o tarjeta de credito del cliente receptor
        /// </summary>
        public string CodigoCuentaReceptor { get; private set; }
        /// <summary>
        /// Entidad receptor
        /// </summary>
        public string EntidadReceptor { get; private set; }
        /// <summary>
        /// Monto del movimiento
        /// </summary>
        public decimal Monto { get; private set; }
        /// <summary>
        /// Monto de comisión del movimiento
        /// </summary>
        public decimal MontoComision { get; private set; }
        /// <summary>
        /// Signo de comision
        /// </summary>
        public string SignoComision { get; private set; }
        /// <summary>
        /// Tipo de transfencia
        /// </summary>
        public string TipoTransferencia { get; private set; }
        /// <summary>
        /// Fecha y hora del movimiento
        /// </summary>
        public DateTime FechaHora { get; private set; }
        /// <summary>
        /// Canal donde se realizo el movimiento
        /// </summary>
        public string Canal { get; private set; }
        /// <summary>
        /// Codigo de trace
        /// </summary>
        public string ReferenciaTransferencia { get; private set; }
        /// <summary>
        /// Identificador de instruccion
        /// </summary>
        public string IdentificadorTransaccion { get; private set; }
        /// <summary>
        /// Codigo de proceso
        /// </summary>
        public string CodigoProceso { get; private set; }
        /// <summary>
        /// Estado del movimiento Aprobado o Rechazado
        /// </summary>
        public string Estado { get; private set; }
        /// <summary>
        /// Codigo de sistema
        /// </summary>
        public string CodigoSistema { get; private set; }
        #endregion
    }
}