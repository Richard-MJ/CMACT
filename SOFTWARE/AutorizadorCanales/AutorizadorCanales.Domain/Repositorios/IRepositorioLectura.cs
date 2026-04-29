using System.Linq.Expressions;

namespace AutorizadorCanales.Domain.Repositorios;

public interface IRepositorioLectura
{
    /// <summary>
    /// Recuperar una lista de entidades.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad de la lista de entidades a devolver.</typeparam>
    /// <param name="expresionFiltro">Expresión que se evaluara para filtrar el resultado
    /// que se devolvera.</param>
    /// <param name="cantidadLimite">Limite de elementos que devolvera la lista.</param>
    /// <returns>Lista de entidades de un tipo indicado.</returns>
    List<T> ObtenerPorExpresionConLimite<T>(Expression<Func<T, bool>> expresionFiltro = null,
        byte cantidadLimite = 0) where T : class;

    /// <summary>
    /// Recuperar una lista de entidades.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad de la lista de entidades a devolver.</typeparam>
    /// <param name="expresionFiltro">Expresión que se evaluara para filtrar el resultado
    /// que se devolvera.</param>
    /// <param name="cantidadLimite">Limite de elementos que devolvera la lista.</param>
    /// <returns>Lista de entidades de un tipo indicado.</returns>
    Task<List<T>> ObtenerPorExpresionConLimiteAsync<T>(Expression<Func<T, bool>> expresionFiltro = null,
        byte cantidadLimite = 0) where T : class;

    /// <summary>
    /// Recupera la primera entidad por filtro
    /// </summary>
    /// <typeparam name="T">Tipo de la entidad</typeparam>
    /// <param name="expresionFiltro">Expresión que se evaluará para filtrar el resultado</param>
    /// <returns></returns>
    Task<T> ObtenerPrimeroPorAsync<T>(Expression<Func<T, bool>> expresionFiltro) where T : class;

    /// <summary>
    /// Devuelve una entidad.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad a devolver.</typeparam>
    /// <param name="llaves">Los datos de la clave primaria.</param>
    /// <returns>Entidad devuelta a partir de los datos de la BBDD.</returns>
    Task<T> ObtenerPorCodigoAsync<T>(params object[] llaves) where T : class;

    /// <summary>
    /// Devuelve una entidad.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad a devolver.</typeparam>
    /// <param name="llaves">Los datos de la clave primaria.</param>
    /// <returns>Entidad devuelta a partir de los datos de la BBDD.</returns>
    T ObtenerPorCodigo<T>(params object[] llaves) where T : class;

    /// <summary>
    /// Se obtiene el número de inicial de una secuencia de números de series de sistema.
    /// </summary>
    /// <param name="codigoAgencia">Código de agencia de la serie de sistema, "%" obtiene la serie por empresa de sistema.</param>
    /// <param name="codigoSistema">Código de sistema de la series de sistema</param>
    /// <param name="codigoSerie">Código de la serie de sistema.</param>
    /// <param name="cantidadSeries">Cantidad de número de la secuencia de números de serie.</param>
    /// <returns>Primero número de la serie de sistema.</returns>
    Task<int> ObtenerNumeroSerieNoBloqueante(string codigoAgencia, string codigoSistema, string codigoSerie, int cantidadSeries);
}
