using AutorizadorCanales.Domain.Entidades.CL;

namespace AutorizadorCanales.Domain.Entidades.SG;

/// <summary>
/// Entidad que representa una sesión activa en los canales electrónicos.
/// </summary>
public class SesionCanalElectronico : EntidadEmpresa
{
    #region Propiedades
    /// <summary>
    /// Identificador de la sesión.
    /// </summary>
    public int IdSesion { get; private set; }

    /// <summary>
    /// Número de tarjeta del cliente asociado a la sesión.
    /// </summary>
    public decimal NumeroTarjeta { get; private set; }

    /// <summary>
    /// Código del cliente.
    /// </summary>
    public string CodigoCliente { get; private set; } = null!;

    /// <summary>
    /// Identificador del dispositivo.
    /// </summary>
    public string DispositivoId { get; private set; } = null!;

    /// <summary>
    /// Dirección IP desde donde se realizó la conexión.
    /// </summary>
    public string DireccionIp { get; private set; } = null!;

    /// <summary>
    /// Sistema operativo del dispositivo conectado.
    /// </summary>
    public string SistemaOperativo { get; private set; } = null!;

    /// <summary>
    /// Navegador utilizado para la conexión.
    /// </summary>
    public string Navegador { get; private set; } = "Other";

    /// <summary>
    /// Modelo del dispositivo utilizado.
    /// </summary>
    public string? ModeloDispositivo { get; private set; }

    /// <summary>
    /// Token hasheado
    /// </summary>
    public string TokenGuid { get; private set; } = null!;    
    
    /// <summary>
    /// Id de la conexión signal R
    /// </summary>
    public string? IdConexion { get; private set; } = null!;

    /// <summary>
    /// Indicador del estado de la sesión (A=Activa, I=Inactiva).
    /// </summary>
    public string IndicadorEstado { get; private set; } = null!;

    /// <summary>
    /// Indicador del canal de acceso (A=App, W=Web, etc).
    /// </summary>
    public string IndicadorCanal { get; private set; } = null!;

    /// <summary>
    /// Fecha y hora de registro de la sesión.
    /// </summary>
    public DateTime FechaRegistro { get; private set; }

    /// <summary>
    /// Fecha y hora de modificación de la sesión.
    /// </summary>
    public DateTime FechaModificacion { get; private set; }

    /// <summary>
    /// Dispositivo canal electrónico relacionado.
    /// </summary>
    public virtual DispositivoCanalElectronico DispositivoCanalElectronico { get; private set; } = null!;

    /// <summary>
    /// Cliente asociado a la sesión.
    /// </summary>
    public virtual Cliente Cliente { get; private set; } = null!;
    #endregion

    #region Autogeneradas
    public bool EstaActiva => IndicadorEstado == EstadoEntidad.ACTIVO;
    #endregion

    #region Métodos
    /// <summary>
    /// Método estático para crear una nueva sesión en el canal electrónico.
    /// </summary>
    /// <param name="dispositivoCanalElectronico">Dispositivo canal electrónico.</param>
    /// <param name="direccionIp">Dirección IP de la conexión.</param>
    /// <param name="sistemaOperativo">Sistema operativo del dispositivo.</param>
    /// <param name="navegador">Navegador utilizado.</param>
    /// <param name="modeloDispositivo">Modelo del dispositivo.</param>
    /// <param name="indicadorCanal">Indicador del canal de acceso.</param>
    /// <param name="fecha">Fecha de creación.</param>
    /// <returns>Instancia de <see cref="SesionCanalElectronico"/>.</returns>
    public static SesionCanalElectronico Crear(
        DispositivoCanalElectronico dispositivoCanalElectronico,
        string direccionIp,
        string sistemaOperativo,
        string navegador,
        string? modeloDispositivo,
        string tokenGuid,
        string indicadorCanal,
        DateTime fecha)
    {
        return new SesionCanalElectronico
        {
            NumeroTarjeta = dispositivoCanalElectronico.NumeroTarjeta,
            CodigoCliente = dispositivoCanalElectronico.CodigoCliente,
            CodigoEmpresa = EMPRESA,
            DispositivoId = dispositivoCanalElectronico.DispositivoId,
            DireccionIp = direccionIp,
            SistemaOperativo = sistemaOperativo,
            Navegador = navegador,
            ModeloDispositivo = modeloDispositivo,
            TokenGuid = tokenGuid,
            IndicadorEstado = EstadoEntidad.ACTIVO,
            IndicadorCanal = indicadorCanal,
            FechaRegistro = fecha,
            FechaModificacion = fecha,
        };
    }

    /// <summary>
    /// Método que modifica la entidad que indica que se cerró la sesión
    /// </summary>
    /// <param name="fecha"></param>
    public void CerrarSesion(
        DateTime fecha)
    {
        IndicadorEstado = EstadoEntidad.INACTIVO;
        FechaModificacion = fecha;
    }

    /// <summary>
    /// Método que actualiza el id de conexión
    /// </summary>
    /// <param name="idConexion">Id conexión</param>
    public void ActualizarConexion(string idConexion)
    {
        IdConexion = idConexion;
        IndicadorEstado = EstadoEntidad.ACTIVO;
    }
    #endregion
}
