namespace AutorizadorCanales.Logging.Interfaz;

/// <summary>
/// Definición de las propiedades necesarias para generar la información
/// del sistema de bitacorización.
/// </summary>
public interface IContextoBitacora
{
    /// <summary>
    /// ID para identificar al evento como parte de una transacción, se debe
    /// generar cuando la operación inicia en el backend.
    /// </summary>
    string IdSesion { get; }
    /// <summary>
    /// Código de Usuario que genera el evento.
    /// </summary>
    string CodigoUsuario { get; }
    /// <summary>
    /// Identidad de usuario
    /// </summary>
    string IdentidadUsuario { get; }
    /// <summary>
    /// Código de la agencia desde donde se genera el evento, por lo general donde
    /// se encuentra activo el usuario.
    /// </summary>
    string CodigoAgencia { get; }
    /// <summary>
    /// Código del canal en el cual se esta ejecutando la operación que genera
    /// el evento.
    /// </summary>
    string IndicadorCanal { get; }
    /// <summary>
    /// Código del sub canal en el cual se esta ejecutando la operación que
    /// genera el evento.
    /// </summary>
    byte IndicadorSubCanal { get; }
    /// <summary>
    /// ID del terminal (nombre o IP) de donde llega la operación.
    /// </summary>
    string IdTerminalOrigen { get; }
    /// <summary>
    /// ID del servicio que genera el evento.
    /// </summary>
    string IdServicio { get; }
}