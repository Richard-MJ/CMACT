namespace AutorizadorCanales.Aplication.Common.ServicioPinOperacionesApi;

public interface IServicioPinOperaciones
{
    /// <summary>
    /// Metodo para encriptar clave
    /// </summary>
    /// <param name="pan">Numero de tarjeta</param>
    /// <param name="pin">Clave</param>
    /// <returns>Retorna un objeto pinpropiedaes con un estado, mensaje</returns>
    Task<string> TrasladaPINBlock(string pan, string pin);

    Task<bool> ValidarClave(string numeroTarjeta, string pinblock, string numeroPvv);

    Task<string> GenerarPvv(string numeroTarjeta, string pinblock);

    /// <summary>
    /// Obtiene los pinblocks
    /// </summary>
    /// <param name="password">Contraseña</param>
    /// <param name="numeroTarjeta">Número de tarjeta</param>
    /// <returns></returns>
    Task<Tuple<string, string>> ObtenerPinBlock(string password, string numeroTarjeta);
}
