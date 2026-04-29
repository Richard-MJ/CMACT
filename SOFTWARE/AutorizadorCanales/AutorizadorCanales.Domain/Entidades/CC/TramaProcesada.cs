using AutorizadorCanales.Domain.Extensiones;
using System.Globalization;

namespace AutorizadorCanales.Domain.Entidades.CC;

/// <summary>
/// TABLA PARA REGISTRAR LOS DATOS DE
/// TRAMA ISO PARA UN CANAL DETERMINADO.
/// </summary>
public class TramaProcesada
{
    #region Propiedades
    /// <summary>
    /// ID DE UN REGISTRO DE TRAMA ISO8583.
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// INDICADOR DEL ORIGEN DEL REGISTRO DE TRAMA.
    /// </summary>
    public string IdCanal { get; private set; } = null!;
    /// <summary>
    /// TIPO DE TRAMA REGISTRADO.
    /// </summary>
    public string CodigoTipoTrama { get; private set; } = null!;
    /// <summary>
    /// NÚMERO DE TARJETA QUE LLEGA EN LA TRAMA.
    /// </summary>
    public string NumeroTarjeta { get; private set; } = null!;
    /// <summary>
    /// CÓDIGO DE PROCESO PARA LA TRAMA REGISTRADA.
    /// </summary>
    public string CodigoProceso { get; private set; } = null!;
    /// <summary>
    /// CODIGO ISO DE LA MONEDA QUE LLEGA EN LA TRAMA.
    /// </summary>
    public string CodigoMonedaIso { get; private set; } = null!;
    /// <summary>
    /// ID DEL TERMINAL EN EL CUAL SE ORIGINO LA TRANSACCIÓN.
    /// </summary>
    public string? IdTerminal { get; private set; } = null!;
    /// <summary>
    /// MONTO DE LA TRANSACCIÓN.
    /// </summary>
    public string CadenaMontoOperacion { get; private set; } = null!;
    /// <summary>
    /// NÚMERO DE IDENTIFICACIÓN DE LA TRAMA GENERADO
    /// POR EL PROVEEDOR.
    /// </summary>
    public string CodigoNumeroTrace { get; private set; } = null!;
    /// <summary>
    /// HORA EN LA CUAL SE GENERÓ LA TRANSACCIÓN EN
    /// FORMATO HHMMSS.
    /// </summary>
    public string CodigoFechaHora { get; private set; } = null!;
    /// <summary>
    /// DIA EN EL CUAL SE GENERÓ LA TRANSACCIÓN EN
    /// FORMATO MMAA.
    /// </summary>
    public string CodigoFechaDia { get; private set; } = null!;
    /// <summary>
    /// CODIGO BIN DE NUESTRA ENTIDAD.
    /// </summary>
    public string CodigoBinAdquiriente { get; private set; } = null!;
    /// <summary>
    /// NÚMERO DEL PRODUCTO ORIGEN DE LA TRANSACCIÓN.
    /// </summary>
    public string? NumeroProductoOrigen { get; private set; } = null!;
    /// <summary>
    /// NÚMERO DEL PRODUCTO DESTINO DE LA TRANSACCIÓN.
    /// </summary>
    public string? NumeroProductoDestino { get; private set; } = null!;
    /// <summary>
    /// ESTADO DEL REGISTRO:
    /// A -> TRAMA REQUEST REGISTRADA,
    /// P -> DATOS DE RESPUESTA LISTO,
    /// R -> TRAMA REVERSADA.
    /// </summary>
    public string IndicadorEstado { get; private set; } = null!;
    /// <summary>
    /// ID DEL MOVIMIENTO TTS GENERADO EN CC O PR.
    /// </summary>
    public int IdMovimientoTts { get; private set; }
    /// <summary>
    /// CÓDIGO DE RESPUESTA DE LA TRAMA.
    /// </summary>
    public string CodigoRespuesta { get; private set; } = null!;
    /// <summary>
    /// NÚMERO DE AUTORIZACIÓN GENERADO POR EL SISTEMA.
    /// </summary>
    public string? CodigoNumeroAutorizacion { get; private set; } = null!;
    /// <summary>
    /// CÓDIGO DE MENSAJE 1
    /// </summary>
    public string? CodigoMensajeUno { get; private set; } = null!;
    /// <summary>
    /// CÓDIGO DE MENSAJE 2
    /// </summary>
    public string? CodigoMensajeDos { get; private set; } = null!;
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
    public string CodigoSistemaFuente { get; private set; } = null!;
    /// <summary>
    /// INDICA SI LA TRANSACCIÓN FUE CONCILIADA.
    /// </summary>
    public string? IndicadorConciliado { get; private set; } = null!;
    /// <summary>
    /// ASIENTO CONTABLE EN QUE FUE COMPENSADA LA OPERACIÓN.
    /// </summary>
    public int? NumeroAsientoCompensa { get; private set; }
    #endregion

    #region Constantes
    public const string PROCESADO = "P";
    public const string REGISTRADO = "A";
    #endregion

    #region Métodos

    public static TramaProcesada Crear(
    string codigoCanal, decimal numeroTarjeta,
    string tipoTrama, string tipoProceso, string codigoIsoMonedaOrigen,
    string idTerminal, decimal montoOperacion, string numeroTrace,
    DateTime fechaOperacion, string codigoBinAdquirente,
    string numeroProductoOrigen, string numeroProductoDestino,
    string codigoMensajeUno, string codigoMensajeDos, DateTime fechaSistema,
    string codigoSistemaOrigen
    )
    {
        var fechaServidor = DateTime.Now;

        var registro = new TramaProcesada
        {
            IdCanal = codigoCanal,
            CodigoTipoTrama = tipoTrama ?? string.Empty,
            NumeroTarjeta = numeroTarjeta.ToString(CultureInfo.InvariantCulture),
            CodigoProceso = tipoProceso,
            CodigoMonedaIso = codigoIsoMonedaOrigen,
            IdTerminal = idTerminal.PadLeft(24).Trim(),
            CadenaMontoOperacion = montoOperacion.ToStringTrama(TipoConversionDecimal.DosUltimosDigitosDecimal, 12),
            CodigoNumeroTrace = numeroTrace,
            CodigoFechaHora = fechaOperacion.ToStringTrama(FormatoFecha.HHMMSS),
            CodigoFechaDia = fechaOperacion.ToStringTrama(FormatoFecha.MMDD),
            CodigoBinAdquiriente = codigoBinAdquirente,
            NumeroProductoOrigen = numeroProductoOrigen,
            NumeroProductoDestino = numeroProductoDestino,
            IndicadorEstado = REGISTRADO,
            IdMovimientoTts = 0,
            CodigoRespuesta = string.Empty,
            CodigoNumeroAutorizacion = string.Empty,
            CodigoMensajeUno = codigoMensajeUno,
            CodigoMensajeDos = codigoMensajeDos,
            FechaSistema = fechaSistema,
            FechaRegistro = fechaServidor,
            FechaModificado = fechaServidor,
            CodigoSistemaFuente = codigoSistemaOrigen,
            IndicadorConciliado = "N",
            NumeroAsientoCompensa = 0
        };

        return registro;
    }

    public void ProcesarTrama(
        int idMovimientoTts,
        string codigoRespuesta,
        string numeroAutorizacion,
        DateTime fechaModificacion)
    {
        IdMovimientoTts = idMovimientoTts;
        CodigoRespuesta = codigoRespuesta;
        CodigoNumeroAutorizacion = numeroAutorizacion;
        FechaModificado = fechaModificacion;
        IndicadorEstado = PROCESADO;
    }

    #endregion
}
