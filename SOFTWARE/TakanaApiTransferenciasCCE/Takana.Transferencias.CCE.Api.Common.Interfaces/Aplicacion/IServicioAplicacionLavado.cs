using Takana.Transferencias.CCE.Api.Common.DTOs.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    public interface IServicioAplicacionLavado
    {
        /// <summary>
        /// Método que genera y realiza el lavado de la transferencia Inmediata Entrante CCE
        /// </summary>
        /// <param name="transaccion">Transaccion de transferencia inmediata</param>
        /// <param name="transferencia">Transferencias, operacion</param>
        /// <param name="clienteBeneficiario">Cliente beneficiado</param>
        /// <param name="asientoContable">Asiento Contable</param>
        /// <param name="entidadOrdenanteCCE">Entidad ordenante</param>
        /// <param name="fechaSistema">Fecha y hora del sistema</param>
        /// <returns>Retorna Operacion Unica o Menor Cuantia de Entrante</returns>
        IRegistroLavado GenerarLavadoTransferenciaEntrante(
            SubTipoTransaccion subTipoTransaccion,
            Transferencia transferencia,
            ClienteExternoDTO clienteOriginante,
            Cliente clienteBeneficiario,
            AsientoContable asientoContable,
            EntidadFinancieraInmediata entidadOrdenanteCCE,
            DateTime fechaSistema);

        /// <summary>
        /// Completa el lavado
        /// </summary>
        /// <param name="idOperacion">Numero que identifica a la transaccion</param>
        /// <param name="movimientoPrincipalTransferencia">Movimiento principal de la transferencia</param>
        /// <param name="detalleTransferenciaCCE">Detalle de la transferencia</param>
        /// <param name="numeroOperacionLavado">Numero de lavado</param>
        /// <returns></returns>
        void CompletarLavadoTransferenciaInterbancario(
            short idOperacion,
            MovimientoDiario movimientoPrincipalTransferencia,
            TransferenciaDetalleSalienteCCE detalleTransferenciaCCE,
            decimal numeroOperacionLavado,
            string subCanal);
        /// <summary>
        /// Anula el lavado de mayor o menor cuantia 
        /// </summary>
        /// <param name="operacionPrincipal">Movimiento principal</param>
        void AnularLavado(MovimientoDiario operacionPrincipal);

        /// <summary>
        /// Obtiene el monto de umbral del lavado
        /// </summary>
        /// <param name="codigoAgencia">Codigo de la agencia</param>
        /// <param name="fecha">Fecha del sistema</param>
        /// <param name="monedaOperacion">Moneda de la cuenta efectivo</param>
        /// <returns>Retorna el monto umbral</returns>
        decimal ObtenerMontoUmbralLavado(string codigoAgencia, DateTime fecha, Moneda monedaOperacion);

        /// <summary>
        /// Obtiene el numero de lavado para la operacion
        /// </summary>
        /// <param name="montoOperacion">Monto de la operacion</param>
        /// <param name="montoUmbralLavado">Umbral de la operacion</param>
        /// <returns>Retorna el numero de lavado</returns>
        int ObtenerNumeroLavado(decimal montoOperacion, decimal montoUmbralLavado);

        /// <summary>
        /// Registra el numero de lavado segun el tipo de lavado 
        /// </summary>
        /// <param name="codigoMoneda">Codigo de moneda</param>
        /// <param name="fechaSistema">Fecha de la operacion</param>
        /// <param name="montoOperacion">Monto de la operacion</param>
        /// <param name="tipoTransferencia">Tipo de transferencia</param>
        /// <param name="codigoCCI">Codigo de Cuenta interbancaria</param>
        int RegistrarNumeroLavado(string codigoMoneda, DateTime fechaSistema, decimal montoOperacion,
            string tipoTransferencia, string codigoCCI, string subCanal, string numeroCuentaOriginante,
            ClienteExternoDTO clienteExternoDTO);
    }
}
