using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using static Takana.Transferencias.CCE.Api.Dominio.Entidades.CF.Moneda;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios
{
    /// <summary>
    /// Servicio de contorlde cajero
    /// </summary>
    public class ServicioDominioCajero
    {
        #region Métodos
        
        /// <summary>
        /// Obtiene el numero de asiento contable para la comision con el tipo de moneda
        /// </summary>
        /// <param name="codigoMoneda">Codigo de moneda de la transferencia</param>
        /// <returns>Numero de cuenta</returns>
        public static string ObtenerCodigoCuentaContableComision(string codigoMoneda)
        {
            return codigoMoneda == ((int)MonedaCodigo.Soles).ToString()
               ? ParametroPorEmpresa.CodigoCuentaContableComisionSoles
               : ParametroPorEmpresa.CodigoCuentaContableComisionDolares;
        }
        #endregion
    }
}
