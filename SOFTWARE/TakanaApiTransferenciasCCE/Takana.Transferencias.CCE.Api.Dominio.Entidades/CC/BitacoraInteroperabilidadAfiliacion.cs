namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

/// <summary>
/// Clase que representa a la entidad de dominio Bitacora de afiliacion a interoperabilidad
/// </summary>
public class BitacoraInteroperabilidadAfiliacion : BitacoraInteroperabilidad
{
    #region Constantes
    /// <summary>
    /// Estado de afiliado a bitacora
    /// </summary>
    public const string EstadoAfiliado = "A";
    /// <summary>
    /// Estado de desafiliado en bitacora
    /// </summary>
    public const string EstadoDesafiliado = "D";
    #endregion Constantes

    #region Propiedades
    /// <summary>
    /// Numero de bitacora
    /// </summary>
    public int NumeroBitacora { get; private set; }
    /// <summary>
    /// Codigo cuenta interbancaria
    /// </summary>
    public string CodigoCCI { get; private set ; }
    /// <summary>
    /// Numero de celular
    /// </summary>
    public string NumeroCelular { get; private set ; }
    /// <summary>
    /// Identificador instruccion
    /// </summary>
    public string IdenditificadorInstruccion { get; private set ; }
    /// <summary>
    /// Numero de seguimiento de la transaccion
    /// </summary>
    public string NumeroSeguimiento { get; private set ; }
    /// <summary>
    /// Codigo del tipo de instruccion
    /// </summary>
    public string CodigoTipoInstruccion { get; private set ; }
    /// <summary>
    /// Indicador de estado de la afiliacion
    /// </summary>
    public string? IndicadorEstadoAfiliacion{ get; private set ; } = string.Empty;
    /// <summary>
    /// Indicador de estado de la afiliacion
    /// </summary>
    public string? Canal { get; private set; } = string.Empty;
    #endregion

    #region Metodos
    /// <summary>
    /// Crea una nueva entrada en la bitácora de interoperabilidad de afiliación con los datos proporcionados.
    /// </summary>
    /// <param name="codigoCCI">Código CCI relacionado con la afiliación.</param>
    /// <param name="numeroCelular">Número de celular asociado a la afiliación.</param>
    /// <param name="identificadorInstruccion">Identificador de la instrucción relacionada.</param>
    /// <param name="numeroSeguimiento">Número de seguimiento de la operación.</param>
    /// <param name="codigoTipoInstruccion">Código que especifica el tipo de instrucción.</param>
    /// <param name="fecha">Fecha y hora en que se realiza la operación.</param>
    /// <param name="codigoUsuario">Código del usuario que realiza la operación.</param>
    /// <returns>Una nueva instancia de BitacoraInteroperabilidadAfiliacion.</returns>
    public static BitacoraInteroperabilidadAfiliacion Crear(
        string codigoCCI,
        string numeroCelular,
        string identificadorInstruccion,
        string numeroSeguimiento,
        string codigoTipoInstruccion,
        DateTime fecha,
        string codigoUsuario,
        string canal,
        string estadoActual)
    {
        return new BitacoraInteroperabilidadAfiliacion
        {
            CodigoCCI = codigoCCI,
            NumeroCelular = numeroCelular,
            IdenditificadorInstruccion = identificadorInstruccion,
            NumeroSeguimiento = numeroSeguimiento,
            CodigoTipoInstruccion = codigoTipoInstruccion,
            FechaRegistro = fecha,
            FechaCreacion = fecha,
            FechaModifico = fecha,
            CodigoUsuarioRegistro = codigoUsuario,
            Canal = canal,
            IndicadorEstadoAfiliacion = estadoActual
        };
    }

    /// <summary>
    /// Crea una nueva entrada en la bitácora de interoperabilidad de afiliación con los datos proporcionados.
    /// </summary>
    /// <param name="codigoCCI"></param>
    /// <param name="numeroCelular"></param>
    /// <param name="identificadorInstruccion"></param>
    /// <param name="numeroSeguimiento"></param>
    /// <param name="codigoTipoInstruccion"></param>
    /// <param name="fecha"></param>
    /// <param name="codigoUsuario"></param>
    /// <param name="canal"></param>
    /// <param name="estadoActual"></param>
    /// <param name="codigoRespuesta"></param>
    /// <returns></returns>
    public static BitacoraInteroperabilidadAfiliacion Crear(
        string codigoCCI,
        string numeroCelular,
        string identificadorInstruccion,
        string numeroSeguimiento,
        string codigoTipoInstruccion,
        DateTime fecha,
        string codigoUsuario,
        string canal,
        string estadoActual,
        string codigoRespuesta)
    {
        return new BitacoraInteroperabilidadAfiliacion
        {
            CodigoCCI = codigoCCI,
            NumeroCelular = numeroCelular,
            IdenditificadorInstruccion = identificadorInstruccion,
            NumeroSeguimiento = numeroSeguimiento,
            CodigoTipoInstruccion = codigoTipoInstruccion,
            FechaRegistro = fecha,
            FechaCreacion = fecha,
            FechaModifico = fecha,
            CodigoUsuarioRegistro = codigoUsuario,
            Canal = canal,
            IndicadorEstadoAfiliacion = estadoActual,
            CodigoRespuesta = codigoRespuesta,
            FechaRespuesta = fecha,
            CodigoUsuarioModifico = codigoUsuario
        };
    }
    /// <summary>
    /// Actualiza la entrada en la bitácora de interoperabilidad de afiliación con los nuevos datos proporcionados.
    /// </summary>
    /// <param name="codigoRespuesta">Código de respuesta relacionado con la actualización.</param>
    /// <param name="fecha">Fecha y hora de la actualización.</param>
    /// <param name="codigoUsuario">Código del usuario que realiza la actualización.</param>
    /// <param name="estadoAfiliacion">Nuevo estado de la afiliación a registrar.</param>
    public void ActualizarBitacora(
        string codigoRespuesta,
        DateTime fecha,
        string codigoUsuario,
        string estadoAfiliacion)
    {
        IndicadorEstadoAfiliacion = estadoAfiliacion;
        CodigoRespuesta = codigoRespuesta;
        FechaRespuesta = fecha;
        FechaModifico = fecha;
        CodigoUsuarioModifico = codigoUsuario;
    }
    #endregion
}

