using Takana.Transferencias.CCE.Api.Common.Constantes;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
/// <summary>
/// Clase de dominio encargada del afiliacion a interoperabilidad detalle
/// </summary>
public class AfiliacionInteroperabilidadDetalle
{
    #region Constante

    /// <summary>
    /// Si afiliado
    /// </summary>
    public const string Afiliado = "S";
    /// <summary>
    /// No afiliado
    /// </summary>
    public const string Desafiliado = "N";
    /// <summary>
    /// Estado anulado
    /// </summary>
    public const string Anulado = "A";
    #endregion

    #region Propiedades
    /// <summary>
    /// Numero de afiliacion
    /// </summary>
    public int NumeroAfiliacion { get; private set; }
    /// <summary>
    /// Codigo de cuenta interbancario
    /// </summary>
    public string CodigoCuentaInterbancario { get; private set; }
    /// <summary>
    /// Numero de celular
    /// </summary>
    public string NumeroCelular { get; private set; }
    /// <summary>
    /// Indicador de estado de afiliacion
    /// </summary>
    public string IndicadorEstadoAfiliado { get; private set; }
    /// <summary>
    /// Fecha de afiliacion
    /// </summary>
    public DateTime FechaAfiliacion { get; private set; }
    /// <summary>
    /// Numero de seguimiento
    /// </summary>
    public string? NumeroSeguimiento { get; private set; }
    /// <summary>
    /// Contador de Cantidad de Barridos Contacto
    /// </summary>
    public int? ContadorBarridosContacto { get; private set; }
    /// <summary>
    /// Fecha y hora de bloqueo
    /// </summary>
    public DateTime? FechaBloqueo { get; private set; }
    /// <summary>
    /// Codigo de usuario de registro
    /// </summary>
    public string? CodigoUsuarioRegistro { get; private set; }
    /// <summary>
    /// Codigo de usuario de modificacion
    /// </summary>
    public string? CodigoUsuarioModifico { get; private set; }
    /// <summary>
    /// Fecha de modificacion
    /// </summary>
    public DateTime? FechaModifico { get; private set; }
    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime? FechaRegistro { get; private set; }
    /// <summary>
    /// Identificador QR de la CCE
    /// </summary>
    public string? IdentificadorQR { get; private set; }
    /// <summary>
    /// Cadena hash del QR generado por la CCE
    /// </summary>
    public string? CadenaHash { get; private set; }
    /// <summary>
    /// Canal de ultima modificacion
    /// </summary>
    public string Canal { get; private set; }
    /// <summary>
    /// Entidad de afiliacion de Interoperabilidad
    /// </summary>
    public virtual AfiliacionInteroperabilidad afiliacion { get; private set; }

    /// <summary>
    /// Indicador para recibir notificaciones por operaciones entrantes
    /// </summary>
    public string IndicadoRecibirNotificacion { get; private set; }

    /// <summary>
    /// Indicador para enviar notificaciones por operaciones salientes
    /// </summary>
    public string IndicadorEnviarNotificacion { get; private set; }
    #endregion

    #region Propiedades Calculadas
    /// <summary>
    /// Clave para los intentos de barrido
    /// </summary>
    public string KeyIntentosBarrido => $"interop:barrido:intentos:{CodigoCuentaInterbancario}";
    /// <summary>
    /// Clave para el bloqueo de barrido
    /// </summary>
    public string KeyBloqueoBarrido => $"interop:barrido:bloqueo:{CodigoCuentaInterbancario}";
    #endregion

    #region Métodos
    /// <summary>
    /// Crea un nuevo detalle de afiliación de interoperabilidad con los datos proporcionados.
    /// </summary>
    /// <param name="numeroAfiliacion">El número de afiliación del nuevo registro.</param>
    /// <param name="codigoCuentaInterbancario">El código de la cuenta interbancaria asociada.</param>
    /// <param name="numeroCelcular">El número de celular del afiliado.</param>
    /// <param name="indicadorEstadoAfiliado">El estado actual del afiliado (activo, inactivo, etc.).</param>
    /// <param name="fechaAfiliacion">La fecha en la que se realizó la afiliación.</param>
    /// <param name="codigoUsuarioRegistro">El código del usuario que registró la afiliación.</param>
    /// <param name="FechaRegistro">La fecha y hora en que se registró el detalle de afiliación.</param>
    /// <returns>Un objeto de tipo <se

    public static AfiliacionInteroperabilidadDetalle Crear(
        int numeroAfiliacion,
        string codigoCuentaInterbancario,
        string numeroCelcular,
        string indicadorEstadoAfiliado,
        DateTime fechaAfiliacion,
        string codigoUsuarioRegistro,
        DateTime FechaRegistro,
        string canal
    )
    {
        return new AfiliacionInteroperabilidadDetalle
        {
            NumeroAfiliacion = numeroAfiliacion,
            CodigoCuentaInterbancario = codigoCuentaInterbancario,
            NumeroCelular = numeroCelcular,
            IndicadorEstadoAfiliado = indicadorEstadoAfiliado,
            FechaAfiliacion = fechaAfiliacion,
            CodigoUsuarioRegistro = codigoUsuarioRegistro,
            CodigoUsuarioModifico = codigoUsuarioRegistro,
            FechaModifico = FechaRegistro,
            FechaRegistro = FechaRegistro,
            Canal = canal,
            ContadorBarridosContacto = 0
        };
    }
    /// <summary>
    /// Estado de afiliado S(SI)
    /// </summary>
    public void Afiliar(DateTime fecha, int numeroAfiliacion, string canal)
    {
        IndicadorEstadoAfiliado = Afiliado;
        NumeroAfiliacion = numeroAfiliacion;
        FechaModifico = fecha;
        FechaAfiliacion = fecha;
        Canal = canal;
        CodigoUsuarioModifico = General.UsuarioPorDefectoInteroperabilidad;
    }
    /// <summary>
    /// Estado de afiliado S(NO)
    /// </summary>
    public void Desafiliar(DateTime fecha,string canal)
    {
        IndicadorEstadoAfiliado = Desafiliado;
        FechaModifico = fecha;
        Canal = canal;
    }
    /// <summary>
    /// Agrega el QR generado por la CCE
    /// </summary>
    /// <param name="identificadorQR">Identificador de QR</param>
    /// <param name="cadenaHash">Cadena hash de qr</param>
    public void AgregarQR(string identificadorQR, string cadenaHash)
    {
        IdentificadorQR = identificadorQR;
        CadenaHash = cadenaHash;
    }
    /// <summary>
    /// Metodo para agregar numero de seguimiento
    /// </summary>
    /// <param name="numeroSeguimiento"></param>
    public void AgregarNumeroSeguimiento(string numeroSeguimiento )
    {
        NumeroSeguimiento = numeroSeguimiento;
    }

    public void ActualizarNotificacion(bool notificarOperacionesRecibidas, bool notificarOperacionesEnviadas)
    {
        IndicadoRecibirNotificacion = notificarOperacionesRecibidas
            ? General.Si
            : General.No;
        IndicadorEnviarNotificacion = notificarOperacionesEnviadas
            ? General.Si
            : General.No;
    }
    #endregion
}
