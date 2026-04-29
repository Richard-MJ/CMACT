using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Logica.Cuenta_Efectivo
{
    /// <summary>
    /// Clase encargada de la logica de la cuenta efectivo Normal
    /// </summary>
    public class LogicaCuentaEfectivoNormal : LogicaCuentaEfectivo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cuentaEfectivo"></param>
        public LogicaCuentaEfectivoNormal(CuentaEfectivo cuentaEfectivo)
           : base(cuentaEfectivo)
        {
        }
    }
}
