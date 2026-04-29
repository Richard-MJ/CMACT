using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Common.DTOs.SignOnOff;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;
using Takana.Transferencias.CCE.Api.Common.SignOnOff;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;

namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    public interface IServicioAplicacionTransferenciaSalida : IServicioBase
    {
        /// <summary>
        /// Metodo encargado de comunicarse con la Operadora para obtener datos de la cuenta receptora
        /// </summary>
        /// <param name="datosConsultaCanal">Los datos enviados por el canal</param>
        /// <param name="consultaPorQr">Si la consulta es por QRl</param>
        /// <returns>Retorna la cuenta del cliente Receptor</returns>
        Task<RespuestaSalidaDTO<ConsultaCuentaRespuestaTraducidoDTO>> ObtenerDatosCuentaCCE(
            ConsultaCanalDTO datosConsultaCanal, Calendario fechaSistema, bool consultaPorQr);
        /// <summary>
        /// Metodo encargado de recibir la confirmacion del cliente originante para enviar la transferencia 
        /// Envia la orden de transferencia, espera que la entidad receptora confirme para luego realizar la operacion en esta entidad.
        /// </summary>
        /// <param name="ordenTransferenciaCanal">Los datos enviados por el canal para enviar la transferencia</param>
        /// <param name="fechaSistema">fecha Sistema</param>
        /// <returns>Retorna la confirmacion de la orden de transferencia enviada</returns>
        Task<RespuestaSalidaDTO<OrdenTransferenciaRespuestaTraducidoDTO>> EnviarOrdenTransferencia(
            OrdenTransferenciaCanalDTO ordenTransferenciaCanal, Calendario fechaSistema, AsientoContable asiento,
            Transferencia transferencia);
        /// <summary>
        /// Metodo que enviar el estado de ON en la operadora
        /// </summary>
        /// <returns>Retorna la confirmacion de cambio de estado ON</returns>
        Task<RespuestaSalidaDTO<SignOnOffDTO>> SignOn();
        /// <summary>
        /// Metodo que enviar el estado de OFF en la operadora
        /// </summary>
        /// <returns>Retorna la confirmacion de cambio de estado OFF</returns>
        Task<RespuestaSalidaDTO<SignOnOffDTO>> SignOff();
        /// <summary>
        /// Verifica conexion entre la CCE y el API Transferencias
        /// Tambien verifica la conexion antes de una operacion de Transferencia de takana
        /// </summary>
        /// <returns>Conexion</returns>
        Task<RespuestaSalidaDTO<bool>> EchoTest();
        /// <summary>
        /// Metodo que programa el SingOn/Off
        /// </summary>
        /// <returns>Retorna la confirmacion de cambio de estado OFF</returns>
        Task<EntidadFinancieroInmediataPeriodo> GestionarProgamacionSignOnOff(SingOnOffProgramadoDTO singProgamado);
        /// <summary>
        /// Metodo que Obtiene las tareas programadas.
        /// </summary>
        /// <returns>Retorna la confirmacion de cambio de estado OFF</returns>
        Task<IList<SingOnOffProgramadoDTO>> ObtenerProgamacionSignOnOff();
        /// <summary>
        /// Metodo que Actualiza el estado Sing On/Off.
        /// </summary>
        /// <returns>Retorna la entidad de Entidad Financiera</returns>
        Task<EntidadFinancieroInmediataPeriodo> ActualizarEstadoPeriodoSignCmact(string estado, long numeroPeriodo);
    }
}
