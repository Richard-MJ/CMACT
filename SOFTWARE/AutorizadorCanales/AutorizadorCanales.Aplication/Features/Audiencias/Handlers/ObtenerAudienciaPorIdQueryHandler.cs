using AutoMapper;
using AutorizadorCanales.Aplication.Features.Audiencias.Queries;
using AutorizadorCanales.Contracts.SG.Response;
using AutorizadorCanales.Domain.Repositorios;
using MediatR;
using Entidad = AutorizadorCanales.Domain.Entidades.SG;

namespace AutorizadorCanales.Aplication.Features.Audiencias.Handlers;

public class ObtenerAudienciaPorIdQueryHandler : IRequestHandler<ObtenerAudienciaPorIdQuery, AudienciaResponse>
{
    private readonly IRepositorioLectura _repositorioLectura;
    protected IMapper _mapper;

    public ObtenerAudienciaPorIdQueryHandler(IRepositorioLectura repositorioLectura, IMapper mapper)
    {
        _repositorioLectura = repositorioLectura;
        _mapper = mapper;
    }

    public async Task<AudienciaResponse> Handle(ObtenerAudienciaPorIdQuery query, CancellationToken cancellationToken)
    {
        var sistemasCliente = await _repositorioLectura
                .ObtenerPorCodigoAsync<Entidad.Audiencia>(query.IdAudiencia);

        return _mapper.Map<Entidad.Audiencia, AudienciaResponse>(sistemasCliente);
    }
}
