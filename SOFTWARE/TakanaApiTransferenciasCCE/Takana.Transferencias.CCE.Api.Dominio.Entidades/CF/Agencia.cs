namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF
{
    /// <summary>
    /// Clase de Dominio que representa a las Agencias
    /// </summary>
    public class Agencia : Empresa
    {
        #region Constantes

        public const string Principal = "01";

        #endregion Constantes

        #region Propiedades
        /// <summary>
        /// Codigo de agencia
        /// </summary>
        public string CodigoAgencia { get; private set; }
        /// <summary>
        /// Nombre de agencia
        /// </summary>
        public string NombreAgencia { get; private set; }
        /// <summary>
        /// Activo
        /// </summary>
        public string Activo { get; private set; }
        /// <summary>
        /// Direccion de la agencia
        /// </summary>
        public string Direccion { get; private set; }
        /// <summary>
        /// Nombre de ciudad
        /// </summary>
        public string NombreCiudad { get; private set; }
        /// <summary>
        /// Codigo de ubigeo
        /// </summary>
        public string CodigoUbigeo { get; private set; }
        /// <summary>
        /// Estado de 
        /// </summary>
        public string Estado { get; private set; }
        #endregion
    }
}