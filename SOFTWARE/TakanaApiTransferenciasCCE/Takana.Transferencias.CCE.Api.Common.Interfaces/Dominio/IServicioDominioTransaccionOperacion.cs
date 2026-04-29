using Takana.Transferencias.CCE.Api.Common.DTOs;
using Takana.Transferencias.CCE.Api.Common.DTOs.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;

namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    /// <summary>
    /// Interfaz que maqueta las tramas de respuesta para la CCE
    /// </summary>
    public interface IServicioDominioTransaccionOperacion : IServicioBase
    {
        #region Metodos entrantes
        /// <summary>
        /// Método que genera el detalle de la transferencia CCE
        /// </summary>
        /// <param name="transferencia"></param>
        /// <param name="EntidadOrdenanteCCE"></param>
        /// <param name="transaccion"></param>
        /// <param name="clienteOrigen"></param>
        /// <returns>Retorna detalle de la transferencia entrante de la CCE</returns>
        TransferenciaDetalleEntranteCCE GenerarDetalleTransferencia(
            TransaccionOrdenTransferenciaInmediata transaccion,
            Transferencia transferencia,
            ClienteExternoDTO clienteOrigen,
            EntidadFinancieraInmediata EntidadOrdenanteCCE);
        #endregion

        #region Metodos Salientes
        /// <summary>
        /// Genera la cabecera y detalle de la transferencia
        /// </summary>
        /// <param name="movimientoPrincipalTransferencia">Movimiento principal de la transferencia</param>
        /// <param name="detalleTransferencia">Datos de la transferencia</param>
        /// <param name="fechaSistema">Fecha del sistema</param>
        /// <param name="usuario">Usuario del sistema</param>
        /// <param name="entidadDestino">Entidad Destino</param>
        /// <returns>Transferencia</returns>
        Transferencia GenerarTransferenciaInmediata(
            MovimientoDiario movimientoPrincipalTransferencia,
            RealizarTransferenciaInmediataDTO detalleTransferencia,
            DateTime fechaSistema,
            Usuario usuario,
            EntidadFinancieraDiferida entidadDestino,
            int numeroTransferencia);

        /// <summary>
        /// Metodo que genera la comisión exonerada
        /// </summary>
        /// <param name="esMismaPlaza"></param>
        /// <param name="movimiento"></param>
        /// <param name="comisionAhorros"></param>
        /// <returns></returns>
        /// <exception cref="ValidacionException"></exception>
        ComisionExonerada? GenerarComisionExonerada(
            bool esMismaPlaza,
            MovimientoDiario movimiento,
            ComisionAhorrosAuxiliar comisionAhorros);

        /// <summary>
        /// Método que aplica la comisin de CCE
        /// </summary>
        /// <param name="movimientoPrincipalTransferencia"></param>
        /// <param name="usuario"></param>
        /// <param name="comisionAhorros"></param>
        /// <param name="numeroMovimiento"></param>
        MovimientoDiario AplicarComisionTransferencia(
            MovimientoDiario movimientoPrincipalTransferencia,
            Usuario usuario,
            ComisionAhorrosAuxiliar comisionAhorros,
            int numeroMovimiento,
            bool indicadorCuentaSueldo);
        /// <summary>
        /// Método que aplica la Exoneracion de comisión segun la configuracion
        /// </summary>
        /// <param name="fechaProceso"></param>
        /// <param name="cuentaEfectivo"></param>
        /// <param name="configuracionComision"></param>
        /// <returns></returns>
        bool VerificarExoneracionSegunConfiguracion(
            DateTime fechaProceso,
            CuentaEfectivo cuentaEfectivo,
            ConfiguracionComision configuracionComision);
        /// <summary>
        /// Realiza el ingreso de los motivos/vinculos de la transferencia CCE.
        /// </summary>
        /// <param name="motivoVinculo">vinculo y motivo</param>
        /// <param name="movimiento">Movimiento</param>
        /// <param name="codigoTipoTransferencia">Codigo el tipo de transferencia</param>
        OperacionesVinculosMotivos AgregarVinculoMotivo(
            IngresoVinculoMotivoDTO motivoVinculo,
            MovimientoDiario movimiento,
            string codigoTipoTransferencia);
        /// <summary>
        /// Actualiza estado de la transaccion anteriormente registrada
        /// </summary>
        /// <param name="transaccion">Datos de la transaccion</param>
        /// <param name="orden">Datos de respuesta de la CCE</param>
        /// <param name="fechaSistema">Datos de respuesta de la CCE</param>
        void ActualizarEstadoTransaccion(
            TransaccionOrdenTransferenciaInmediata transaccion,
            TramaProcesada tramaProcesada,
            OrdenTransferenciaRecepcionSalidaDTO orden, 
            DateTime fechaSistema);
        #endregion Metodos Salientes
    }
}
