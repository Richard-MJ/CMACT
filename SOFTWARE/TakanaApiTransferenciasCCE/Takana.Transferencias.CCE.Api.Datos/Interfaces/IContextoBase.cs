using Microsoft.EntityFrameworkCore;

namespace Takana.Transferencias.CCE.Api.Datos.Interfaces
{
    /// <summary>
    /// Interfaz base de los contextos
    /// </summary>
    public interface IContextoBase
    {
        /// <summary>
        /// Obtiene el conjunto de datos de una entidad.
        /// </summary>
        /// <typeparam name="T">Tipo de conjunto.</typeparam>
        /// <returns>Conjunto de datos de una entidad</returns>
        DbSet<T> Establecer<T>() where T : class;

        /// <summary>
        /// Almacena los datos en el contexto.
        /// </summary>
        void GuardarCambios();
    }
}