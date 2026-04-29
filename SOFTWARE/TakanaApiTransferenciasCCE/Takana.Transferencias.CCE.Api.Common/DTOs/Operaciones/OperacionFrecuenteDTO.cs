namespace Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones
{
    /// <summary>
    /// DTO de afiliacion
    /// </summary>
    public class OperacionFrecuenteDTO
    {
        /// <summary>
        /// Número de la cuenta de origen
        /// </summary>
        public string NumeroCuenta { get; set; }
        /// <summary>
        /// Nombre que toma la operacion frecuente
        /// </summary>
        public string NombreOperacionFrecuente { get; set; }
        /// <summary>
        /// Cuenta de destino CCI
        /// </summary>
        public string CodigoCuentaInterbancariaReceptor { get; set; }
        /// <summary>
        /// Nombres del cliente beneficiario
        /// </summary>
        public string NombreDestino { get; set; }
        /// <summary>
        /// Identifica si el cliente de destino es el mismo que hace la operacion
        /// </summary>
        public bool MismoTitularEnDestino { get; set; }
        /// <summary>
        /// Propiedad que distingue si es persona natural
        /// </summary>
        public int TipoDocumento { get; set; }
        /// <summary>
        /// Indica el Tipo de Operacion Frecuente
        /// </summary>
        public int TipoOperacionFrecuente { get; set; }
        /// <summary>
        /// Número de documento del destinatario
        /// </summary>
        public string NumeroDocumento { get; set; }
    }
}