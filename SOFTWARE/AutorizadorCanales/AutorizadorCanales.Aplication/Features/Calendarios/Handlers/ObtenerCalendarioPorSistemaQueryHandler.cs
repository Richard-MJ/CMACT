using AutoMapper;
using AutorizadorCanales.Aplication.Features.Calendarios.Queries;
using AutorizadorCanales.Core.DTO;
using AutorizadorCanales.Domain.Entidades;
using AutorizadorCanales.Domain.Entidades.CF;
using AutorizadorCanales.Domain.Repositorios;
using MediatR;

namespace AutorizadorCanales.Aplication.Features.Calendarios.Handlers;

public class ObtenerCalendarioPorSistemaQueryHandler : IRequestHandler<ObtenerCalendarioPorSistemaQuery, CalendarioDto>
{
    private readonly IRepositorioLectura _repositorioLectura;
    protected IMapper _mapper;

    public ObtenerCalendarioPorSistemaQueryHandler(IRepositorioLectura repositorioLectura, IMapper mapper)
    {
        _repositorioLectura = repositorioLectura;
        _mapper = mapper;
    }

    /// <summary>
    /// Maneja la query obtener calendario por sistema
    /// </summary>
    /// <param name="query">Datos de la query</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Dto calendario</returns>
    public async Task<CalendarioDto> Handle(ObtenerCalendarioPorSistemaQuery query, CancellationToken cancellationToken)
    {
        var calendario = await _repositorioLectura.ObtenerPorCodigoAsync<Calendario>
        (EntidadEmpresa.EMPRESA, query.CodigoAgencia, query.CodigoSistema);

        return _mapper.Map<Calendario, CalendarioDto>(calendario);
    }
}
