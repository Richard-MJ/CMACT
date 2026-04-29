using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using static Takana.Transferencias.CCE.Api.Common.Utilidades.ExtensionesString;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    /// <summary>
    /// TABLA PARA REGISTRAR LOS DATOS DE TRAMA ISO PARA UN CANAL DETERMINADO.
    /// </summary>
    public class TramaProcesada
    {
        #region Constantes
        public const string Procesado = "P";
        public const string Registrado = "A";
        public const string Reversado = "R";
        public const string TipoTramaRequest = "200";
        public const string TipoTramaTransferenciasCCE = "400102";
        #endregion

        #region Propiedades
        /// <summary>
        /// ID DE UN REGISTRO DE TRAMA ISO8583.
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// INDICADOR DEL ORIGEN DEL REGISTRO DE TRAMA.
        /// </summary>
        public string CodigoCanal { get; private set; }
        /// <summary>
        /// TIPO DE TRAMA REGISTRADO.
        /// </summary>
        public string CodigoTipoTrama { get; private set; }
        /// <summary>
        /// NÚMERO DE TARJETA QUE LLEGA EN LA TRAMA.
        /// </summary>
        public string NumeroTarjeta { get; private set; }
        /// <summary>
        /// CÓDIGO DE PROCESO PARA LA TRAMA REGISTRADA.
        /// </summary>
        public string CodigoProceso { get; private set; }
        /// <summary>
        /// CODIGO ISO DE LA MONEDA QUE LLEGA EN LA TRAMA.
        /// </summary>
        public string CodigoMonedaIso { get; private set; }
        /// <summary>
        /// ID DEL TERMINAL EN EL CUAL SE ORIGINO LA TRANSACCIÓN.
        /// </summary>
        public string IdTerminal { get; private set; }
        /// <summary>
        /// MONTO DE LA TRANSACCIÓN.
        /// </summary>
        public string CadenaMontoOperacion { get; private set; }
        /// <summary>
        /// NÚMERO DE IDENTIFICACIÓN DE LA TRAMA GENERADO
        /// POR EL PROVEEDOR.
        /// </summary>
        public string CodigoNumeroTrace { get; private set; }
        /// <summary>
        /// HORA EN LA CUAL SE GENERÓ LA TRANSACCIÓN EN
        /// FORMATO HHMMSS.
        /// </summary>
        public string CodigoFechaHora { get; private set; }
        /// <summary>
        /// DIA EN EL CUAL SE GENERÓ LA TRANSACCIÓN EN
        /// FORMATO MMAA.
        /// </summary>
        public string CodigoFechaDia { get; private set; }
        /// <summary>
        /// CODIGO BIN DE NUESTRA ENTIDAD.
        /// </summary>
        public string CodigoBinAdquiriente { get; private set; }
        /// <summary>
        /// NÚMERO DEL PRODUCTO ORIGEN DE LA TRANSACCIÓN.
        /// </summary>
        public string NumeroProductoOrigen { get; private set; }
        /// <summary>
        /// NÚMERO DEL PRODUCTO DESTINO DE LA TRANSACCIÓN.
        /// </summary>
        public string NumeroProductoDestino { get; private set; }
        /// <summary>
        /// ESTADO DEL REGISTRO:
        /// A -> TRAMA REQUEST REGISTRADA,
        /// P -> DATOS DE RESPUESTA LISTO,
        /// R -> TRAMA REVERSADA.
        /// </summary>
        public string IndicadorEstado { get; private set; }
        /// <summary>
        /// ID DEL MOVIMIENTO TTS GENERADO EN CC O PR.
        /// </summary>
        public int IdMovimientoTts { get; private set; }
        /// <summary>
        /// CÓDIGO DE RESPUESTA DE LA TRAMA.
        /// </summary>
        public string CodigoRespuestaTrama { get; private set; }
        /// <summary>
        /// NÚMERO DE AUTORIZACIÓN GENERADO POR EL SISTEMA.
        /// </summary>
        public string CodigoNumeroAutorizacion { get; private set; }
        /// <summary>
        /// CÓDIGO DE MENSAJE 1
        /// </summary>
        public string CodigoMensajeUno { get; private set; }
        /// <summary>
        /// CÓDIGO DE MENSAJE 2
        /// </summary>
        public string CodigoMensajeDos { get; private set; }
        /// <summary>
        /// FECHA DEL SISTEMA.
        /// </summary>
        public DateTime FechaSistema { get; private set; }
        /// <summary>
        /// FECHA DE REGISTRO DE LOS DATOS.
        /// </summary>
        public DateTime FechaRegistro { get; private set; }
        /// <summary>
        /// FECHA EN LA QUE FUE MODIFICADO LOS DATOS.
        /// </summary>
        public DateTime FechaModificado { get; private set; }
        /// <summary>
        /// CÓDIGO DEL SISTEMA AL QUE AFECTA LA TRANSACCIÓN.
        /// </summary>
        public string CodigoSistemaFuente { get; private set; }
        /// <summary>
        /// INDICA SI LA TRANSACCIÓN FUE CONCILIADA.
        /// </summary>
        public string IndicadorConciliado { get; private set; }
        /// <summary>
        /// ASIENTO CONTABLE EN QUE FUE COMPENSADA LA OPERACIÓN.
        /// </summary>
        public int NumeroAsientoCompensa { get; private set; }

        #endregion

        /// <summary>
        /// Método que registra la trama de datos
        /// </summary>
        /// <param name="numeroTarjeta"></param>
        /// <param name="numeroMovimiento"></param>
        /// <param name="numeroAsiento"></param>
        /// <param name="codigoCanal"></param>
        /// <param name="tipoTrama"></param>
        /// <param name="tipoProceso"></param>
        /// <param name="codigoIsoMonedaOrigen"></param>
        /// <param name="idTerminal"></param>
        /// <param name="montoOperacion"></param>
        /// <param name="numeroTrace"></param>
        /// <param name="fechaOperacion"></param>
        /// <param name="numeroProductoOrigen"></param>
        /// <param name="numeroProductoDestino"></param>
        /// <param name="fechaSistema"></param>
        /// <param name="codigoSistemaOrigen"></param>
        /// <returns></returns>
        public static TramaProcesada RegistrarDatosTrama(
            string numeroTarjeta, 
            string codigoCanal,
            int numeroMovimiento,
            int numeroAsiento,
            string tipoTrama, 
            string tipoProceso, 
            string codigoIsoMonedaOrigen,
            string idTerminal, 
            decimal montoOperacion, 
            string numeroTrace,
            DateTime fechaOperacion, 
            string numeroProductoOrigen, 
            string numeroProductoDestino,
            DateTime fechaSistema,
            string codigoSistemaOrigen
        )
        {
            var binAdquirente = string.IsNullOrEmpty(numeroTarjeta) ? numeroTarjeta.Substring(0, 8) : string.Empty;
            var numTarjeta = codigoCanal == General.Ventanilla ? string.Empty : numeroTarjeta;

            var registro = new TramaProcesada
            {
                CodigoCanal = codigoCanal,
                CodigoTipoTrama = tipoTrama,
                NumeroTarjeta = numTarjeta,
                CodigoProceso = tipoProceso,
                CodigoMonedaIso = codigoIsoMonedaOrigen,
                IdTerminal = idTerminal = idTerminal.Length > 24
                    ? idTerminal.Substring(0, 24)
                    : idTerminal.PadLeft(24).Trim(),
                CadenaMontoOperacion = montoOperacion.ObtenerImporteOperacion().ToStringTrama(TipoConversionDecimal.DosUltimosDigitosDecimal, 12),
                CodigoNumeroTrace = numeroTrace,
                CodigoFechaHora = fechaOperacion.ToStringTrama(FormatoFecha.HHMMSS),
                CodigoFechaDia = fechaOperacion.ToStringTrama(FormatoFecha.MMDD),
                CodigoBinAdquiriente = binAdquirente,
                NumeroProductoOrigen = numeroProductoOrigen,
                NumeroProductoDestino = numeroProductoDestino,
                IndicadorEstado = Registrado,
                IdMovimientoTts = numeroMovimiento,
                CodigoRespuestaTrama = string.Empty,
                CodigoNumeroAutorizacion = string.Empty,
                CodigoMensajeUno = string.Empty,
                CodigoMensajeDos = string.Empty,
                FechaSistema = fechaSistema,
                FechaRegistro = fechaSistema,
                FechaModificado = fechaSistema,
                CodigoSistemaFuente = codigoSistemaOrigen,
                IndicadorConciliado = General.No,
                NumeroAsientoCompensa = numeroAsiento
            };

            return registro;
        }

        /// <summary>
        /// Actualizar dataos de trama procesada
        /// </summary>
        /// <param name="fechaSistema"></param>
        /// <param name="codigoRespuesta"></param>
        /// <param name="indicadorEstado"></param>
        public void ActualizarTramaProcesada(
            DateTime fechaSistema, 
            string codigoRespuesta,
            string indicadorEstado)
        {
            CodigoRespuestaTrama = codigoRespuesta;
            IndicadorEstado = indicadorEstado;
            FechaModificado = fechaSistema;
        }
    }
}