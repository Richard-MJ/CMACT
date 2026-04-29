using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Takana.Transferencias.CCE.Api.Datos.Contexto;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Repositorios
{
    /// <summary>
    /// Clase que se utiliza como repositorio de metodos para el contexto de operacion
    /// </summary>
    public class RepositorioOperacion : IRepositorioOperacion
    {
        protected readonly ContextoOperacion _contextoOperacion;
        public RepositorioOperacion(ContextoOperacion contextoOperacion)
        {
            _contextoOperacion = contextoOperacion;
        }
        /// <summary>
        /// Metodo para buscar el registro de una tabla con una llave
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="llaves"></param>
        /// <returns></returns>
        /// <exception cref="EntidadNoExisteException"></exception>
        public T ObtenerPorCodigo<T>(params object[] llaves) where T : class
        {
            var entidad = _contextoOperacion.Establecer<T>().FindAsync(llaves).Result;

            if (entidad == null) throw new EntidadNoExisteException(typeof(T), llaves);

            return entidad;
        }

        /// <summary>
        /// Metodo para buscar el registro de una tabla con una llave
        /// </summary>
        /// <typeparam name="T"> Clase</typeparam>
        /// <param name="llaves">Llavs para buscar</param>
        /// <returns>Resultado de busqueda</returns>
        public async Task<T> ObtenerPorCodigoAsync<T>(params object[] llaves) where T : class
        {
            var entidad = await _contextoOperacion.Establecer<T>().FindAsync(llaves);

            if (entidad == null) throw new EntidadNoExisteException(typeof(T), llaves);

            return entidad;
        }

        /// <summary>
        /// Metodo que busca un registro en una tabla con un filtro en especifico
        /// </summary>
        /// <typeparam name="T">Clase</typeparam>
        /// <param name="filtro">Filtro</param>
        /// <param name="incluir">Inlcuir</param>
        /// <param name="limite">Limite</param>
        /// <returns>Retorna resultado de busqueda con filtros</returns>
        public IList<T> ObtenerPorExpresionConLimite<T>(Expression<Func<T, bool>> filtro = null,
            string incluir = null,
            byte limite = 0) where T : class
        {
            try
            {
                if (filtro != null)
                {
                    if (limite == 0)
                        return _contextoOperacion.Establecer<T>().Where(filtro).ToListAsync().Result;
                    else
                        return _contextoOperacion.Establecer<T>().Where(filtro).Take(limite).ToListAsync().Result;
                }
                else
                {
                    return _contextoOperacion.Establecer<T>().ToListAsync().Result;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Se ha producido un error de 'Data is Null'. Detalles: " + ex.Message, ex);
            }
            catch (Exception excepcion)
            {
                throw new InvalidOperationException(excepcion.Message);
            }
        }
        
        /// Lista los registro d euna tabla
        /// </summary>
        /// <typeparam name="T">Clase</typeparam>
        /// <returns>Retorna la lista de los registros</returns>
        public IQueryable<T> Listar<T>() where T : class
        {
            return _contextoOperacion.Establecer<T>();
        }

        /// <summary>
        /// Método que adiciona una entidad
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="aoObjeto"></param>
        public void Adicionar<T>(T aoObjeto) where T : class
        {
            _contextoOperacion.Establecer<T>().AddAsync(aoObjeto);
        }

        /// <summary>
        /// Método que adiciona una entidad
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="aoObjeto"></param>
        public async Task AdicionarAsync<T>(T objeto) where T : class
        {
            await _contextoOperacion.Establecer<T>().AddAsync(objeto);
        }

        /// <summary>
        /// Método que guarda cambios en la BBDD
        /// </summary>
        public void GuardarCambios()
        {
            _contextoOperacion.GuardarCambios();
        }        
        
        /// <summary>
        /// Método que guarda cambios en la BBDD
        /// </summary>
        public Task GuardarCambiosAsync()
        {
            return _contextoOperacion.GuardarCambiosAsync();
        }

        /// <summary>
        /// Método de obtener la fecha de cuenta efectivo
        /// </summary>
        /// <returns>Retorna los datos de la fecha de cuenta efectivo</returns>
        public Calendario ObtenerCalendarioCuentaEfectivo()
        {
            return ObtenerPorCodigo<Calendario>(Empresa.CodigoPrincipal, Agencia.Principal, Sistema.CuentaEfectivo);
        }
        /// <summary>
        /// Método para obtener uno o nulo
        /// </summary>
        /// <returns>Entidad Consultada</returns>
        public T ObtenerUnoONulo<T>(Expression<Func<T, bool>> filtro,
           string incluir = null) where T : class
        {
            try
            {
                return _contextoOperacion.Establecer<T>().FirstOrDefault(filtro);
            }
            catch (Exception excepcion)
            {
                throw new InvalidOperationException(excepcion.Message);
            }
        }

        /// <summary>
        /// Adiciona objetos por lotes
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="objetos">Objetos a adicionar</param>
        /// <returns></returns>
        public Task AdicionarRangoAsync<T>(List<T> objetos) where T : class
        {
            return _contextoOperacion.Set<T>().AddRangeAsync(objetos);
        }
    }
}
