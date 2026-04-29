using AutoMapper;
using AutorizadorCanales.Aplication.Features.Audiencias.Queries;
using AutorizadorCanales.Contracts.SG.Response;
using AutorizadorCanales.Domain.Repositorios;
using MediatR;
using Entidad = AutorizadorCanales.Domain.Entidades.SG;

namespace AutorizadorCanales.Aplication.Features.Audiencias.Handlers;

public class ObtenerAudienciasQueryHandler : IRequestHandler<ObtenerAudienciasQuery, List<AudienciaResponse>>
{
    private readonly IRepositorioLectura _repositorioLectura;
    protected IMapper _mapper;

    public ObtenerAudienciasQueryHandler(IRepositorioLectura repositorioLectura, IMapper mapper)
    {
        _repositorioLectura = repositorioLectura;
        _mapper = mapper;
    }

    public async Task<List<AudienciaResponse>> Handle(ObtenerAudienciasQuery request, CancellationToken cancellationToken)
    {
        var sistemasCliente = await _repositorioLectura
            .ObtenerPorExpresionConLimiteAsync<Entidad.Audiencia>();

        return _mapper.Map<List<Entidad.Audiencia>, List<AudienciaResponse>>(sistemasCliente);
    }
}
