using AutorizadorCanales.Domain.Entidades.TJ;
using System.Linq.Expressions;

namespace AutorizadorCanales.Domain.Repositorios;

public interface IRepositorioEscritura
{
    // <summary>
    /// Recuperar una lista de entidades.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad de la lista de entidades a devolver.</typeparam>
    /// <param name="expresionFiltro">Expresión que se evaluara para filtrar el resultado
    /// que se devolvera.</param>
    /// <param name="cantidadLimite">Limite de elementos que devolvera la lista.</param>
    /// <returns>Lista de entidades de un tipo indicado.</returns>
    Task<List<T>> ObtenerPorExpresionConLimiteAsync<T>(Expression<Func<T, bool>>? expresionFiltro = null,
        byte cantidadLimite = 0) where T : class;

    Task<T> ObtenerPrimeroPor<T>(Expression<Func<T, bool>> expresionFiltro) where T : class;

    /// <summary>
    /// Devuelve una entidad.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad a devolver.</typeparam>
    /// <param name="llaves">Los datos de la clave primaria.</param>
    /// <returns>Entidad devuelta a partir de los datos de la BBDD.</returns>
    Task<T> ObtenerPorCodigoAsync<T>(params object[] llaves) where T : class;

    /// <summary>
    /// Método que adiciona una Entidad de clase T al contexto de base de datos.
    /// </summary>
    /// <typeparam name="T">Clase del objeto que se desea adicionar</typeparam>
    /// <param name="aoObjeto">Instancia de la clase T que se adicionara</param>
    Task AdicionarAsync<T>(T aoObjeto) where T : class;

    /// Método que guarda los cambios realizados al contexto asincronamente.
    /// </summary>
    Task GuardarCambiosAsync();

    /// <summary>
    /// Método que retorna una tarjeta por su primary key
    /// </summary>
    /// <param name="llaves"></param>
    /// <returns></returns>
    Task<Tarjeta?> ObtenerTarjetaPorCodigoAsync(params object[] llaves);
}
