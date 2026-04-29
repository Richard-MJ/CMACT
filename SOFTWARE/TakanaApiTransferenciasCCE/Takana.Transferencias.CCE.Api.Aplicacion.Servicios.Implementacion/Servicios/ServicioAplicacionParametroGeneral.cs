using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios
{
    /// <summary>
    /// Clase Padre que representa los datos generales del proceso de Consulta Cuenta.
    /// Son datos redundantes de todos los tramos.
    /// </summary>
    public class ServicioAplicacionParametroGeneral : IServicioAplicacionParametroGeneral
    {   
        private readonly IRepositorioGeneral _repositorioGeneral;
        public ServicioAplicacionParametroGeneral(
            IRepositorioGeneral repositorioConsultaGeneral)
        {
            _repositorioGeneral = repositorioConsultaGeneral;
        }  
        
        /// <summary>
        /// Obtiene un parametro con el codigo
        /// </summary>
        /// <param name="codigo">Codigo del parametro a buscar</param>
        /// <returns>Retorna el valor del parametro</returns>
        public string obtenerParametrosConfiguracion(string codigo)
        {
            var parametro = _repositorioGeneral.ObtenerPorExpresionConLimite<ParametroGeneralTransferencia>()
            .FirstOrDefault(x => x.CodigoParametro == codigo);
            if (parametro == null)
                throw new ValidacionException("No se pudo obtener parametro: " + codigo);

            return parametro.ValorParametro;
        }

        /// <summary>
        /// Metodo que obtiene el tipo de documento de la CCE con el tipo documento de Takana
        /// </summary>
        /// <param name="codigoTipodocumento">Codigo documento takana</param>
        /// <returns>Tipo documento</returns>
        public string ObtenertipoDocumentoCCE(string codigoTipodocumento)
        {
            try
            {
                var documento = _repositorioGeneral
                    .ObtenerPorExpresionConLimite<TipoDocumento>(x => x
                    .CodigoTipoDocumento == codigoTipodocumento)
                    .FirstOrDefault();
                if(documento == null)
                    throw new Exception("No se pudo obtener el tipo de  documento de la CCE");
                return documento.CodigoTipoDocumentoInmediataCce!;
            }
            catch (System.Exception){
                throw new Exception("Error al obtener el tipo de  documento de la CCE");
            }
        }

        /// <summary>
        /// Metodo que obtiene el tipo de documento de takana con el tipo documento de la CCE
        /// </summary>
        /// <param name="codigoTipodocumento"></param>
        /// <returns></returns>Retorna el tipo de documento CCE convertido a tipo Takana <summary>
        public string ObtenertipoDocumentoTakana(string codigoTipodocumento)
        {
            try{
                return _repositorioGeneral
                    .ObtenerPorExpresionConLimite<TipoDocumento>(x => x
                    .CodigoTipoDocumentoInmediataCce == codigoTipodocumento)
                    .FirstOrDefault()!.CodigoTipoDocumento;
            }
            catch (System.Exception){
                return codigoTipodocumento;
            }
        }

        /// <summary>
        /// Obtiene el numero de seguimiento para todas las consultas
        /// </summary>
        /// <returns>Retorna el numero de seguimiento</returns>
        public string ObtenerNumeroSeguimiento(string codigo)
        {
            try
            {
                int cantidadSeries = 1;
                int numeroSeguimiento = _repositorioGeneral.ObtenerNumeroSerieNoBloqueante("%",
                    Sistema.CuentaEfectivo, codigo, cantidadSeries);

                if (numeroSeguimiento.ToString().Length > 6)
                    throw new Exception("La operacion no puede continuar porque el numero de seguimiento(trace) supera los 6 digitos,informarlo a TI");

                return RellenarCadena(numeroSeguimiento.ToString(), '0', 6, true);
            }
            catch (Exception)
            {
                throw new Exception("No se pudo obtener un numero de seguimiento(Trace) para la operacion");
            }
        }

        /// <summary>
        /// Rellena el numero de seguimiento con 0 a la izquierda, tienen que ser 6 digitos
        /// </summary>
        /// <param name="cadenaARellenar">Cadena a rellenar</param>
        /// <param name="caracterDeRelleno">Cararcter con el cual rellenar</param>
        /// <param name="longitud">Cantidad de digitos de longitud</param>
        /// <param name="izquierda">Que lado rellenar</param>
        /// <returns></returns>
        public string RellenarCadena(string cadenaARellenar, char caracterDeRelleno, int longitud, bool izquierda)
        {
            if (izquierda)
                return cadenaARellenar.PadLeft(longitud, caracterDeRelleno);
            else
                return cadenaARellenar.PadRight(longitud, caracterDeRelleno);
        }

        ///<summary>
        /// Obtiene los datos del codigo de error
        /// </summary>
        /// <param name="codigoError">Error resultandte de la CCE</param>
        /// <returns>Codigo de respuesta</returns>
        public CodigoRespuesta ObtenerErrorLocal(string codigoError)
        {
            try
            {
                return _repositorioGeneral.ObtenerPorCodigo<CodigoRespuesta>(codigoError);
            }
            catch (Exception)
            {
                throw new Exception("No se pudo identificar el error: " + codigoError);
            }
            
        }

        /// <summary>
        /// Metodo para convertir los datos entre Takana y CCE
        /// </summary>
        /// <param name="datos">Datos a convertir</param>
        /// <returns>Reotrna datos convertidos</returns>
        public OrdenTransferenciaCanalDTO ConvertirTipoDocumentos(OrdenTransferenciaCanalDTO datos)
        {
            datos.TipoDocumentoReceptorCCE = datos.TipoDocumentoReceptor;

            datos.TipoDocumentoReceptor = datos.TipoDocumentoReceptor != General.Cero
                    ? ObtenertipoDocumentoTakana(datos.TipoDocumentoReceptor)
                    : ((int)TipoDocumentoEnum.OtroDocumento).ToString();

            datos.TipoDocumentoDeudorCCE = datos.TipoDocumentoDeudor;
            datos.TipoDocumentoDeudor = datos.TipoDocumentoDeudor != General.Cero
                    ? ObtenertipoDocumentoTakana(datos.TipoDocumentoDeudor)
                    : ((int)TipoDocumentoEnum.OtroDocumento).ToString();

            return datos;
        }
    }
}


