using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion
{
    public interface IServicioAplicacionParametroGeneral
    {
        /// <summary>
        /// Obtiene un parametro con el codigo
        /// </summary>
        /// <param name="Codigo">Codigo del parametro a buscar</param>
        /// <returns>Retorna el valor del parametro</returns>
        string obtenerParametrosConfiguracion(string Codigo);
        /// <summary>
        /// Metodo que obtiene el tipo de documento de la CCE con el tipo documento de Takana
        /// </summary>
        /// <param name="codigoTipodocumento">Codigo documento takana</param>
        /// <returns>Tipo documento</returns>
        string ObtenertipoDocumentoCCE(string documento);
        /// <summary>
        /// Metodo que obtiene el tipo de documento de takana con el tipo documento de la CCE
        /// </summary>
        /// <param name="codigoTipodocumento"></param>
        /// <returns></returns>Retorna el tipo de documento CCE convertido a tipo Takana <summary>
        string ObtenertipoDocumentoTakana(string codigoTipodocumento);
        /// <summary>
        /// Obtiene el numero de seguimiento para todas las consultas
        /// </summary>
        /// <returns>Retorna el numero de seguimiento</returns>
        public string ObtenerNumeroSeguimiento(string codigo);
        ///<summary>
        /// Obtiene los datos del codigo de error
        /// </summary>
        /// <param name="codigoError">Error resultandte de la CCE</param>
        /// <returns>Codigo de respuesta</returns>
        public CodigoRespuesta ObtenerErrorLocal(string? codigoError);

        /// <summary>
        /// Metodo para convertir los datos entre Takana y CCE
        /// </summary>
        /// <param name="datos">Datos a convertir</param>
        /// <returns>Reotrna datos convertidos</returns>
        OrdenTransferenciaCanalDTO ConvertirTipoDocumentos(OrdenTransferenciaCanalDTO datos);
    }
}
