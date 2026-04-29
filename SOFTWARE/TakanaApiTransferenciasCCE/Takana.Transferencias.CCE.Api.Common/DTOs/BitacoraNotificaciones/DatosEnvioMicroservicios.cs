using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.BitacoraNotificaciones
{
    /// <summary>
    /// Datos de envio de microservicios
    /// </summary>
    public class DatosEnvioMicroservicios
    {
        /// <summary>
        /// fecha de sistema
        /// </summary>
        public DateTime FechaSistema { get; set; }
        /// <summary>
        /// monto a debitar
        /// </summary>
        public decimal MontoDebitar { get; set; }
        /// <summary>
        /// numero de tarjeta
        /// </summary>
        public string NumeroTarjeta { get; set; }
        /// <summary>
        /// codigo de moneda
        /// </summary>
        public string CodigoMoneda { get; set; }
        /// <summary>
        /// numero de operacion
        /// </summary>
        public string NumeroOperacion { get; set; }
        /// <summary>
        /// cuenta origen
        /// </summary>
        public string CuentaOrigen { get; set; }
        /// <summary>
        /// cuenta destino
        /// </summary>
        public string CuentaDestino { get; set; }
        /// <summary>
        /// numero de movimiento
        /// </summary>
        public int NumeroMovimientoTTS { get; set; }
    }
}
