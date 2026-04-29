namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    /// <summary>
    /// Clase de dominio de tipo de trama
    /// </summary>
    public class TipoTrama
    {
        /// <summary>
        /// Identificador de tipo de trama
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Descripcion de tipo de trama
        /// </summary>
        public string Descripcion { get; private set; }
        /// <summary>
        /// Tipo de la trama
        /// </summary>
        public string Tipo { get; private set; }
    }
}
