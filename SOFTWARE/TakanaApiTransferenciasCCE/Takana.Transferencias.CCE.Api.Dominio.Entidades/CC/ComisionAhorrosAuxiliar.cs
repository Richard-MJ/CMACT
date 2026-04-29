using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    public class ComisionAhorrosAuxiliar
    {
        /// <summary>
        /// Monto de la comisión
        /// </summary>
        public decimal MontoComision { get; private set; }
        /// <summary>
        /// Comfiguración de la comisión
        /// </summary>
        public ConfiguracionComision ConfiguracionComision { get; private set; }
        /// <summary>
        /// Inicializador de la clase
        /// </summary>
        /// <param name="configuracionComision">configuración de la comiisón</param>
        /// <param name="montoComision">monto de la comiisón</param>
        /// <returns>comisión auxliar creada</returns>
        public static ComisionAhorrosAuxiliar Crear(
            ConfiguracionComision configuracionComision,
            decimal montoComision)
        {
            if (configuracionComision == null)
                throw new Exception("No se ha encontrado una configuración de la comisión interbancaria.");

            return new ComisionAhorrosAuxiliar()
            {
                MontoComision = montoComision,
                ConfiguracionComision = configuracionComision,
            };
        }
    }
}
