namespace Takana.Transferencias.CCE.Api.Common.Constantes.Interoperabilidad;

/// <summary>
/// Clase de constantes para todo interoperabilidad
/// </summary>
public  static class DatosGeneralesInteroperabilidad
{
    /// <summary>
    /// Constante de afiliado
    /// </summary>
    public const string Afiliado = "A";
    /// <summary>
    /// Constante de desafiliado
    /// </summary>
    public const string Desafiliado = "D";
    /// <summary>
    /// Canal interoperabilidad
    /// </summary>
    public const string CanalInteroperabilidad = "52";
    /// <summary>
    /// Dispositivo APP movil
    /// </summary>
    public const string Terminal = "CelAPP";
    /// <summary>
    /// Rechazo de interoperabilidad
    /// </summary>
    public const string Rechazado = "RJCT";
    /// <summary>
    /// Aceptado de interoperabilidad
    /// </summary>
    public const string Aceptado = "ACTC";
    /// <summary>
    /// Aceptado de interoperabilidad
    /// </summary>
    public const string TipoInstruccionEliminacion = "DEAC";
    /// <summary>
    /// Codigo de servicio de interoperabilidad
    /// </summary>
    public const string CodigoServicioInteroperabilidad = "SER_INTER";
    /// <summary>
    /// Parametro
    /// </summary>
    public const string Parametro = "19";
    /// <summary>
    /// Digitos faltantes de CCI
    /// </summary>
    public const string DigitosFaltantesCCI = "99999999999999999";
    /// <summary>
    /// Preijo peruano 
    /// </summary>
    public const string PrefijoPeruano = "+51";
    /// <summary>
    /// Formato para validar el celular de peru con regex
    /// </summary>
    public const string FormatoCelularPeruRegex = "@\"^\\+519[0-8]{8}$";
    /// <summary>
    /// Formato par validar que son 9 digitos ocn regex
    /// </summary>
    public const string Formato9DigitosRegex = "^9[0-9]{8}$";
    /// <summary>
    /// Maximo de operaciones por dia
    /// </summary>
    public const string MaximoOperacion = "MAX_TRANS_SOLES";
    /// <summary>
    /// Maximo de operaciones por dia
    /// </summary>
    public const string MinimoOperacion = "MIN_TRANS_SOLES";
    /// <summary>
    /// Maximo de operaciones por dia
    /// </summary>
    public const string MaximoOperacionesDia = "MAX_OPER_DIA";
    /// <summary>
    /// Maximo de operaciones por dia
    /// </summary>
    public const string MontoLimiteDia = "MON_LIMITE_DIA_CLIENTE";
    /// <summary>
    /// Intentos maximos de barrido
    /// </summary>
    public const string ExcesoOperacionesBarrido= "MAX_CONS";
    /// <summary>
    /// Minutos que se consideran para el bloqueo por Barrido de Contactos
    /// </summary>
    public const string MinutosBloquedoPorBarrido = "MIN_BLOQUEO";
    /// <summary>
    /// Tiempo de bloqueo del servicio
    /// </summary>
    public const string TiempoBloqueoServicio = "BLO_SERV";
    /// <summary>
    /// Tiempo de bloqueo del servicio
    /// </summary>
    public const string Comision = "COM";
    /// <summary>
    /// Codigo de exito interoperabilidad
    /// </summary>
    public const string Exitoso = "0";
    /// <summary>
    /// Codigo de directorio CCE
    /// </summary>
    public const string DirectorioCCE = "0903";
    /// <summary>
    /// Codigo de directorio CCE 3 digitos
    /// </summary>
    public const string CodigoDirectorioCCE = "903";
    /// <summary>
    /// Codigo de Operacion lectura
    /// </summary>
    public const string OperacionLecturaQR = "LECQR";
    /// <summary>
    /// Operacion Exitosa de transferencias inmediatas
    /// </summary>
    public const string OperacionExitosa = "Operación Exitosa";
    /// <summary>
    /// Ubicacion de la operacion 
    /// </summary>
    public const string Ubicacion = "Billtera Virtual";
    /// <summary>
    /// Tema de la operacion  de transferencia por interoperabilidad
    /// </summary>
    public const string TemaMensajeInteroperabilidad = "Transferencia Billetera Virtual";

    #region Constantes Trama
    /// <summary>
    /// Codigo de tipo de instruccion
    /// </summary>
    public const string CodigoTipoInstruccion = "L05";
    /// <summary>
    /// Codigo de id tramo de mensaje de Proxy 01
    /// </summary>
    public const string CodigoIdTramoMensajeProxy01 = "H03";
    /// <summary>
    /// Codigo de CCI
    /// </summary>
    public const string CodigoCCI = "L03";
    /// <summary>
    /// Código de numero de celular
    /// </summary>
    public const string CodigoNumeroCelular = "L08";
    #endregion
}
