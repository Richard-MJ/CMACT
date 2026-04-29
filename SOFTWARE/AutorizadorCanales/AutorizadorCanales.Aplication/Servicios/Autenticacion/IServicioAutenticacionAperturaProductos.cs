using AutorizadorCanales.Aplication.Features.Autenticacion.Commands;
using System.Text.Json.Nodes;

namespace AutorizadorCanales.Aplication.Servicios.Autenticacion;

/// <summary>
/// Servicio autenticación
/// </summary>
public interface IServicioAutenticacionAperturaProductos
{
    /// <summary>
    /// Autentica al cliente
    /// </summary>
    /// <param name="datos">Datos para generar el acceso de cliente</param>
    /// <returns>Retorna los datos de acceso</returns>
    Task<JsonObject> AutenticarAperturaProducto(AutenticarAperturaProductoCommand datos);

}
