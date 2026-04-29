using AutorizadorCanales.Domain.Entidades.CF;

namespace AutorizadorCanales.Domain.Entidades.TJ
{
    public class TarjetaMovimiento
    {
        /// <summary>
        /// Código de la agencia en el que se hizo el movimeinto
        /// </summary>
        public string CodigoAgencia { get; private set; } = null!;

        /// <summary>
        /// Número de movimiento, obtenido de una serie (SEC_MOVIMT)
        /// </summary>
        public decimal NumeroMovimiento { get; private set; }

        /// <summary>
        /// Código del usuario que realizo el movimiento.
        /// </summary>
        public string CodigoUsuario { get; private set; } = null!;

        /// <summary>
        /// Código del cliente al que pertenece la tarjeta.
        /// </summary>
        public string CodigoCliente { get; private set; } = null!;

        /// <summary>
        /// Número de Tarjeta.
        /// </summary>
        public decimal NumeroTarjeta { get; private set; }

        /// <summary>
        /// Tipo de transacción (en relación a catal_transacciones)
        /// </summary>
        public string CodigoTipoTransaccion { get; private set; } = null!;

        /// <summary>
        /// Subtipo de Transaccion (en relacion  a la tabla subtip_transac)
        /// </summary>
        public string CodigoSubTipoTransaccion { get; private set; } = null!;

        /// <summary>
        /// Fecha en el que se realizoel movimiento.
        /// </summary>
        public DateTime FechaMovimiento { get; set; }

        /// <summary>
        /// Indicador de estado del movimiento (A Activo,N Nulo)
        /// </summary>
        public string IndicadorEstado { get; private set; } = null!;

        /// <summary>
        /// Observación del movimiento
        /// </summary>
        public string? Observaciones { get; private set; } = null!;

        /// <summary>
        /// Indicador de Canal.
        /// </summary>
        public string? IndicadorCanal { get; private set; } = null!;

        public static TarjetaMovimiento Crear(
            decimal numeroSerie,
            Tarjeta tarjeta,
            string codigoUsuario,
            string codigoAgencia,
            SubTipoTransaccion subTipoTransaccion,
            DateTime fechaMovimiento,
            string indicadorEstado,
            string observaciones,
            string indicadorCanal)
        {
            return new TarjetaMovimiento()
            {
                NumeroMovimiento = numeroSerie,
                NumeroTarjeta = tarjeta.NumeroTarjeta,
                CodigoUsuario = codigoUsuario,
                CodigoAgencia = codigoAgencia,
                CodigoCliente = tarjeta.CodigoCliente!,
                CodigoTipoTransaccion = subTipoTransaccion.CodigoTipoTransaccion,
                CodigoSubTipoTransaccion = subTipoTransaccion.CodigoSubTipoTransaccion,
                FechaMovimiento = fechaMovimiento,
                IndicadorEstado = indicadorEstado,
                Observaciones = observaciones,
                IndicadorCanal = indicadorCanal
            };
        }
    }
}
