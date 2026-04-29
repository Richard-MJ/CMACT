using Swashbuckle.AspNetCore.Annotations;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones
{
    /// <summary>
    /// Clase de datos para el resultado de la consulta de cuenta CCE
    /// </summary>
    public record ResultadoConsultaCuentaCCE
    {
        /// <summary>
        /// Código de cuenta de la transacción.
        /// </summary>
        [SwaggerSchema("Código de cuenta de la transacción.")]
        public string CodigoCuentaTransaccion { get; set; }
        /// <summary>
        /// Código de la entidad originante de la transacción.
        /// </summary>
        [SwaggerSchema("Código de la entidad originante de la transacción.")]
        public string CodigoEntidadOriginante { get; set; }
        /// <summary>
        /// Código de la entidad receptora asociada.
        /// </summary>
        [SwaggerSchema("Código de la entidad receptora asociada.")]
        public string CodigoEntidadReceptora { get; set; }
        /// <summary>
        /// Fecha de creación de la transacción.
        /// </summary>
        [SwaggerSchema("Fecha de creación de la transacción.")]
        public string FechaCreacionTransaccion { get; set; }
        /// <summary>
        /// Hora de creación de la transacción.
        /// </summary>
        [SwaggerSchema("Hora de creación de la transacción.")]
        public string HoraCreacionTransaccion { get; set; }
        /// <summary>
        /// Número de referencia de la transacción.
        /// </summary>
        [SwaggerSchema("Número de referencia de la transacción.")]
        public string NumeroReferencia { get; set; }
        /// <summary>
        /// Trace de la transacción.
        /// </summary>
        [SwaggerSchema("Trace de la transacción.")]
        public string Trace { get; set; }
        /// <summary>
        /// Canal utilizado para la transacción.
        /// </summary>
        [SwaggerSchema("Canal utilizado para la transacción.")]
        public string Canal { get; set; }
        /// <summary>
        /// Código de la moneda de la transacción.
        /// </summary>
        [SwaggerSchema("Código de la moneda de la transacción.")]
        public string CodigoMoneda { get; set; }
        /// <summary>
        /// Código de la transferencia asociada.
        /// </summary>
        [SwaggerSchema("Código de la transferencia asociada.")]
        public string CodigoTransferencia { get; set; }
        /// <summary>
        /// Criterio de plaza aplicado en la transacción.
        /// </summary>
        [SwaggerSchema("Criterio de plaza aplicado en la transacción.")]
        public string CriterioPlaza { get; set; }
        /// <summary>
        /// Tipo de persona del deudor en la transacción.
        /// </summary>
        [SwaggerSchema("Tipo de persona del deudor en la transacción.")]
        public string TipoPersonaDeudor { get; set; }
        /// <summary>
        /// Nombre del deudor en la transacción.
        /// </summary>
        [SwaggerSchema("Nombre del deudor en la transacción.")]
        public string NommbreDeudor { get; set; }
        /// <summary>
        /// Tipo de documento del deudor en la transacción.
        /// </summary>
        [SwaggerSchema("Tipo de documento del deudor en la transacción.")]
        public string TipoDocumentoDeudor { get; set; }
        /// <summary>
        /// Número de identidad del deudor en la transacción.
        /// </summary>
        [SwaggerSchema("Número de identidad del deudor en la transacción.")]
        public string NumeroIdentidadDeudor { get; set; }
        /// <summary>
        /// Número de celular del deudor en la transacción.
        /// </summary>
        [SwaggerSchema("Número de celular del deudor en la transacción.")]
        public string NumeroCelularDeudor { get; set; }
        /// <summary>
        /// Código de cuenta interbancaria del deudor en la transacción.
        /// </summary>
        [SwaggerSchema("Código de cuenta interbancaria del deudor en la transacción.")]
        public string CodigoCuentaInterbancariaDeudor { get; set; }
        /// <summary>
        /// Nombre del cliente receptor.
        /// </summary>
        [SwaggerSchema("Nombre del cliente receptor.")]
        public string NombreReceptor { get; set; }
        /// <summary>
        /// Dirección del receptor.
        /// </summary>
        [SwaggerSchema("Dirección del receptor.")]
        public string DireccionReceptor { get; set; }
        /// <summary>
        /// Número de teléfono del receptor.
        /// </summary>
        [SwaggerSchema("Número de teléfono del receptor.")]
        public string NumeroTelefonoReceptor { get; set; }
        /// <summary>
        /// Número de celular del receptor.
        /// </summary>
        [SwaggerSchema("Número de celular del receptor.")]
        public string NumeroCelularReceptor { get; set; }
        /// <summary>
        /// Código de cuenta interbancaria del receptor.
        /// </summary>
        [SwaggerSchema("Código de cuenta interbancaria del receptor.")]
        public string CodigoCuentaInterbancariaReceptor { get; set; }
        /// <summary>
        /// Código de tarjeta del receptor.
        /// </summary>
        [SwaggerSchema("Código de tarjeta del receptor.")]
        public string CodigoTarjetaReceptor { get; set; }
        /// <summary>
        /// Indicador ITF (Impuesto a las Transacciones Financieras).
        /// </summary>
        [SwaggerSchema("Indicador ITF (Impuesto a las Transacciones Financieras).")]
        public string IndicadorITF { get; set; }
        /// <summary>
        /// Plaza asociada a la transacción.
        /// </summary>
        [SwaggerSchema("Plaza asociada a la transacción.")]
        public string Plaza { get; set; }
        /// <summary>
        /// Tipo de transacción.
        /// </summary>
        [SwaggerSchema("Tipo de transacción.")]
        public string TipoTransaccion { get; set; }
        /// <summary>
        /// ID de la instrucción asociada.
        /// </summary>
        [SwaggerSchema("ID de la instrucción asociada.")]
        public string InstruccionId { get; set; }
        /// <summary>
        /// Tipo de documento del receptor.
        /// </summary>
        [SwaggerSchema("Tipo de documento del receptor.")]
        public string TipoDocumentoReceptor { get; set; }
        /// <summary>
        /// Número de identidad del receptor.
        /// </summary>
        [SwaggerSchema("Número de identidad del receptor.")]
        public string NumeroIdentidadReceptor { get; set; }
        /// <summary>
        /// Indicador de si el receptor es el mismo titular.
        /// </summary>
        [SwaggerSchema("Indicador de si el receptor es el mismo titular.")]
        public string MismoTitular { get; set; }
        /// <summary>
        /// Comisión asociada a la transacción.
        /// </summary>
        [SwaggerSchema("Comisión asociada a la transacción.")]
        public ComisionDTO Comision { get; set; }
        /// <summary>
        /// Indicador si es exonerada de comision.
        /// </summary>
        [SwaggerSchema("Indicador si es exonerada de comision.")]
        public bool EsExoneradoComision { get; set; }

    }

}
