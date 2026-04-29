using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Logica.Cuenta_Efectivo
{
    /// <summary>
    /// Clase encargada de la logica de la cuenta efectivo Devoluciones
    /// </summary>
    public class LogicaCuentaEfectivoDevoluciones : LogicaCuentaEfectivo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cuentaEfectivo"></param>
        public LogicaCuentaEfectivoDevoluciones(CuentaEfectivo cuentaEfectivo)
           : base(cuentaEfectivo)
        {
        }
    }
}
