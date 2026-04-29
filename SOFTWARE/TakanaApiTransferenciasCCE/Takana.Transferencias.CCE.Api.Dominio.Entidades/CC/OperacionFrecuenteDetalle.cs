namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    /// <summary>
    /// Clase de dominio de tipo de cuenta grupo
    /// </summary>
    public class OperacionFrecuenteDetalle
    {
        /// <summary>
        /// Identificador numerico del detalle de la operacion frecuente
        /// </summary>
        public int NumeroDetalleOperacionFrecuente { get; private set; }
        /// <summary>
        /// Identificador numerico de la operacion frecuente
        /// </summary>
        public int NumeroOperacionFrecuente { get; private set; }
        /// <summary>
        /// Identificador numerico de la propiedad
        /// </summary>
        public int NumeroPropiedad { get; private set; }
        /// <summary>
        /// Valor que toma la propiedad especificada
        /// </summary>
        public string ValorPropiedad { get; private set; }

        /// <summary>
        /// Metodo estatico para crear el detalle de una operacion frecuente
        /// </summary>
        public static OperacionFrecuenteDetalle RegistrarDetalle(
            string valorPropiedad
            ,int identificadorPropiedad)
        {
            return new OperacionFrecuenteDetalle
            {
                ValorPropiedad = valorPropiedad,
                NumeroPropiedad = identificadorPropiedad
            };
        }
    }
}
