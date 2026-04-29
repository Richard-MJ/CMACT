using AutorizadorCanales.Aplication.Features.Autenticacion.Queries;
using AutorizadorCanales.Aplication.Common.JwtGenerator;
using AutorizadorCanales.Domain.Entidades.SG;
using AutorizadorCanales.Domain.Repositorios;
using AutorizadorCanales.Logging.Interfaz;
using AutorizadorCanales.Excepciones;
using MediatR;

namespace AutorizadorCanales.Aplication.Features.Autenticacion.Handlers;

public class ValidarTokenQueryHandler : IRequestHandler<ValidarTokenQuery, int>
{
    private IContexto _contexto;
    private IRepositorioLectura _repositorioLectura;
    private IJwtTokenGenerator _jwtTokenGenerator;

    public ValidarTokenQueryHandler(IRepositorioLectura repositorioLectura, IJwtTokenGenerator jwtTokenGenerator, IContexto contexto)
    {
        _repositorioLectura = repositorioLectura;
        _jwtTokenGenerator = jwtTokenGenerator;
        _contexto = contexto;
    }

    /// <summary>
    /// Handler del query ValidarToken
    /// </summary>
    /// <param name="query">Query con los datos para validar el token</param>
    /// <returns></returns>
    public async Task<int> Handle(ValidarTokenQuery query, CancellationToken cancellationToken)
    {
        var guidToken = _jwtTokenGenerator.ObtenerGuidToken(_contexto.Token);

        if (_contexto.EsKiosko())
            return 1;

        if (_contexto.EsAperturaCanalElectronico())
            return 1;

        if (_contexto.EsAppHomeBanking())
            return 1;

        var sesionCanalElectronico = await _repositorioLectura.ObtenerPrimeroPorAsync<SesionCanalElectronico>
                (x => x.TokenGuid == guidToken)
            ?? throw new ExcepcionAUsuario("06", "No existe la sesión");

        if (!sesionCanalElectronico.EstaActiva)
            throw new ExcepcionAUsuario("06", "La sesión ya ha sido finalizada");

        return 1;
    }
}
