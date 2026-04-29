using AutorizadorCanales.Core.Constantes;
using AutorizadorCanales.Logging.Interfaz;

namespace AutorizadorCanales.Infrastructure.Modelos;

public class Sesion : IContextoBitacora, IContextoApiColas, IContexto
{
    public string IdSesion { get; set; }
    public string CodigoUsuario { get; set; }
    public string CodigoAgencia { get; set; }
    public string IndicadorCanal { get; set; }
    public byte IndicadorSubCanal { get; set; }
    public string IdTerminalOrigen { get; set; }
    public string IdServicio => "ApiAutorizador";
    public string IdLogin { get; set; }
    public string IdAudiencia { get; set; }
    public string IdUsuarioAutenticado { get; set; }
    public string IdTerminalLogin { get; set; }
    public string IdCanalOrigen { get; set; }
    public DateTime FechaSistema { get; set; }
    public DateTime FechaHoraServidor => DateTime.Now;
    public string IdTerminal { get; set; }
    public string ModeloDispositivo { get; set; }
    public string DireccionIp { get; set; }
    public string Navegador { get; set; }
    public string SistemaOperativo { get; set; }
    public string NombreImpresora { get; set; }
    public string Token { get; set; }

    public string IdentidadUsuario { get; set; }

    public Sesion(string idSesion, string idTerminalCliente, string idTerminalLogin)
    {
        IdSesion = idSesion;
        var identityActual = Environment.UserName;
        CodigoUsuario =
            identityActual.Substring(identityActual.IndexOf("\\", StringComparison.Ordinal) + 1);
        CodigoAgencia = "01";
        IndicadorCanal = "";
        IndicadorSubCanal = 0;
        IdTerminalOrigen = idTerminalCliente;
        IdTerminalLogin = idTerminalLogin == null ? "" : idTerminalLogin;
    }

    public Sesion(
        string idSesion,
        string idTerminalCliente,
        string idTerminalLogin,
        string modeloDispositivo,
        string direccionIp,
        string navegador,
        string sistemaOperativo,
        string token,
        string indicadorCanal,
        string subCanalOrigen,
        string audiencia,
        string idUsuarioAutenticado)
    {
        IdSesion = idSesion;
        var identityActual = Environment.UserName;
        CodigoUsuario =
            identityActual.Substring(identityActual.IndexOf("\\", StringComparison.Ordinal) + 1);
        CodigoAgencia = "01";
        IndicadorCanal = indicadorCanal;
        IndicadorSubCanal = (byte)(string.IsNullOrEmpty(subCanalOrigen) ? 0 : Convert.ToByte(subCanalOrigen));
        IdTerminalOrigen = idTerminalCliente;
        IdTerminalLogin = string.IsNullOrEmpty(idTerminalLogin) ? string.Empty : idTerminalLogin;
        ModeloDispositivo = modeloDispositivo;
        DireccionIp = direccionIp;
        Navegador = navegador;
        SistemaOperativo = sistemaOperativo;
        Token = token;
        IdAudiencia = audiencia;
        IdUsuarioAutenticado = idUsuarioAutenticado;
    }

    public Sesion()
    {
        IdSesion = Guid.NewGuid().ToString();
        var identityActual = Environment.UserName;
        CodigoUsuario =
            identityActual.Substring(identityActual.IndexOf("\\", StringComparison.Ordinal) + 1);
        CodigoAgencia = "01";
        IndicadorCanal = "";
        IndicadorSubCanal = 0;
        IdTerminalOrigen = "";
        IdTerminalLogin = "";
        Token = "";
    }

    public void ActualizarSesion(string idLogin, string idAudiencia, string idUsuarioAutenticado,
        string idTerminalLogin, string idCanalOrigen, string codigoUsuario,
        string codigoAgencia, byte indicadorSubCanal)
    {
        IdLogin = idLogin;
        IdSesion = idLogin;
        IdAudiencia = idAudiencia;
        IdUsuarioAutenticado = idUsuarioAutenticado;
        IdTerminalLogin = idTerminalLogin;
        IdCanalOrigen = idCanalOrigen;
        CodigoUsuario = codigoUsuario;
        CodigoAgencia = codigoAgencia;
        IndicadorSubCanal = indicadorSubCanal;
    }

    public bool EsAppHomeBanking()
    {
        return IndicadorCanal == CanalElectronicoConstante.HOME_BANKING || IndicadorCanal == CanalElectronicoConstante.APP_CMACT;
    }

    public bool EsCanalEmpresarial()
    {
        return IndicadorCanal == CanalElectronicoConstante.BANKING_EMPRESARIAL;
    }

    public bool EsKiosko() =>
        IndicadorCanal == CanalElectronicoConstante.KIOSCO;

    public bool EsAperturaCanalElectronico() => IndicadorSubCanal == CanalElectronicoConstante.SUB_CANAL_APERTURA_WEB;

    public void ActualizarDatos(
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
        string identidadUsuario)
    {
        IdLogin = idLogin;
        CodigoUsuario = codigoUsuario;
        CodigoAgencia = codigoAgencia;
        IdCanalOrigen = canalOrigen;
        IndicadorCanal = canalOrigen;
        IndicadorSubCanal = subCanalOrigen;
        IdTerminal = nombreEquipo;
        IdTerminalOrigen = nombreEquipo;
        IdTerminalLogin = nombreEquipo;
        ModeloDispositivo = modeloDispositivo;
        DireccionIp = direccionIp;
        Navegador = navegador;
        SistemaOperativo = sistemaOperativo;
        Token = token;
        FechaSistema = DateTime.Now;
        IdAudiencia = audiencia;
        IdUsuarioAutenticado = idUsuarioAutenticado;
        IdentidadUsuario = identidadUsuario;
    }

    public void ActualizarDatos(
        string modeloDispositivo,
        string direccionIp,
        string navegador,
        string sistemaOperativo,
        string indicadorCanal)
    {
        ModeloDispositivo = modeloDispositivo;
        DireccionIp = direccionIp;
        Navegador = navegador;
        SistemaOperativo = sistemaOperativo;
        IndicadorCanal = indicadorCanal;
    }

    public void ActualizarDatos(
        string codigoAgencia,
        string codigoUsuario,
        string terminal)
    {
        CodigoAgencia = codigoAgencia;
        CodigoUsuario = codigoUsuario;
        IdTerminal = terminal;
    }
}
