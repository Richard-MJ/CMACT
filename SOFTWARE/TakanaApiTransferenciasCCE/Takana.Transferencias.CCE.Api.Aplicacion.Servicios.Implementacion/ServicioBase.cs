using Takana.Transferencias.CCE.Api.Common.Interfaz;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion
{
    /// <summary>
    /// Clase base de servicios
    /// </summary>
    public class ServicioBase : IServicioBase
    {
        /// <summary>
        /// Contexto Api
        /// </summary>
        public IContextoAplicacion _contextoAplicacion { get; }

        /// <summary>
        /// Método constructor
        /// </summary>
        /// <param name="contexto"></param>
        public ServicioBase(IContextoAplicacion contexto)
        {
            _contextoAplicacion = contexto;
        }
    }
}
