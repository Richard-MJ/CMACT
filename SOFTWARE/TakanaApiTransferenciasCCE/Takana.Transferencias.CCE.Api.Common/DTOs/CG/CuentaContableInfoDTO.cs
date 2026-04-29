using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.CG
{
    /// <summary>
    /// Datos de la cuenta contable
    /// </summary>
    public class CuentaContableInfoDTO
    {
        /// <summary>
        /// Número de cuenta contable
        /// </summary>
        public string NumeroCuenta { get; set; }
        /// <summary>
        /// Detalle que tendrá la cuenta en el asiento
        /// </summary>
        public string DetalleCuenta { get; set; }
        /// <summary>
        /// Tasa de cambio local
        /// </summary>
        public decimal TasaCambioLocal { get; set; }
        /// <summary>
        /// Tasa de cambio Cuenta
        /// </summary>
        public decimal TasaCambioCuenta { get; set; }
        /// <summary>
        /// Código tipo de cambio
        /// </summary>
        public string? CodigoTipoCambio { get; set; }
        /// <summary>
        /// Tipo de asiento para el cual será utilizada la cuenta (Crédito o débito)
        /// </summary>
        public string TipoAsiento { get; set; }
        /// <summary>
        /// Código de unidad ejecutara
        /// </summary>
        public string CodigoUnidadEjecutora { get; set; }
    }
}
