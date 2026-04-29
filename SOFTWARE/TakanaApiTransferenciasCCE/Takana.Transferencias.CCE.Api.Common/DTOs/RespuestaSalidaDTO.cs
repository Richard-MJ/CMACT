namespace Takana.Transferencias.CCE.Api.Common;
/// <summary>
/// Clase que refleja el DTO de TAKANA API CORE en el cliente web api
/// </summary>
public class RespuestaSalidaDTO<T>
{
        /// <summary>
        /// Código del mensaje de respuesta
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Razon del error de la respuesta
        /// </summary>
        public string Razon { get; set; }
        /// <summary>
        /// Mensaje extra
        /// </summary>
        public string RazonExtra { get; set; }
        /// <summary>
        /// Tipo de Respuesta
        /// </summary>
        public string? Tipo { get; set; }

        /// <summary>
        /// Contenido de la respuesta
        /// </summary>
        public T Datos { get; set; }
}
