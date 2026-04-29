using AutorizadorCanales.Aplication.Features.Autenticacion.Commands;
using System.Text.Json.Nodes;

namespace AutorizadorCanales.Aplication.Servicios.Autenticacion;

public interface IServicioRefrescoAcceso
{
    /// <summary>
    /// Refrescar token de cliente
    /// </summary>
    /// <param name="datos">Datos para generar el refresco</param>
    /// <returns>Retorna los datos de acceso</returns>
    Task<JsonObject> RefrescarTokenCliente(RefrescarTokenCommand datos);

    /// <summary>
    /// Valida el cliente para accesos
    /// </summary>
    /// <param name="numeroTarjeta">Número de tarjeta</param>
    /// <param name="idAudiencia">Id del sistema</param>
    /// <param name="idVisual">Id visual</param>
    /// <returns></returns>
    Task<bool> ValidarClienteParaAcceso(string numeroTarjeta, string idAudiencia, string idVisual);
}
