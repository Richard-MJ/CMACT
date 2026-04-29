using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Takana.Transferencias.CCE.Api.Common.Constantes;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    /// <summary>
    /// Información adicional de una operación realizada con tarjeta.
    /// </summary>
    public class MovimientoInfoAdicional
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
        /// Número secuencial único del movimiento.
        /// </summary>
        public int NumeroSecuencia { get; private set; }
        /// <summary>
        /// Identificador único del movimiento.
        /// </summary>
        public decimal NumeroMovimiento { get; private set; }
        /// <summary>
        /// Código del canal por el cual se realizó la transacción (ej. APP, WEB, ATM).
        /// </summary>
        public string CodigoCanal { get; private set; }
        /// <summary>
        /// Número del TTS (Transferencia Temporal de Saldos).
        /// </summary>
        public int NumeroTTS { get; private set; }
        /// <summary>
        /// Número de cuenta asociada a la transacción.
        /// </summary>
        public string NumeroCuenta { get; private set; }
        /// <summary>
        /// Periodo de tránsito asociado al TTS.
        /// </summary>
        public int PeriodoTransitoTTS { get; private set; }
        /// <summary>
        /// Observaciones relacionadas al proceso TTS.
        /// </summary>
        public string ObservacionesTTS { get; private set; }
        /// <summary>
        /// Fecha y hora en que se realizó la transacción.
        /// </summary>
        public DateTime FechaTransaccion { get; private set; }
        /// <summary>
        /// Número de la tarjeta utilizada en la operación.
        /// </summary>
        public string NumeroTarjeta { get; private set; }
        /// <summary>
        /// Código del subcanal dentro del canal principal.
        /// </summary>
        public byte CodigoSubCanal { get; private set; }
        /// <summary>
        /// Código del usuario que ejecutó la transacción.
        /// </summary>
        public string CodigoUsuario { get; private set; }
        /// <summary>
        /// Código del país de origen de la transacción.
        /// </summary>
        public string CodigoPaisOrigen { get; private set; }
        /// <summary>
        /// Identificador externo de la transacción (usado para conciliaciones).
        /// </summary>
        public string IdTransaccionExterno { get; private set; }
        /// <summary>
        /// Identificador del terminal o dispositivo desde el cual se generó la operación.
        /// </summary>
        public string IdTerminal { get; private set; }
        /// <summary>
        /// Código que identifica el tipo específico de subtransacción.
        /// </summary>
        public string CodigoSubTransaccion { get; private set; }
        /// <summary>
        /// Código del motivo de reversión, si aplica.
        /// </summary>
        public string? CodigoMotivoReversion { get; private set; }

        /// <summary>
        /// Método para crear un movimiento adicional
        /// </summary>
        /// <param name="movimientoEnCc"></param>
        /// <param name="numeroTarjeta"></param>
        /// <param name="indicadorCanalOrigen"></param>
        /// <param name="indicadorSubCanalOrigen"></param>
        /// <param name="idTerminalOrigen"></param>
        /// <returns></returns>
        public static MovimientoInfoAdicional Crear(
            Movimiento movimientoEnCc, 
            string numeroTarjeta,
            string idTerminalOrigen,
            string indicadorCanalOrigen,
            byte indicadorSubCanalOrigen = 0) 
        {
            idTerminalOrigen = idTerminalOrigen ?? "ND";

            return new MovimientoInfoAdicional
            {
                NumeroMovimiento = movimientoEnCc.NumeroMovimiento,
                NumeroCuenta = movimientoEnCc.NumeroCuenta,
                CodigoCanal = indicadorCanalOrigen,
                CodigoSubCanal = indicadorSubCanalOrigen,
                PeriodoTransitoTTS = movimientoEnCc.FechaMovimiento.Year,
                NumeroTTS = 0,
                ObservacionesTTS = movimientoEnCc.DescripcionMovimiento,
                IdTransaccionExterno = string.Empty,
                IdTerminal = idTerminalOrigen.Length > 16
                    ? idTerminalOrigen.Substring(0, 16)
                    : idTerminalOrigen,
                CodigoSubTransaccion = movimientoEnCc.CodigoSubTipoTransaccion,
                CodigoMotivoReversion = string.Empty,
                NumeroTarjeta = numeroTarjeta,
                FechaTransaccion = movimientoEnCc.FechaMovimiento,
                CodigoUsuario = movimientoEnCc.CodigoUsuario,
                CodigoPaisOrigen = "PE"
            };
        }
    }
}
