using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Common.Cancelaciones;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Extensiones
{    public static class CancelacionExtensiones
    {
        /// <summary>
        /// Extension que mapea la estructura de contenido de cancelacion
        /// </summary>
        /// <param name="datosRecibidos"></param>
        /// <param name="datosCalculados"></param>
        /// <returns>Retorna la estructura CTC2</returns>
        public static EstructuraContenidoCTC2 ArmarDatos(
            this CancelacionRecepcionDTO datosRecibidos,
            CancelacionRespuestaDTO datosCalculados
            ){

            return new EstructuraContenidoCTC2(){
                CTC2 = new CancelacionRespuestaDTO
                {
                    creditorParticipantCode = datosRecibidos.creditorParticipantCode,
                    responseDate = datosCalculados.responseDate,
                    responseTime = datosCalculados.responseTime,
                    currency = datosRecibidos.currency,
                    branchId = datosRecibidos.branchId,
                    instructionId = datosRecibidos.instructionId,                
                    responseCode = datosCalculados.responseCode,
                    reasonCode = datosCalculados.reasonCode?.EsVacioTexto()
                } 
            };    
        }
    }
}

