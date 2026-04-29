using Takana.Transferencias.CCE.Api.Datos.Contexto;
using Takana.Transferencias.CCE.Api.Common.Interfaz;

namespace Takana.Transferencias.CCE.Api.Datos.Repositorios
{
    /// <summary>
    /// Clase que se utiliza como repositorio de metodos para el contexto de general
    /// </summary>
    public class RepositorioEscritura : IRepositorioEscritura
    {
        protected readonly ContextoEscritura _contextoBitacora;
        public RepositorioEscritura(ContextoEscritura contextoBitacora)
        {
            _contextoBitacora = contextoBitacora;
        }

        /// <summary>
        /// Método que adiciona una entidad
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objeto"></param>
        public void Adicionar<T>(T objeto) where T : class
        {
            _contextoBitacora.Establecer<T>().AddAsync(objeto);
        }

        /// <summary>
        /// Método que guarda cambios en la BBDD
        /// </summary>
        public void GuardarCambios()
        {
            _contextoBitacora.GuardarCambios();
        }
    }
}