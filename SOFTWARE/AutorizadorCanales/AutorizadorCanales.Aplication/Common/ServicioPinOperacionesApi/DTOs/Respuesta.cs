namespace AutorizadorCanales.Aplication.Common.ServicioPinOperacionesApi.DTOs;

public class Respuesta
{
    /// <summary>
    /// Código de error de la respuesta
    /// </summary>
    public string Codigo { get; set; }

    /// <summary>
    /// Mensaje del error de la respuesta
    /// </summary>
    public string Mensaje { get; set; }

    /// <summary>
    /// Contenido de la respuesta
    /// </summary>
    public object Datos { get; set; }

    /// <summary>
    /// Constructor de la clase
    /// </summary>
    public Respuesta() { }
}
