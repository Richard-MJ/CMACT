using AutorizadorCanales.Domain.Repositorios;
using AutorizadorCanales.Excepciones;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AutorizadorCanales.Infrastructure.Persistencia.Repositorios;

public class RepositorioLectura : IRepositorioLectura
{
    private readonly ContextoLectura _contextoLectura;

    public RepositorioLectura(ContextoLectura contextoSeguridad)
    {
        this._contextoLectura = contextoSeguridad;
    }

    public async Task<T> ObtenerPorCodigoAsync<T>(params object[] llaves) where T : class
    {
        var entidad = await _contextoLectura.Establecer<T>().FindAsync(llaves);

        if (entidad == null)
            throw new EntidadNoExisteExcepcion(typeof(T), llaves);

        return entidad;
    }

    public async Task<T> ObtenerPrimeroPorAsync<T>(Expression<Func<T, bool>> expresionFiltro) where T : class
    {
        return await _contextoLectura.Establecer<T>().FirstOrDefaultAsync(expresionFiltro);
    }

    public async Task<List<T>> ObtenerPorExpresionConLimiteAsync<T>(Expression<Func<T, bool>> expresionFiltro = null, byte cantidadLimite = 0) where T : class
    {
        if (expresionFiltro == null) return await _contextoLectura.Establecer<T>().ToListAsync();
        return cantidadLimite == 0
            ? await _contextoLectura.Establecer<T>().Where(expresionFiltro).ToListAsync()
            : await _contextoLectura.Establecer<T>().Where(expresionFiltro).Take(cantidadLimite).ToListAsync();
    }

    public List<T> ObtenerPorExpresionConLimite<T>(Expression<Func<T, bool>> expresionFiltro = null, byte cantidadLimite = 0) where T : class
    {
        if (expresionFiltro == null) return _contextoLectura.Establecer<T>().ToList();
        return cantidadLimite == 0
            ? _contextoLectura.Establecer<T>().Where(expresionFiltro).ToList()
            : _contextoLectura.Establecer<T>().Where(expresionFiltro).Take(cantidadLimite).ToList();
    }

    public T ObtenerPorCodigo<T>(params object[] llaves) where T : class
    {
        var entidad = _contextoLectura.Establecer<T>().Find(llaves);

        if (entidad == null)
            throw new EntidadNoExisteExcepcion(typeof(T), llaves);

        return entidad;
    }

    public async Task<int> ObtenerNumeroSerieNoBloqueante(string codigoAgencia, string codigoSistema, string codigoSerie, int cantidadSeries)
    {
        SqlParameter resultado = new SqlParameter
        {
            ParameterName = "@decVALOR",
            Direction = System.Data.ParameterDirection.Output,
            SqlDbType = System.Data.SqlDbType.Decimal,
            Value = 0
        };
        await _contextoLectura.EjecutarComandoSQL(
            "CF.USP_CF_OBTENER_SIGUIENTE_SERIE @vchAGENCIA, @vchSISTEMA, @vchSERIE, @decINCREMENTO, @decVALOR OUTPUT"
            , new SqlParameter("@vchAGENCIA", codigoAgencia)
            , new SqlParameter("@vchSISTEMA", codigoSistema)
            , new SqlParameter("@vchSERIE", codigoSerie)
            , new SqlParameter("@decINCREMENTO", cantidadSeries)
            , resultado
            );
        return Convert.ToInt32(resultado.Value);
    }

}
