namespace AutorizadorCanales.Aplication.Servicios.Autenticacion.DTOs;

/// <summary>
/// Dto sesion para el logeo de usuarios
/// </summary>
public class DtoSesion
{
    /// <summary>
    /// Numero de tarjeta
    /// </summary>
    public string NumeroTarjeta { get; set; }
    /// <summary>
    /// Codigo de usuario
    /// </summary>
    public string IdVisual { get; set; }
    /// <summary>
    /// Primer nombre usuario
    /// </summary>
    public string PrimerNombreUsuario { get; set; }
    /// <summary>
    /// Segundo nombre usuario
    /// </summary>
    public string SegundoNombreUsuario { get; set; }
    /// <summary>
    /// Apellido usuario
    /// </summary>
    public string ApellidoUsuario { get; set; }
}
