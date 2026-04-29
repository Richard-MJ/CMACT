
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad;

namespace Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion
{
    public interface IServicioAplicacionValidacion
    {
        /// <summary>
        /// Valida las reglas que la IPS plantea en su documentacion
        /// </summary>
        /// <param name="datos">Datos enviados por el canal de originen</param>
        /// <returns>Retorna True si pasa las validaciones</returns>
        void ValidarReglasIPS(ConsultaCanalDTO datos);
        /// <summary>
        /// Metodo de verifica el estado de las entidades originante y receptor
        /// </summary>
        /// <param name="originante">Codigo de entidad originante</param>
        /// <param name="receptora">Codigo de entidad receptora</param>
        /// <returns></returns>
        List<EntidadFinancieraInmediata> VerificarEstadoEntidades(string originante, string receptora);

        /// <summary>
        /// Metodo que valida servicio de interoperabilidad
        /// </summary>
        /// <param name="fechaSistema">fecha y hora del sistema</param>
        /// <param name="afiliacion">datos de afiliacion</param>
        /// <param name="datosEntrada">Datos de entrada</param>
        Task ValidarServicioInteroperabilidad(Calendario fechaSistema, AfiliacionInteroperabilidadDetalle afiliacion,
            EntradaBarridoDTO datosEntrada);
    }
}
