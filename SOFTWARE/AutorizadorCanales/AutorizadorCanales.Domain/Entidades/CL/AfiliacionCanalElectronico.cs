using AutorizadorCanales.Domain.Entidades.TJ;

namespace AutorizadorCanales.Domain.Entidades.CL;

public class AfiliacionCanalElectronico
{
    #region Propieades
    /// <summary>
    /// Id de la afiliacion del canal electronico
    /// </summary>
    public int IdAfiliacionCanalElectronico { get; private set; }
    /// <summary>
    /// Numero de tarjeta
    /// </summary>
    public decimal NumeroTarjeta { get; private set; }
    /// <summary>
    /// Fecha de la afiliacion principal
    /// </summary>
    public DateTime FechaAfiliacionPrincipal { get; private set; }
    /// <summary>
    /// Indicador de la afiliacion principal
    /// </summary>
    public bool IndicadorAfiliacionPrincipal { get; private set; }
    /// <summary>
    /// Fecha de confiracion de la afiliacion
    /// </summary>
    public DateTime FechaConfirmacionAfiliacion { get; private set; }
    /// <summary>
    /// Indicador de confirmacion de la afiliacion
    /// </summary>
    public bool IndicadorConfirmacionAfiliacion { get; private set; }
    /// <summary>
    /// Fecha de la afiliacion del SMS
    /// </summary>
    public DateTime FechaAfiliacionSms { get; private set; }
    /// <summary>
    /// Indicador de la afiliacion del SMS
    /// </summary>
    public bool IndicadorAfiliacionSms { get; private set; }
    /// <summary>
    /// Fecha de la confirmacion del SMS
    /// </summary>
    public DateTime FechaConfirmacionSms { get; private set; }
    /// <summary>
    /// Indicador de la confirmacion del SMS
    /// </summary>
    public bool IndicadorConfirmacionSms { get; private set; }
    /// <summary>
    /// Fecha de cambio de clave de la tarjeta
    /// </summary>
    public DateTime FechaCambioClaveTarjeta { get; private set; }
    /// <summary>
    /// Indicador de cambio de clave de tarjeta
    /// </summary>
    public bool IndicadorCambioClaveTarjeta { get; private set; }
    /// <summary>
    /// Fecha de cambio de clave de internet
    /// </summary>
    public DateTime FechaCambioClaveInternet { get; private set; }
    /// <summary>
    /// Indicador de cambio de clave de internet
    /// </summary>
    public bool IndicadorCambioClaveInternet { get; private set; }
    /// <summary>
    /// Numero de intentos de clave de internet
    /// </summary>
    public int NumeroIntentosClaveInternet { get; private set; }
    /// <summary>
    /// Fecha de caducidad de la clave de internet
    /// </summary>
    public DateTime FechaCaducidadClaveInternet { get; private set; }
    /// <summary>
    /// Id del api usuario
    /// </summary>
    public int IdApiUsuario { get; private set; }
    /// <summary>
    /// Id de la verificacion
    /// </summary>
    public int? IdVerificacion { get; private set; }
    /// <summary>
    /// Id del dispositivo de la autenticacion
    /// </summary>
    public string? ObservacionAfiliaciones { get; private set; } = null!;
    /// <summary>
    /// Id del dispositivo de la autenticacion
    /// </summary>
    public string? IdDispositivoAutenticacion { get; private set; } = null!;
    /// <summary>
    /// Indicador de estado
    /// </summary>
    public bool IndicadorActivo { get; private set; }
    /// <summary>
    /// Indicador vencido
    /// </summary>
    public bool IndicadorVencido { get; private set; }
    /// <summary>
    /// Codigo de usuario de registro
    /// </summary>
    public string? CodigoUsuarioRegistro { get; private set; } = null!;
    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime FechaRegistro { get; set; }
    /// <summary>
    /// Codigo del usuario de modificacion
    /// </summary>
    public string? CodigoUsuarioModificacion { get; private set; } = null!;
    /// <summary>
    /// Fecha de modificacion
    /// </summary>
    public DateTime FechaModificacion { get; private set; }
    /// <summary>
    /// Modelo de dispositivo
    /// </summary>
    public string ModeloDispositivo { get; private set; } = null!;
    /// <summary>
    /// Dirección IP
    /// </summary>
    public string DireccionIp { get; private set; } = null!;
    /// <summary>
    /// Navegador
    /// </summary>
    public string Navegador { get; private set; } = null!;
    /// <summary>
    /// Sistema Operativo
    /// </summary>
    public string SistemaOperativo { get; private set; } = null!;
    /// <summary>
    /// Tarjeta asociada al dispositivo
    /// </summary>
    public virtual Tarjeta Tarjeta { get; private set; } = null!;
    /// <summary>
    /// Validar si se realizo la afiliacion por sms al cliente
    /// </summary>
    #endregion

    #region Autogeneradas
    public bool EsAfiliacionSms => IndicadorAfiliacionSms
        && IndicadorConfirmacionSms;

    /// <summary>
    /// Validar si la tarjeta del cliente esta afiliada a canales electronicos
    /// </summary>
    public bool EsTarjetaAfiliada => IndicadorAfiliacionPrincipal
        && IndicadorConfirmacionAfiliacion && EsAfiliacionSms;
    #endregion

    #region Métodos
    /// <summary>
    /// Crear la entidad CrearPrimerFactorAutenticacion
    /// </summary>
    /// <param name="clienteApi">Entidad ClienteApi</param>
    /// <param name="usuario">Entidad Usuario</param>
    /// <param name="idDispositivoAutenticacion">Id del dispositivo de autenticacion</param>
    /// <param name="fechaCaducidad">Fecha de caducidad</param>
    /// <returns>Entidad AfiliacionCanalElectronico</returns>
    public static AfiliacionCanalElectronico CrearPrimerFactorAutenticacion(
        ClienteApi clienteApi,
        string codigoUsuario,
        string idDispositivoAutenticacion,
        DateTime fechaCaducidad,
        string modeloDispositivo,
        string direccionIp,
        string navegador,
        string sistemaOperativo)
    {
        return new AfiliacionCanalElectronico
        {
            NumeroTarjeta = clienteApi?.NumeroTarjeta ?? 0,
            FechaAfiliacionPrincipal = DateTime.Now,
            IndicadorAfiliacionPrincipal = true,
            FechaConfirmacionAfiliacion = DateTime.Parse("01/01/1990"),
            IndicadorConfirmacionAfiliacion = false,
            FechaAfiliacionSms = DateTime.Parse("01/01/1990"),
            IndicadorAfiliacionSms = false,
            FechaConfirmacionSms = DateTime.Parse("01/01/1990"),
            IndicadorConfirmacionSms = false,
            FechaCambioClaveTarjeta = DateTime.Parse("01/01/1990"),
            IndicadorCambioClaveTarjeta = false,
            FechaCambioClaveInternet = DateTime.Parse("01/01/1990"),
            IndicadorCambioClaveInternet = false,
            NumeroIntentosClaveInternet = 0,
            FechaCaducidadClaveInternet = fechaCaducidad.Date,
            IdApiUsuario = clienteApi.Id,
            ObservacionAfiliaciones = "Registro correcto del primer factor de autenticacion",
            IdDispositivoAutenticacion = idDispositivoAutenticacion,
            IndicadorActivo = true,
            IndicadorVencido = false,
            CodigoUsuarioRegistro = codigoUsuario,
            FechaRegistro = DateTime.Now,
            FechaModificacion = DateTime.Now,
            ModeloDispositivo = modeloDispositivo,
            DireccionIp = direccionIp,
            Navegador = navegador,
            SistemaOperativo = sistemaOperativo,
        };
    }

    /// <summary>
    /// Metodo para actualizar la confirmacion de la autenticacion
    /// </summary>
    /// <param name="clienteApi">Entidad ClienteApi</param>
    /// <param name="idDispositivoAutenticacion">Id del dispositivo de autenticacion</param>
    public void ActualizarConfirmacionAutenticacion(ClienteApi clienteApi, string idDispositivoAutenticacion)
    {
        IdApiUsuario = clienteApi.Id;
        FechaConfirmacionAfiliacion = DateTime.Now;
        IndicadorConfirmacionAfiliacion = true;
        ObservacionAfiliaciones = "Registro correcto de la confirmacion de la autenticacion";
        IdDispositivoAutenticacion = idDispositivoAutenticacion;
        CodigoUsuarioModificacion = string.Empty;
        FechaModificacion = DateTime.Now;
    }

    /// <summary>
    /// Metodo para desactivar el primer factor de autenticacion
    /// </summary>
    /// <param name="usuario">Entidad Usuario</param>
    /// <param name="idDispositivoAutenticacion">Id del dispositivo de autenticacion</param>
    public void DesactivarPrimerFactorAutenticacion(string codigoUsuario, string idDispositivoAutenticacion)
    {
        ObservacionAfiliaciones = "Desactivado por no confirmar SMS";
        IdDispositivoAutenticacion = idDispositivoAutenticacion;
        IndicadorActivo = false;
        CodigoUsuarioModificacion = codigoUsuario;
        FechaModificacion = DateTime.Now;
    }

    /// <summary>
    /// Metodo para actualizar vigencia de la clave de internet
    /// </summary>
    /// <param name="codigoUsuario">codigo de usuario</param>
    /// <param name="idDispositivoAutenticacion">Id del dispositivo de autenticacion</param>
    public void ActualizarVigenciaDeClaveInternet(string codigoUsuario, string idDispositivoAutenticacion,
        DateTime fechaModificacion)
    {
        IndicadorActivo = false;
        IndicadorVencido = true;
        IdDispositivoAutenticacion = idDispositivoAutenticacion;
        ObservacionAfiliaciones = "Clave de internet caducado";
        CodigoUsuarioModificacion = codigoUsuario;
        FechaModificacion = fechaModificacion;
    }
    #endregion
}