using System.Reflection;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Excepciones;

namespace Takana.Transferencias.CCE.Api.Controllers
{
    public class BaseController<T> : ControllerBase where T : class
    {
        /// <summary>
        /// Interfaz de la IBitacora
        /// </summary>
        protected readonly IBitacora<T> _bitacora;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bitacora">Interfaz en IBitacora</param>
        protected BaseController(IBitacora<T> bitacora)
        {
            _bitacora = bitacora;
        }

        /// <summary>
        /// Realiza una operacion presente en un servicio.
        /// </summary>        
        /// <param name="operacion">Operacion a realizar presente en el servicio.</param>        
        /// <returns>Respuesta de la operacion.</returns>
        protected async Task<ActionResult<TResult>> InvocarOperacionDesdeServicios<TResult>(Func<Task<TResult>> operacion)
        {
            try
            {
                var resultado = await operacion.Invoke();
                return Ok(resultado);
            }
            catch (EntidadNoExisteException ex)
            {
                return ProcesarExcepcionControlada(ex, ex.CodigoError, StatusCodes.Status400BadRequest);
            }
            catch (ValidacionException ex)
            {
                return ProcesarExcepcionControlada(ex, ex.CodigoError, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                var exInicial = ObtenerExcepcionInicial(ex);
                var metodo = ObtenerMetodoOrigen(ex);

                var datos = new DatosExcepcion
                {
                    Codigo = "03",
                    Mensaje = exInicial.Message
                };

                _bitacora.Error($"Excepción inesperada en: {metodo?.Name}.");
                _bitacora.Error($"{exInicial.GetType().FullName}|{exInicial.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, datos);
            }
        }

        /// <summary>
        /// Método que procesa la excepcion controlada
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="codigo"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        private ActionResult ProcesarExcepcionControlada(Exception ex, string codigo, int statusCode)
        {
            var metodo = ObtenerMetodoOrigen(ex);

            var datos = new DatosExcepcion
            {
                Codigo = codigo,
                Mensaje = ex.Message
            };

            _bitacora.Error("Método donde se genera la excepción: {nombreMetodoError}.", metodo?.Name);
            _bitacora.Error("Tipo de excepción {tipoExcepcion} con mensaje {mensaje}", ex.GetType().FullName, ex.Message);

            return StatusCode(statusCode, datos);
        }

        /// <summary>
        /// Método que obtiene la excepcion inicial
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static Exception ObtenerExcepcionInicial(Exception ex)
        {
            var actual = ex;
            var limite = 1000;

            while (limite-- > 0 && actual.InnerException != null)
                actual = actual.InnerException;

            return actual;
        }

        /// <summary>
        /// Método que obtiene el metodo Origen
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static MethodBase? ObtenerMetodoOrigen(Exception ex)
        {
            return new StackTrace(ex)
                .GetFrames()?
                .Select(frame => frame?.GetMethod())
                .FirstOrDefault(m => m?.Module.Assembly.Location.Contains(ConfigApi.CodigoBase) == true);
        }
    }
}