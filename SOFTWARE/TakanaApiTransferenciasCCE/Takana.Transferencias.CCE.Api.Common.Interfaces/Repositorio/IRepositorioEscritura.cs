namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    public interface IRepositorioEscritura
    {
        /// <summary>
        /// Adiciona un objeto en el contexto.
        /// </summary>
        /// <typeparam name="T">Tipo de entidad.</typeparam>
        /// <param name="objeto">Objeto a adicionar.</param>
        void Adicionar<T>(T objeto) where T : class;
        /// <summary>
        /// Guarda los cambios realizados en el contexto.
        /// </summary>
        void GuardarCambios();
    }
}
