namespace Takana.Transferencias.CCE.Api.Common.DTOs.CL
{
    /// <summary>
    /// Clase del cliente originante de tranferencia Interbancaria Inmediata
    /// </summary>
    public class ClienteExternoDTO
    {
        #region Constantes
        public enum Persona
        {
            Natural = 1,
            Juridica = 2,
        }
        public bool EsPersonaJuridica => TipoPersona == ((int)Persona.Juridica).ToString();
        public bool EsClienteExterno => TipoCliente == ClienteExterno;
        public const string TipoPersonaNaturalCCE = "N";
        public const string TipoPersonaJuridicaCCE = "J";
        public const string Cliente = "CL";
        public const string ClienteExterno = "EX";
        #endregion

        /// <summary>
        /// Codigo de cliente
        /// </summary>
        public string CodigoCliente { get; set; }
        /// <summary>
        /// Codigo de de cuenta interbancaria
        /// </summary>
        public string CodigoCuentaInterbancaria { get; set; }
        /// <summary>
        /// Nombres del cliente originante
        /// </summary>
        public string Nombres { get; set; }
        /// <summary>
        /// Apellido Paterno
        /// </summary>
        public string ApellidoPaterno { get; set; }
        /// <summary>
        /// Apellido Materno
        /// </summary>
        public string ApellidoMaterno { get; set; }
        /// <summary>
        /// Numero de Documento
        /// </summary>
        public string NumeroDocumento { get; set; }
        /// <summary>
        /// Numero de Documento Ruc
        /// </summary>
        public string NumeroDocumentoRuc { get; set; }        
        /// <summary>
        /// Codigo de tipo de documento
        /// </summary>
        public string CodigoTipoDocumento { get; set; }
        /// <summary>
        /// Tipo de persona
        /// </summary>
        public string TipoPersona { get; set; }
        /// <summary>
        /// Tipo de cliente
        /// </summary>
        public string TipoCliente { get; set; }
        /// <summary>
        /// Fecha de nacimiento
        /// </summary>
        public DateTime? FechaNacimiento { get; set; }
        /// <summary>
        /// Nacionalidad del cliente
        /// </summary>
        public string Nacionalidad { get; set; }
        /// <summary>
        /// Telefono del cliente
        /// </summary>
        public string Telefono { get; set; }
        /// <summary>
        /// Codigo de cliente
        /// </summary>
        public int? CodigoResidencia { get; set; }
        /// <summary>
        /// Codigo de departamento
        /// </summary>
        public string CodigoDepartamento { get; set; }
        /// <summary>
        /// Codigo de distrito
        /// </summary>
        public string CodigoDistrito { get; set; }
        /// <summary>
        /// Codigo de provincia
        /// </summary>
        public string CodigoProvincia { get; set; }
        /// <summary>
        /// Codigo de ocupacion
        /// </summary>
        public string CodigoOcupacion { get; set; }
        /// <summary>
        /// Codigo de ocupacion
        /// </summary>
        public int CodigoCargo { get; set; }
        /// <summary>
        /// Codigo de actividad
        /// </summary>
        public string CodigoActividad { get; set; }
        /// <summary>
        /// Codigo de usuario
        /// </summary>
        public string CodigoUsuario { get; set; }
        /// <summary>
        /// Codigo de subactividad
        /// </summary>
        public string CodigoSubactividad { get; set; }
        /// <summary>
        /// Detalle de direccion
        /// </summary>
        public string DetalleDireccion { get; set; }
    }
}
