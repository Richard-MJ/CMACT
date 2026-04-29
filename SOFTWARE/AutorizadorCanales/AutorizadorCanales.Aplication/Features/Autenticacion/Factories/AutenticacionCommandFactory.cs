using AutorizadorCanales.Aplication.Features.Autenticacion.Commands;
using AutorizadorCanales.Contracts.SG.Autenticacion;
using AutorizadorCanales.Contracts.SG.Response;
using AutorizadorCanales.Logging.Interfaz;
using MediatR;
using MyCSharp.HttpUserAgentParser;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace AutorizadorCanales.Aplication.Features.Autenticacion.Factories;

public interface IAutenticacionCommandFactory
{
    /// <summary>
    /// Crea el comando respectivo según el tipo de autenticación
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="tarjeta">Entidad tarjeta</param>
    /// <param name="audiencia">Audiencia</param>
    /// <param name="idTrama">Id de la trama</param>
    /// <param name="userAgent">User agent</param>
    /// <param name="direccionIp">Dirección IP</param>
    /// <returns></returns>
    IRequest<JsonObject> CrearComando
        (AutenticacionRequest request, AudienciaResponse audiencia, int idTrama, string userAgent, string direccionIp);
}

public class AutenticacionCommandFactory : IAutenticacionCommandFactory
{
    private readonly IContexto _contexto;
    private readonly IBitacora<AutenticacionCommandFactory> _bitacora;

    public AutenticacionCommandFactory(IContexto contexto, IBitacora<AutenticacionCommandFactory> bitacora)
    {
        _contexto = contexto;
        _bitacora = bitacora;
    }

    /// <summary>
    /// Crea el comando respectivo según el tipo de autenticación
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="tarjeta">Entidad tarjeta</param>
    /// <param name="audiencia">Audiencia</param>
    /// <param name="idTrama">Id de la trama</param>
    /// <param name="userAgent">User agent</param>
    /// <param name="direccionIp">Dirección IP</param>
    /// <returns></returns>
    public IRequest<JsonObject> CrearComando(AutenticacionRequest request, AudienciaResponse audiencia, int idTrama, string userAgent, string direccionIp)
    {
        _bitacora.Info(JsonSerializer.Serialize(new Dictionary<string, object>
        {
            { "accion", 19 },
            { "grant_type", request.grant_type ?? "" },
            { "username", request.username ?? "" },
            { "client_id", request.client_id ?? "" },
            { "pingblock", "" },
            { "tipo_operacion", request.idTipoOperacionCanalElectronico }
        }));

        ActualizarContextoUsuario(request, audiencia, userAgent, direccionIp);

        if (request.grant_type == "refresh_token")
            return new RefrescarTokenCommand(request.refresh_token!, audiencia);

        if (audiencia.IndicadorCanal == "K")
            return new AutenticarKioskoCommand(request.username, request.password, idTrama, request.terminal, request.usuario, audiencia);


        if (request.idTipoOperacionCanalElectronico == 4)
            return new AutenticarAperturaProductoCommand(
                    request.username,
                    request.terminal,
                    request.idTipoDocumento,
                    request.idTipoOperacionCanalElectronico,
                    request.guids,
                    request.modeloDispositivo,
                    audiencia,
                    idTrama);

        return new AutenticarClienteCommand(
                    request.username,
                    request.password,
                    request.passwordPrimario,
                    request.terminal,
                    request.numeroDocumento,
                    request.idTipoDocumento,
                    request.idTipoOperacionCanalElectronico,
                    request.guids,
                    request.modeloDispositivo,
                    audiencia,
                    idTrama);
    }

    private void ActualizarContextoUsuario(AutenticacionRequest request, AudienciaResponse audiencia, string userAgent, string direccionIp)
    {
        direccionIp = request.ipRequest ?? direccionIp;
        userAgent = request.userAgent ?? userAgent;
        var modeloDispositivo = request.modeloDispositivo ?? "--";
        var info = HttpUserAgentParser.Parse(userAgent);
        var navegador = info.Name ?? "--";
        var sistemaOperativo = info.Platform == null ? "--" : info.Platform.Value.Name;

        _contexto.ActualizarDatos(modeloDispositivo, direccionIp, navegador, sistemaOperativo, audiencia.IndicadorCanal);
    }
}
