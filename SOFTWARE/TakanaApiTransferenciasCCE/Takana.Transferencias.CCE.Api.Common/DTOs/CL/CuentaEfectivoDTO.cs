using Swashbuckle.AspNetCore.Annotations;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.CL
{
    public class CuentaEfectivoDTO
    {
        /// <summary>
        /// Número de la cuenta efectivo
        /// </summary>
        [SwaggerSchema("Número de la cuenta efectivo")]
        public string NumeroCuenta { get; set; }
        /// <summary>
        /// Descripción del titular de la cunta
        /// </summary>
        [SwaggerSchema("Descripción del titular de la cunta")]
        public string Titular { get; set; }
        /// <summary>
        /// Tipo de Producto de la cuenta
        /// </summary>
        [SwaggerSchema("Tipo de Producto de la cuenta")]
        public string TipoProductoInterno { get; set; }
        /// <summary>
        /// Descripción de la moneda de la cuenta
        /// </summary>
        [SwaggerSchema("Descripción de la moneda de la cuenta")]
        public string Moneda { get; set; }
        /// <summary>
        /// Tipo de Cuenta, individuak o mancomunada
        /// </summary>
        [SwaggerSchema("Tipo de Cuenta, individual o mancomunada")]
        public string TipoCuentaTitular { get; set; }
        /// <summary>
        /// Si la cuenta esta exonerada de impuestos
        /// </summary>
        [SwaggerSchema("Si la cuenta esta exonerada de impuestos")]
        public bool ExoneradoImpuestos { get; set; }
        /// <summary>
        /// Código del cliente
        /// </summary>
        [SwaggerSchema("Código del cliente")]
        public string CodigoCliente { get; set; }
        /// <summary>
        /// Indicador si es cuenta sueldo
        /// </summary>
        [SwaggerSchema("Indicador si es cuenta sueldo")]
        public bool IndicadorCuentaSueldo { get; set; }
        /// <summary>
        /// Monto remunerativo
        /// </summary>
        [SwaggerSchema("Monto remunerativo")]
        public decimal MontoRemunerativo { get; set; }
        /// <summary>
        /// Monto no remunerativo
        /// </summary>
        [SwaggerSchema("Monto no remunerativo")]
        public decimal MontoNoremunerativo { get; set; }
        /// <summary>
        /// Codigo del producto
        /// </summary>
        [SwaggerSchema("Codigo del producto")]
        public string CodigoProducto { get; set; }
        /// <summary>
        /// Alias del producto
        /// </summary>
        [SwaggerSchema("Alias del producto")]
        public string Alias { get; set; }
        /// <summary>
        /// Indica si el producto esta exonerado de comisiones
        /// </summary>
        [SwaggerSchema("Indica si el producto esta exonerado de comisiones")]
        public bool EsExoneradoCobroComisiones { get; set; }
        /// <summary>
        /// Nombre del producto
        /// </summary>
        [SwaggerSchema("Nombre del producto ")]
        public string? NombreProducto { get; set; }
        /// <summary>
        /// Código de la moneda
        /// </summary>
        [SwaggerSchema("Código de la moneda")]
        public string CodigoMoneda { get; set; }
        /// <summary>
        /// Simbolo de Moneda de Producto
        /// </summary>
        [SwaggerSchema("Simbolo de Moneda de Producto")]
        public string SimboloMonedaProducto { get; set; }
        /// <summary>
        /// Propiedad que define el codigo de cuenta interbancario
        /// </summary>
        [SwaggerSchema("Propiedad que define el codigo de cuenta interbancario")]
        public string CodigoCuentaInterbancario { get; set; }
        /// <summary>
        /// Propiedad que define el tipo de cuenta Individual o Mancomunada
        /// </summary>
        [SwaggerSchema("Propiedad que define el tipo de cuenta Individual o Mancomunada")]
        public string IndicadorTipoCuenta { get; set; }
        /// <summary>
        /// Nombres del cliente dueño de la cuenta
        /// </summary>
        [SwaggerSchema("Nombres del cliente dueño de la cuenta")]
        public string Nombres { get; set; }
        /// <summary>
        /// Apellido Materno del cliente dueño de la cuenta
        /// </summary>
        [SwaggerSchema("Apellido Materno del cliente dueño de la cuenta")]
        public string ApellidoMaterno { get; set; }
        /// <summary>
        /// Apellido Patenro del cliente dueño de la cuenta
        /// </summary>
        [SwaggerSchema("Apellido Patenro del cliente dueño de la cuenta")]
        public string ApellidoPaterno { get; set; }
        /// <summary>
        /// Valida si es mismo firmante que la cuenta de destino
        /// </summary>
        [SwaggerSchema("Valida si es mismo firmante que la cuenta de destino")]
        public bool EsMismoFirmante { get; set; }
        /// <summary>
        /// Indicador Transferencia inmediata
        /// </summary>
        [SwaggerSchema("Indicador Transferencia inmediata ")]
        public string? IndicadorTransferenciaCce { get; set; }
        /// <summary>
        /// Saldo Disponible
        /// </summary>
        [SwaggerSchema("Saldo Disponible")]
        public decimal SaldoDisponible { get; set; }
        /// <summary>
        /// Numero Documento
        /// </summary>
        [SwaggerSchema("Numero Documento ")]
        public string? NumeroDocumento { get; set; }
        /// <summary>
        /// Tipo documento del cliente originante
        /// </summary>
        [SwaggerSchema("Tipo documento del cliente originante ")]
        public TipoDocumentoTinDTO? TipoDocumentoOriginante { get; set; }
        /// <summary>
        /// Monto minimo de saldo que debe tener la cuenta 
        /// </summary>
        [SwaggerSchema("Monto minimo de saldo que debe tener la cuenta ")]
        public decimal MontoMinimo { get; set; }
        /// <summary>
        /// Indicador de cuenta exonerada de ITF
        /// </summary>
        [SwaggerSchema("Indicador de cuenta exonerada de ITF")]
        public bool EsExoneradaITF { get; set; }
        /// <summary>
        /// Indicador de cuenta sueldo
        /// </summary>
        [SwaggerSchema("Indicador de cuenta sueldo")]
        public bool EsCuentaSueldo { get; set; }

    }
}
