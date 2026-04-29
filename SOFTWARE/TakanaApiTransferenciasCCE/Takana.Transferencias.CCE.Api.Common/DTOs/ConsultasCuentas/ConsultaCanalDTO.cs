using System.ComponentModel.DataAnnotations;
using Takana.Transferencias.CCE.Api.Common.DTOs;

namespace Takana.Transferencias.CCE.Api.Common.ConsultasCuentas
{
    /// <summary>
    /// Clase de datos que recibe como entra este API desde un canal
    /// </summary>
    public record ConsultaCanalDTO : CanalDTO
    {
        /// <summary>
        /// Identificador del equipo
        /// </summary>
        [Required]
        public string IdTerminal {get; set;}
        /// <summary>
        /// Número de Documento del Cliente Originante
        /// </summary>
        [Required]
        public string IdDeudor {get; set;}
        /// <summary>
        /// Nombre del cliente originante
        /// </summary>
        [Required]
        public string NombreDeudor {get; set;}
        /// <summary>
        /// Tipo de Documento del Cliente Originante.
        /// </summary>
        public int TipoDocumentoDeudor {get; set;}
        /// <summary>
        /// Numero de telefono del Cliente Originante
        /// </summary>
        public string? NumeroTelefonoDeudor {get; set;}
        /// <summary>
        /// Direccion del Cliente Originante
        /// </summary>
        public string? DireccionDeudor {get; set;}
        /// <summary>
        /// Numero de celular del Cliente Originante
        /// </summary>
        public string? NumeroCelularDeudor {get; set;}
        /// <summary>
        /// Codigo del tipo de transaccion de TIN
        /// </summary>
        [Required]
        public string TipoTransaccion {get; set;}
        /// <summary>
        /// Tipo de Persona del Cliente Originante
        /// </summary>
        [Required]
        public string TipoPersonaDeudor {get; set;}
        /// <summary>
        /// Código de Moneda de la Transacción.
        /// </summary>
        [Required]
        public string Moneda {get; set;}
        /// <summary>
        /// CCI del cliente receptor.
        /// </summary>
        public string? AcreedorCCI {get; set;}
        /// <summary>
        /// Numero de tarjeta de credio del cliente receptor
        /// </summary>
        public string? TarjetaCreditoAcreedor {get; set;}
        /// <summary>
        /// Codigo de la Entidad Originante
        /// </summary>
        
        public string? EntidadOriginante { get; set; }
        /// <summary>
        /// Codigo de la Entidad Recepetora
        /// </summary>
        
        public string? EntidadReceptora { get; set; }
        /// <summary>
        /// Numero de cuenta del Cliente Originante.
        /// </summary>
        [Required]
        public string NumeroCuenta { get; set; }
        /// <summary>
        /// Indicador de cuenta valida o no para este tipo de transferencia
        /// </summary>
        public bool IndicadorCuentaValidaTransaccion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IndicadorCuentaMancomunada{ get; set; }
        /// <summary>
        /// Indicadro de saldo valido de la cuenta
        /// </summary>
        public bool IndicadorSaldoValido{ get; set; }
        /// <summary>
        /// CCI del Cliente Originante.
        /// </summary>
        [Required]
        public string CodigoCuentaInterbancarioOriginante { get; set; }
        /// <summary>
        /// Numero celular del receptor para interoperabilidad
        /// </summary>
        public string? NumeroCelularReceptor { get; set; }
        /// <summary>
        /// Valor proxy para interoperabilidad
        /// </summary>
        public string? ValorProxy { get; set;}
        /// <summary>
        /// Tipo de proxy para interoperabilidad
        /// </summary>
        public string? TipoProxy { get; set; }

    }
}

