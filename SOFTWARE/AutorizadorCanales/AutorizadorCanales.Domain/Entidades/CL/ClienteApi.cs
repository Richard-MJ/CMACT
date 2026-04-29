using AutorizadorCanales.Core.Constantes;
using AutorizadorCanales.Domain.Entidades.SG;
using AutorizadorCanales.Domain.Entidades.TJ;
using AutorizadorCanales.Excepciones;

namespace AutorizadorCanales.Domain.Entidades.CL;

/// <summary>
/// Clase que representa la entidad de bitacora del cliente 
/// </summary>
public class ClienteApi : EntidadEmpresa
{
    /// <summary>
    /// Id del cliente API
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// Id del visual
    /// </summary>
    public string IdVisual { get; private set; } = null!;
    /// <summary>
    /// Id de la audiencia
    /// </summary>
    public string IdSistemaCliente { get; private set; } = null!;
    /// <summary>
    /// Codigo del cliente financiero
    /// </summary>
    public string CodigoCliente { get; private set; } = null!;
    /// <summary>
    /// Indicador de estado
    /// </summary>
    public string IndicadorEstado { get; private set; } = null!;
    /// <summary>
    /// Fecha del primer intento fallido
    /// </summary>
    public DateTime? FechaPrimerIntentoFallido { get; private set; }
    /// <summary>
    /// Numero de intentos fallidos
    /// </summary>
    public int NumeroIntentosFallidos { get; private set; }
    /// <summary>
    /// Fecha del fin de bloqueo
    /// </summary>
    public DateTime? FechaFinBloqueo { get; private set; }
    /// <summary>
    /// Numero de tarjeta del cliente
    /// </summary>
    public decimal? NumeroTarjeta { get; private set; }
    /// <summary>
    /// Usuario SAF con el cual se genera el registro, ya que varios usuarios pueden tener el mismo codigo cliente.
    /// Sera null en caso sea un cliente de canales electronicos donde se usa tarjeta.
    /// </summary>
    public string? CodigoUsuarioSaf { get; private set; } = null!;
    /// <summary>
    /// Descripcion del motivo del fallo
    /// </summary>
    public string? DescripcionMotivoFallo { get; private set; } = null!;
    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime? FechaRegistro { get; private set; }
    /// <summary>
    /// Fecha de modificacion
    /// </summary>
    public DateTime? FechaModificacion { get; private set; }
    /// <summary>
    /// Indicador de bloqueo para las operaciones con SMS
    /// </summary>
    public bool IndicadorBloqueoSms { get; private set; }
    /// <summary>
    /// Numero de intento fallido con el SMS
    /// </summary>
    public int NumeroIntentoFallidoSms { get; private set; }
    /// <summary>
    /// Fecha de registro de intentos fallidos con el SMS
    /// </summary>
    public DateTime? FechaRegistroIntentoFallidoSms { get; private set; }
    /// <summary>
    /// Fecha de fin de bloqueo
    /// </summary>
    public DateTime? FechaFinBloqueoSms { get; private set; }

    /// <summary>
    /// Instancia de la entidad SistemaCliente
    /// </summary>
    public virtual Audiencia SistemaCliente { get; private set; } = null!;

    /// <summary>
    /// Constantes
    /// </summary>
    public const string BLOQUEADO = "B";
    public const string DESAFILIADO = "D";
    public const string NULO = "N";
    public const string AFILIADO = "A";

    /// <summary>
    /// Validar si es cliente bloqueado
    /// </summary>
    public bool EsClienteBloqueado => IndicadorEstado == BLOQUEADO;

    public static ClienteApi Crear(string idSistemaCliente,
        Tarjeta tarjeta)
    {
        return new ClienteApi
        {
            IdVisual = Guid.NewGuid().ToString("N"),
            IdSistemaCliente = idSistemaCliente,
            CodigoEmpresa = tarjeta.CodigoEmpresa,
            CodigoCliente = tarjeta.CodigoCliente,
            IndicadorEstado = AFILIADO,
            FechaPrimerIntentoFallido = DateTime.Now,
            NumeroIntentosFallidos = 0,
            DescripcionMotivoFallo = string.Empty,
            NumeroTarjeta = tarjeta.NumeroTarjeta,
            FechaFinBloqueo = DateTime.Now,
            FechaRegistro = DateTime.Now,
            FechaModificacion = DateTime.Now,
            FechaRegistroIntentoFallidoSms = DateTime.Parse("01/01/1990"),
            FechaFinBloqueoSms = DateTime.Parse("01/01/1990")
        };
    }

    /// <summary>
    /// Ejecuta tareas (validaciones y bloqueos) por ingresar una clave inválida.
    /// </summary>
    /// <param name="fechaActual">Fecha en que se ingresa una clave invalida.</param>
    /// <param name="segundosRangoIntentosFallidos">Numero de segundos fallidos permitidos</param>
    /// <param name="maximoIntentosFallidos">Máximo de intentos fallidos para bloquear.</param>
    /// <param name="horasBloqueo">Cuantas horas se bloquea el cliente en el servicio.</param>
    /// <param name="indicadorCanal">Indicador del canal de la operacion</param>
    /// <returns>Devuelve el mismo objeto que invoca el método.</returns>
    public ClienteApi RegistrarIngresoClaveInvalida(DateTime fechaActual,
        int segundosRangoIntentosFallidos, int maximoIntentosFallidos,
        int horasBloqueo, string indicadorCanal = "")
    {
        if (NumeroIntentosFallidos <= 0)
        {
            FechaPrimerIntentoFallido = fechaActual;
        }

        if (IndicadorEstado == BLOQUEADO && fechaActual > FechaFinBloqueo)
        {
            NumeroIntentosFallidos = 1;
            IndicadorEstado = AFILIADO;

            return this;
        }

        NumeroIntentosFallidos++;

        if (NumeroIntentosFallidos >= maximoIntentosFallidos && indicadorCanal != CanalElectronicoConstante.KIOSCO)
        {
            IndicadorEstado = BLOQUEADO;
            FechaFinBloqueo = FechaPrimerIntentoFallido +
                              TimeSpan.FromHours(horasBloqueo);
        }

        return this;
    }

    /// <summary>
    /// Ejecuta tareas (validaciones y desbloqueos) por ingresar una clave válida
    /// </summary>
    /// <param name="fechaActual">Fecha en que se ingresa una clave válida.</param>
    /// <returns>Devuelve el mismo objeto que invoca el método.</returns>
    public ClienteApi RegistrarIngresoClaveValida(DateTime fechaActual)
    {
        if (IndicadorEstado == BLOQUEADO && FechaFinBloqueo > fechaActual)
        {
            return this;
        }

        if (IndicadorEstado == BLOQUEADO) IndicadorEstado = AFILIADO;

        NumeroIntentosFallidos = 0;
        DescripcionMotivoFallo = string.Empty;
        FechaModificacion = DateTime.Now;
        return this;
    }

    /// <summary>
    /// Valida el estado del cliente en el canal electrónico, generando excepciones en determinados estados.
    /// </summary>
    /// <returns>Devuelve el mismo objeto que invoca el método.</returns>
    public ClienteApi ValidarEstado()
    {
        if (IndicadorEstado == BLOQUEADO)
        {
            // TODO debe obtener de mensajes de sistema.
            throw new ExcepcionAUsuario("06", "Cliente con bloqueo parcial.");
        }

        if (IndicadorEstado == DESAFILIADO)
        {
            // TODO debe obtener de mensajes de sistema.
            throw new ExcepcionAUsuario("06", "Debe afiliarse previamente.");
        }

        if (IndicadorEstado != AFILIADO)
        {
            // TODO debe obtener de mensajes de sistema.
            throw new ExcepcionAUsuario("06", "Cliente no puede realizar operaciones por este canal.");
        }

        return this;
    }

    /// <summary>
    /// Metodo que permite actualizar el mensaje de intentos fallidos
    /// </summary>
    /// <param name="maximoIntentosFallidos">Numero de intentos fallidos</param>
    public void ActualizarMensaje(int maximoIntentosFallidos)
    {
        DescripcionMotivoFallo = $"Te quedan {maximoIntentosFallidos - NumeroIntentosFallidos} intento(s).";
    }

    /// <summary>
    /// Metodo que permite modificar los datos de intentos fallidos
    /// </summary>
    /// <param name="clienteApi">Entidad ClienteApi</param>
    public void ModificarDatosDeIntentosFallidos(ClienteApi clienteApi)
    {
        NumeroIntentosFallidos = clienteApi.NumeroIntentosFallidos;
        DescripcionMotivoFallo = clienteApi.DescripcionMotivoFallo;
        FechaPrimerIntentoFallido = clienteApi.FechaPrimerIntentoFallido;
        FechaFinBloqueo = clienteApi.FechaFinBloqueo;
        IndicadorEstado = clienteApi.IndicadorEstado;
        FechaModificacion = DateTime.Now;
    }

    public void Desafiliar()
    {
        IndicadorEstado = DESAFILIADO;
    }
}
