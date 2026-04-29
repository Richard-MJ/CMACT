namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    /// <summary>
    /// Clase de dominio que repreenta a la entidad Cuenta afiliada
    /// </summary>
    public class EstadoCuenta
    {
        /// <summary>
        /// Codigo de estado de la cuenta
        /// </summary>
        public string CodigoEstado { get; set; }
        /// <summary>
        /// Descripcion del estado de la cuenta
        /// </summary>
        public string DescripcionEstado { get; set; }
        /// <summary>
        /// Indicador de estado de vigencia
        /// </summary>
        public string IndicadorEstadoVigente { get; set; }
    }
}
