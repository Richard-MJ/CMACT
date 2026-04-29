using System.Linq.Expressions;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    public interface IRepositorioLectura
    {
        /// <summary>
        /// Obtiene una entidad por su identificador.
        /// </summary>
        /// <param name="llave">Identificador o identificadores.</param>
        /// <typeparam name="T">Tipo de entidad.</typeparam>
        /// <returns>Entidad.</returns>
        T ObtenerPorCodigo<T>(params object[] ao_llaves) where T : class;

        /// <summary>
        /// Obtiene una entidad por su identificador.
        /// </summary>
        /// <param name="llave">Identificador o identificadores.</param>
        /// <typeparam name="T">Tipo de entidad.</typeparam>
        /// <returns>Entidad.</returns>
        Task<T> ObtenerPorCodigoAsync<T>(params object[] ao_llaves) where T : class;
        
        /// <summary>
        /// Obtiene una lista de entidades de acuerdo al filtro aplicado.
        /// </summary>
        /// <param name="filtro">Filtro de consulta.</param>
        /// <param name="incluir">Elementos a incluir.</param>
        /// <param name="limite">Limite de registros de la consulta.</param>
        /// <typeparam name="T">Tipo de entidad.</typeparam>
        /// <returns>Registros de la entidad.</returns>
        IList<T> ObtenerPorExpresionConLimite<T>(Expression<Func<T, bool>> ao_filtro = null,
            string as_incluir = null, byte aby_limite = 0) where T : class;
        /// <summary>
        /// Obtiene todos los registros.
        /// </summary>
        /// <typeparam name="T">Tipo de entidad.</typeparam>
        /// <returns>Registros de la entidad.</returns>
        IQueryable<T> Listar<T>() where T : class;
        /// <summary>
        /// Método de obtener la fecha de cuenta efectivo
        /// </summary>
        /// <returns></returns>
        Calendario ObtenerCalendarioCuentaEfectivo();
        /// <summary>
        /// Obtiene una única entidad del tipo <typeparamref name="T"/> que cumpla con el filtro especificado,
        /// o devuelve <c>null</c> si no se encuentra ningún registro.
        /// </summary>
        /// <typeparam name="T">Tipo de la entidad a obtener.</typeparam>
        /// <param name="filtro">Expresión lambda que define la condición de búsqueda.</param>
        /// <param name="incluir">
        /// Lista opcional de propiedades de navegación a incluir en la consulta (separadas por comas).
        /// </param>
        /// <returns>
        /// Una instancia de <typeparamref name="T"/> si se encuentra un registro que cumpla el filtro;
        /// de lo contrario, <c>null</c>.
        /// </returns>
        T ObtenerUnoONulo<T>(Expression<Func<T, bool>> filtro,
           string incluir = null) where T : class;
    }
}
