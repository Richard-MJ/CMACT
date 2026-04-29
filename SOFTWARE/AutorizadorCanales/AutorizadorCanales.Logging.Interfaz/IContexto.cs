namespace AutorizadorCanales.Logging.Interfaz;

/// <summary>
/// Interface que representa datos de contexto a nivel de aplicación.
/// </summary>
public interface IContexto
{
    /// <summary>
    /// Identificador del procesode login realizoda al ingresar al sistema cliente.
    /// </summary>
    string IdLogin { get; }

    /// <summary>
    /// Un identificar de la sesión en la cual se ejecuta una operación.
    /// </summary>
    string IdSesion { get; }

    /// <summary>
    /// Fecha (día y hora) según el calendario del sistema con el cual se realizó el login.
    /// Por lo general el sistema es CC.
    /// </summary>
    DateTime FechaSistema { get; }
    DateTime FechaHoraServidor { get; }
    string IndicadorCanal { get; }
    byte IndicadorSubCanal { get; }
    string IdTerminal { get; }
    string CodigoUsuario { get; }
    string CodigoAgencia { get; }
    string IdServicio { get; }
    string ModeloDispositivo { get; }
    string DireccionIp { get; }
    string Navegador { get; }
    string SistemaOperativo { get; }
    string IdUsuarioAutenticado { get; }
    string IdAudiencia { get; set; }

    /// <summary>
    /// Token para operaciones
    /// </summary>
    string Token { get; }


    void ActualizarDatos(
        string idLogin,
        string codigoUsuario,
        string codigoAgencia,
        string canalOrigen,
        byte subCanalOrigen,
        string nombreEquipo,
        string modeloDispositivo,
        string direccionIp,
        string navegador,
        string sistemaOperativo,
        string token,
        string audiencia,
        string idUsuarioAutenticado,
        string identidadUsuario);

    bool EsAppHomeBanking();
    bool EsCanalEmpresarial();
    bool EsKiosko();
    /// <summary>
    /// Metodo apertura de canal electronico
    /// </summary>
    /// <returns></returns>
    bool EsAperturaCanalElectronico();

    void ActualizarDatos(
        string modeloDispositivo,
        string direccionIp,
        string navegador,
        string sistemaOperativo, 
        string indicadorCanal);

    void ActualizarDatos(
        string codigoAgencia,
        string codigoUsuario,
        string terminal);
}
