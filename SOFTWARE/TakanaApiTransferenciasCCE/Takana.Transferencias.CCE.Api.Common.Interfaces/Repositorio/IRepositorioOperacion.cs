namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    public interface IRepositorioOperacion : IRepositorioLectura, IRepositorioEscritura
    {        
        /// <summary>
        /// Adiciona un objeto en el contexto.
        /// </summary>
        /// <typeparam name="T">Tipo de entidad.</typeparam>
        /// <param name="objeto">Objeto a adicionar.</param>
        Task AdicionarAsync<T>(T objeto) where T : class;
        
        /// <summary>
        /// Guarda los cambios realizados en el contexto.
        /// </summary>
        Task GuardarCambiosAsync();

        /// <summary>
        /// Adiciona objetos por lotes
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="objetos">Objetos a adicionar</param>
        /// <returns></returns>
        Task AdicionarRangoAsync<T>(List<T> objetos) where T : class;
    }
}
