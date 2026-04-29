namespace Takana.Transferencias.CCE.Api.Common.ConsultasCuentas
{
    /// <summary>
    /// Clase de datos del cliente receptor de Cuenta de Transferencia Inmediata
    /// </summary>
    public record ClienteReceptorDTO
    {
        /// <summary>
        /// Codigo de Cuenta Interbancaria del cliente receptor
        /// </summary>
        public string CodigoCuentaInterbancaria { get; set; }
        /// <summary>
        /// Nombre del cliente receptor
        /// </summary>
        public string NombreCliente { get; set; }
        /// <summary>
        /// Numero de documento del cliente receptor
        /// </summary>
        public string NumeroDocumento { get; set; }
        /// <summary>
        /// Tipo de documento del cliente receptor
        /// </summary>
        public string TipoDocumento { get; set; }
        /// <summary>
        /// Tipo de documento del cliente receptor segun la CCE
        /// </summary>
        public string TipoDocumentoCCE { get; set; }
        /// <summary>
        /// Direccion del cliente receptor
        /// </summary>
        public string Direccion { get; set; }
        /// <summary>
        /// Numero de telefono del cliente receptor
        /// </summary>
        public string NumeroTelefono { get; set; }
        /// <summary>
        /// Numero de celular del cliente receptor
        /// </summary>
        public string NumeroCelular { get; set; }
        /// <summary>
        /// Estado de cuenta del cliente receptor
        /// </summary>
        public string EstadoCuenta { get; set; }
        /// <summary>
        /// Codigo de moneda de la cuenta del cliente receptor
        /// </summary>
        public string CodigoMoneda { get; set; }
        /// <summary>
        /// Codigo de Moneda Iso del cliente receptor
        /// </summary>
        public string CodigoMonedaISO { get; set; }  
        /// <summary>
        /// Indicador de cuenta valida del cliente receptor
        /// </summary>
        public string IndicadorCuentaValida { get; set; }

    }
}
