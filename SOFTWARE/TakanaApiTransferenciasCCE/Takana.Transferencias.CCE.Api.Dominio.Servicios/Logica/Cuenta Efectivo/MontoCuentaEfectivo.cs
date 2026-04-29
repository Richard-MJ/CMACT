namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Logica.Cuenta_Efectivo
{
    /// <summary>
    /// Clase encargada del monto de cuenta efectivo
    /// </summary>
    public class MontoCuentaEfectivo
    {
        /// <summary>
        /// Parte del monto que es no remunerativa.
        /// </summary>
        public decimal NoRemunerativo { get; }
        /// <summary>
        /// Parte del monto que es remunerativa.
        /// </summary>
        public decimal Remunerativo { get; }

        /// <summary>
        /// Constructor de un monto de cuenta efectivo.
        /// </summary>
        /// <param name="noRemunerativo">Parte no remunerativa del monto.</param>
        /// <param name="remunerativo">Parte remunerativa del monto.</param>
        private MontoCuentaEfectivo(decimal noRemunerativo, decimal remunerativo)
        {
            NoRemunerativo = noRemunerativo;
            Remunerativo = remunerativo;
        }

        /// <summary>
        /// Crea un monto de cuenta efectivo.
        /// </summary>
        /// <param name="noRemunerativo">Parte no remunerativa del monto a crear.</param>
        /// <param name="remunerativo">Parte remunerativa del monto a crear.</param>
        /// <returns>Monto cuenta fectivo creada.</returns>
        public static MontoCuentaEfectivo Crear(decimal noRemunerativo, decimal remunerativo)
        {
            return new MontoCuentaEfectivo(noRemunerativo, remunerativo);
        }
        /// <summary>
        /// Monto remunerativo y no remunerativo
        /// </summary>
        public decimal Monto => NoRemunerativo + Remunerativo;
    }
}
