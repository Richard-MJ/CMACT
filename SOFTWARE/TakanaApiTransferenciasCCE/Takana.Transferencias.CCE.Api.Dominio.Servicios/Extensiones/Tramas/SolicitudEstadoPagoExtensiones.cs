using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Common.SolicitudEstadoPago;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Extensiones
{
    public static class SolicitudEstadoPagoExtensiones
    {
        /// <summary>
        /// Extesión que mapea la estrucutra de contenido de solicitud estado de pago
        /// </summary>
        /// <param name="datosRecibidos"></param>
        /// <returns>Retorna la estructura PSR2</returns>
        public static EstructuraContenidoPSR1 ArmarDatos(
            this TransaccionOrdenTransferenciaInmediata datosRecibidos,
            DateTime fechaSistema
        )
        {
            return new EstructuraContenidoPSR1()
            {
                PSR1 = new SolicitudEstadoPagoSalidaDTO
                {            
                    creditorParticipantCode = datosRecibidos.EntidadReceptor,
                    currency = datosRecibidos.CodigoMoneda == DatosGenerales.CodigoMonedaSoles 
                        ? DatosGenerales.CodigoMonedaSolesCCE : DatosGenerales.CodigoMonedaDolaresCCE,
                    instructionId = datosRecibidos.IdentificadorInstruccion,
                    creationDate = fechaSistema.ToString("yyyyMMdd"),
                    creationTime = fechaSistema.ToString("hhmmss"),
                    originalCreationDate = datosRecibidos.FechaOperacion.ToString("yyyyMMdd"),
                    originalCreationTime = datosRecibidos.FechaOperacion.ToString("hhmmss"),
                }
            };       
        }
    }
}

