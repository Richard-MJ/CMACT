namespace AutorizadorCanales.Domain.Entidades.SG;

public class AlertaInicioSesion
{
    /// <summary>
    /// Numero identificador del registro
    /// </summary>
    public int NumeroIdentificador { get; private set; }
    /// <summary>
    /// Numero de tarjeta del cliente
    /// </summary>
    public decimal? NumeroTarjeta { get; private set; }
    /// <summary>
    /// Indicador de estado del inicio de sesion 
    /// S: Exitoso, N: No exitoso
    /// </summary>
    public string? IndicadorEstado { get; private set; } = null!;
    /// <summary>
    /// Propiedad que representa el canal 
    /// </summary>
    public string? IndicadorCanal { get; private set; } = null!;
    /// <summary>
    /// Direccion IP del cliente
    /// </summary>
    public string? DireccionIp { get; private set; } = null!;
    /// <summary>
    /// Sistema operativo del inicio de sesion
    /// </summary>
    public string? SistemaOperativo { get; private set; } = null!;
    /// <summary>
    /// Navegador del inicio de sesion
    /// </summary>
    public string? Navegador { get; private set; } = null!;
    /// <summary>
    /// Ubicacion del inicio de sesion
    /// </summary>
    public string? Ubicacion { get; private set; }
    /// <summary>
    /// Identificador del dispositivo donde se realizo la alerta
    /// </summary>
    public string? IdRegistroDispositivo { get; set; } = null!;
    /// <summary>
    /// Fecha en la que se registro el inicio de sesion
    /// </summary>
    public DateTime? FechaRegistro { get; private set; }
    /// <summary>
    /// Modelo de dispositivo
    /// </summary>
    public string ModeloDispositivo { get; private set; } = null!;
    /// <summary>
    /// Numero de movimiento
    /// </summary>
    public decimal NumeroMovimiento { get; private set; }
    
    /// <summary>
    /// Metodo para generar una nueva alerta inicio de sesion
    /// </summary>
    /// <param name="tarjeta">tarjeta</param>
    /// <param name="indicadorEstado">Indicador de estado de la alerta</param>
    /// <param name="direccionIp">IP Address donde se realizo la operacion</param>
    /// <param name="dispositivo">Dispositivo donde se realizo la operacion</param>
    /// <param name="navegador">Navegador donde se realizo la operacion</param>
    /// <param name="indicadorCanal">Canal</param>        
    /// <param name="calendario">Entidad calendario</param>
    /// <param name="registroDispositivo">Identificador del dispositivo</param>        
    /// <returns>Entidad alertainiciosesion</returns>
    public static AlertaInicioSesion Crear(decimal numeroTarjeta, string indicadorEstado, string direccionIp,
        string sistemaOperativo, string navegador, string indicadorCanal, string registroDispositivo, DateTime fechaSistema,
        string modeloDispositivo, decimal numeroMovimiento)
    {
        return new AlertaInicioSesion
        {
            NumeroTarjeta = numeroTarjeta,
            IndicadorEstado = indicadorEstado,
            DireccionIp = direccionIp,
            SistemaOperativo = sistemaOperativo,
            IdRegistroDispositivo = registroDispositivo,
            Navegador = navegador,
            IndicadorCanal = indicadorCanal,
            FechaRegistro = fechaSistema,
            ModeloDispositivo = modeloDispositivo,
            NumeroMovimiento = numeroMovimiento,
        };
    }
}
