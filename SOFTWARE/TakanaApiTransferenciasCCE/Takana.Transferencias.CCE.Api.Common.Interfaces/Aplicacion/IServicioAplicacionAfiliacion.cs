using Takana.Transferencias.CCE.Api.Common.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad.DatosRegistroDirectorio;

namespace Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion
{
    public interface IServicioAplicacionAfiliacion
    {
        /// <summary>
        /// Metodo que afilia servicios
        /// </summary>
        /// <param name="numeroTarjeta">numero tarjeta</param>
        /// <param name="numeroCuenta">numeor de cuenta</param>
        /// <param name="codigoServicio">codigo del servicio</param>
        /// <returns>DTO de la afiliacion</returns>
        Task<RespuestaAfiliacionCCEDTO> AfiliacionDirectorioCCE(EntradaAfiliacionDirectorioDTO datosAfiliacion);
        /// <summary>
        /// Metodo que desafilia de un servicio
        /// </summary>
        /// <param name="numeroTarjeta">numero de tarjeta</param>
        /// <param name="parametroEmpresa">parametro de la empresa</param>
        /// <returns>Retorna DTO de la desafiliacion</returns>
        Task<RespuestaAfiliacionCCEDTO> DesafiliacionDirectorioCCE(EntradaAfiliacionDirectorioDTO datosAfiliacion);

        /// <summary>
        /// Registrar datos procesado en bitacora de afiliación
        /// </summary>
        /// <param name="contenido"></param>
        /// <returns></returns>
        Task RegistrarDatosProcesadosBitacoraAfiliacion(byte[] contenido);
    }
}
