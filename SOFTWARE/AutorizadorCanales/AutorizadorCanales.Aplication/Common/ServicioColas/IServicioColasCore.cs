namespace AutorizadorCanales.Aplication.Common.ServicioColas;

public interface IServicioColasCore
{
    /// <summary>
    /// Enviar a notificacion
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="datos"></param>
    void EnviarNotificacion<T>(T datos) where T : class;
}
