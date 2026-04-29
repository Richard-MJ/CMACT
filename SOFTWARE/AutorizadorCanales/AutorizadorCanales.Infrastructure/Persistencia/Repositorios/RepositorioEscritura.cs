using AutoMapper.Execution;
using AutorizadorCanales.Domain.Entidades.TJ;
using AutorizadorCanales.Domain.Repositorios;
using AutorizadorCanales.Excepciones;
using AutorizadorCanales.Infrastructure.Logging.Nlog;
using AutorizadorCanales.Logging.Interfaz;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog.LayoutRenderers.Wrappers;
using System.Linq.Expressions;

namespace AutorizadorCanales.Infrastructure.Persistencia.Repositorios;

public class RepositorioEscritura : IRepositorioEscritura
{
    private readonly ContextoEscritura _contextoEscritura;
    private readonly IBitacora<RepositorioEscritura> _bitacora;

    public RepositorioEscritura(ContextoEscritura contextoEscritura, IBitacora<RepositorioEscritura> bitacora)
    {
        _contextoEscritura = contextoEscritura;
        _bitacora = bitacora;
    }

    public async Task AdicionarAsync<T>(T entidad) where T : class
    {
        try
        {
            await _contextoEscritura.Establecer<T>().AddAsync(entidad);
        }
        catch (Exception le_excepcion)
        {
            throw new InvalidOperationException(le_excepcion.Message);
        }
    }

    public async Task GuardarCambiosAsync()
    {
        try
        {
            await _contextoEscritura.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _bitacora.Error("Error Guardar Cambios " + ex);
            throw new ExcepcionAUsuario("06", "La operación no se procesó. Por favor revisar");
        }
        catch (Exception ex)
        {
            _bitacora.Fatal("Error " + ex);
            throw new ExcepcionAUsuario("06", "La operación no se procesó. Por favor revisar");
        }
    }

    public async Task<T> ObtenerPorCodigoAsync<T>(params object[] llaves) where T : class
    {
        var entidad = await _contextoEscritura.Establecer<T>().FindAsync(llaves);

        if (entidad == null)
            throw new EntidadNoExisteExcepcion(typeof(T), llaves);

        return entidad;
    }

    public async Task<List<T>> ObtenerPorExpresionConLimiteAsync<T>(Expression<Func<T, bool>>? expresionFiltro = null, byte cantidadLimite = 0) where T : class
    {
        if (expresionFiltro == null) return await _contextoEscritura.Establecer<T>().ToListAsync();
        return cantidadLimite == 0
            ? await _contextoEscritura.Establecer<T>().Where(expresionFiltro).ToListAsync()
            : await _contextoEscritura.Establecer<T>().Where(expresionFiltro).Take(cantidadLimite).ToListAsync();
    }

    public async Task<T> ObtenerPrimeroPor<T>(Expression<Func<T, bool>> expresionFiltro) where T : class
    {
        return await _contextoEscritura.Establecer<T>().FirstOrDefaultAsync(expresionFiltro);
    }

    public async Task<Tarjeta?> ObtenerTarjetaPorCodigoAsync(params object[] llaves)
    {
        return await _contextoEscritura.Tarjeta.FindAsync(llaves);
    }
}
