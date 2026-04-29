namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF
{
    /// <summary>
    /// Se Declaran los objetos y sus tipos de datos de archivo conciliacion
    /// </summary>
    public class TipoReporte
    {
        /// <summary>
        /// Identificador del Tipo Reporte
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Descripcion de Tipo de Reporte
        /// </summary>
        public string Descripcion { get; private set; }
        /// <summary>
        /// Codigo de anexo
        /// </summary>
        public string CodigoAnexo { get; private set; }
        /// <summary>
        /// Tipo de frecuencia (Diario o Mensual)
        /// </summary>
        public string TipoFrecuencia { get; private set; }
    }
}