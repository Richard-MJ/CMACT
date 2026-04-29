using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Logica.Cuenta_Efectivo
{
    /// <summary>
    /// Clase encargada de la logica de la cuenta efectivo Futuro
    /// </summary>
    public class LogicaCuentaEfectivoFuturo : LogicaCuentaEfectivo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cuentaEfectivo"></param>
        public LogicaCuentaEfectivoFuturo(CuentaEfectivo cuentaEfectivo)
            : base(cuentaEfectivo)
        {
        }
    }
}
