using AutorizadorCanales.Aplication.Servicios.Autenticacion.DTOs;
using System.Text.Json.Nodes;

namespace AutorizadorCanales.Aplication.Servicios.Autenticacion;

public interface IServicioAutenticacionKiosko
{
    /// <summary>
    /// Método que autentica al cliente en el kiosko
    /// </summary>
    /// <param name="idAudiencia"></param>
    /// <param name="numeroTarjeta"></param>
    /// <param name="password"></param>
    /// <param name="indicadorCanalOrigen"></param>
    /// <returns></returns>
    Task<JsonObject> AutenticarClienteKiosko(string idAudiencia, string numeroTarjeta, string password, int idTrama, string codigoUsuario, string AutenticarClienteKiosko);
}
