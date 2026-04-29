using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Logica.Cuenta_Efectivo
{
    /// <summary>
    /// Clase encargada de la logica de la cuenta efectivo Patrono
    /// </summary>
    public class LogicaCuentaEfectivoPatrono : LogicaCuentaEfectivo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cuentaEfectivo"></param>
        public LogicaCuentaEfectivoPatrono(CuentaEfectivo cuentaEfectivo)
            : base(cuentaEfectivo)
        {
        }
    }
}
