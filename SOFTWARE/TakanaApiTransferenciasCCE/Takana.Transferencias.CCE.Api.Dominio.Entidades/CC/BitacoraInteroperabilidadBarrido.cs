namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

/// <summary>
/// Clase que representa a la entidad de dominio Bitacora de barrido de interoperabilidad
/// </summary>
public class BitacoraInteroperabilidadBarrido : BitacoraInteroperabilidad
{
    #region Constantes
    /// <summary>
    /// Si hay al menos un resultado de banco en la respuesta de la CCE segun el numero enviado
    /// </summary>
    public const string SiHayResultado= "S";
    /// <summary>
    /// No hay resultado de banco en la respuesta de la CCE segun el numero enviado
    /// </summary>
    public const string NoHayResultado= "N";
    #endregion

    #region Propiedades
    /// <summary>
    /// Numero de bitacora
    /// </summary>
    public int? NumeroBitacora { get; private set; }
    /// <summary>
    /// Codigo cuenta interbancario
    /// </summary>
    public string? CodigoCCI { get; private set; } = string.Empty;
    /// <summary>
    /// Codigo de entidad Emisora
    /// </summary>
    public string? CodigoEntidad { get; private set; } = string.Empty;
    /// <summary>
    /// Numero celular originante
    /// </summary>
    public string? NumeroCelularOrigen{ get; private set; } = string.Empty;
    /// <summary>
    /// Numero celular Receptor
    /// </summary>
    public string? NumeroCelularReceptor { get; private set; } = string.Empty;
    /// <summary>
    /// Numero seguimiento
    /// </summary>
    public string? NumeroSeguimiento { get; private set; } = string.Empty;
    /// <summary>
    /// Identificador para la CCE
    /// </summary>
    public string? IdentificadorInstruccion { get; private set; } = string.Empty;
    /// <summary>
    /// Resultado de intento, al menos un banco relacionado al resultado
    /// </summary>
    public string? ResultadoAceptado { get; private set; } = string.Empty;


    #endregion

    /// <summary>
    /// Crea una nueva entrada en la bitácora de interoperabilidad para el barrido de afiliaciones con los datos proporcionados.
    /// </summary>
    /// <param name="codigoCCI">Código CCI relacionado con la afiliación.</param>
    /// <param name="codigoEntidad">Código de la entidad relacionada con la afiliación.</param>
    /// <param name="numeroCelularOrigen">Número de celular de origen relacionado con la afiliación.</param>
    /// <param name="numeroCelularReceptor">Número de celular receptor relacionado con la afiliación.</param>
    /// <param name="numeroSeguimiento">Número de seguimiento de la operación.</param>
    /// <param name="identificadorInstruccion">Identificador de la instrucción relacionada.</param>
    /// <param name="fechaCreacion">Fecha y hora de creación de la operación.</param>
    /// <param name="codUsuario">Código del usuario que realiza la operación.</param>
    /// <returns>Una nueva instancia de BitacoraInteroperabilidadBarrido.</returns>
    public static BitacoraInteroperabilidadBarrido Crear(
        string codigoCCI,
        string codigoEntidad,
        string numeroCelularOrigen,
        string numeroCelularReceptor,
        string numeroSeguimiento,
        string identificadorInstruccion,
        DateTime fechaCreacion,
        string codUsuario)
    {
        return new BitacoraInteroperabilidadBarrido
        {
            CodigoCCI = codigoCCI,
            CodigoEntidad = codigoEntidad,
            NumeroCelularOrigen = numeroCelularOrigen,
            NumeroCelularReceptor = numeroCelularReceptor,
            NumeroSeguimiento = numeroSeguimiento,
            IdentificadorInstruccion = identificadorInstruccion,
            FechaCreacion = fechaCreacion,
            FechaRegistro = fechaCreacion,
            FechaModifico = fechaCreacion,
            CodigoUsuarioModifico = codUsuario,
            CodigoUsuarioRegistro = codUsuario,
        };
    }
    /// <summary>
    /// Actualiza la entrada en la bitácora de interoperabilidad con los nuevos datos proporcionados.
    /// </summary>
    /// <param name="codigoRespuesta">Código de respuesta relacionado con la actualización.</param>
    /// <param name="fecha">Fecha y hora de la actualización.</param>
    /// <param name="codigoUsuario">Código del usuario que realiza la actualización.</param>
    public void ActualicarBitacora(
        string codigoRespuesta,
        DateTime fecha,
        string codigoUsuario)
    {
        CodigoRespuesta = codigoRespuesta;
        FechaModifico = fecha;
        CodigoUsuarioModifico = codigoUsuario;
        FechaRespuesta = fecha;
        ResultadoAceptado = SiHayResultado;
    }
    /// <summary>
    /// Si no hay al menos una entidad  o directorio en el resultado, esta cuenta como intento fallido
    /// </summary>
    public void ContarComoIntentoFallido()
    {
        ResultadoAceptado = NoHayResultado;
    }
}

