namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.SG
{
    /// <summary>
    /// Clase que representa a la entidad de dominio del sistema de cliente
    /// </summary>
    public class SistemaCliente
    {
        /// <summary>
        /// Identificador del sistema.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Indicador del estado.
        /// </summary>
        public string IndicadorEstado { get; private set; }

        /// <summary>
        /// Llave secreta.
        /// </summary>
        public string IdSecreto { get; private set; }

        /// <summary>
        /// Direccion de origen permitido.
        /// </summary>
        public string DireccionOrigenPermitido { get; private set; }
    }
}