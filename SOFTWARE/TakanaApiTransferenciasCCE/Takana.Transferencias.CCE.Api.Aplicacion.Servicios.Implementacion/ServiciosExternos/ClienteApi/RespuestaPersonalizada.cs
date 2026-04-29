namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.ServiciosExternos
{
    /// <summary>
    /// Clase que contiene la estructura de respuesta de las peticiones cuando
    /// se produce alguna validación o BadRequest en respuesta del servicio
    /// API
    /// </summary>
    public class RespuestaPersonalizada
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
        /// Constructor de la clase
        /// </summary>
        public RespuestaPersonalizada() { }
    }
}