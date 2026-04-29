using Takana.Transferencias.CCE.Api.Common.DTOs.CL;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CJ;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios
{
    /// <summary>
    /// Servicio de maquetacion de las Entradas
    /// </summary>
    public class ServicioDominioLavado : IServicioDominioLavado
    {
        #region Métodos de transferencias Entrantes
        /// <summary>
        /// Metodo que registra el Lavado de operacion unica de las Transferencia Interbancaria Inmediata Entrante
        /// </summary>
        /// <param name="transferencia"></param>
        /// <param name="asiento"></param>
        /// <param name="clienteBeneficiario"></param>
        /// <param name="clienteOriginante"></param>
        /// <param name="banco"></param>
        /// <returns>Retorna el Registro del lavado de Operacion Unica</returns>
        public IRegistroLavado RegistrarLavadoOperacionUnicaTransferenciaEntrante(
            SubTipoTransaccion subTipoTransaccion,
            Transferencia transferencia,
            AsientoContable asiento,
            Cliente clienteBeneficiario,
            ClienteExternoDTO clienteOriginante,
            DateTime fechaSistema,
            EntidadFinancieraInmediata banco,
            int numeroOperacionUnica)
        {
            var operacionUnica = OperacionUnicaLavado.Crear(transferencia, asiento,
                clienteOriginante, fechaSistema, banco, numeroOperacionUnica, 
                subTipoTransaccion.DescripcionSubTransaccion, subTipoTransaccion.CodigoTipoTransaccion,
                subTipoTransaccion.CodigoSubTipoTransaccion);
            if (clienteBeneficiario.EsClienteNatural)
            {
                var detalleOperacionBeneficiarioNatural = OperacionUnicaDetalle.GenerarDesdePersonaFisica(
                    numeroOperacionUnica, clienteBeneficiario.PersonaFisica, transferencia.CodigoUsuario,
                    TipoInteviniente.Beneficiario, fechaSistema);
                operacionUnica.AdicionarInterviniente(detalleOperacionBeneficiarioNatural);
            }
            else
            {
                var detalleOperaionBeneficiarioJuridico = OperacionUnicaDetalle.GenerarDesdePersonaJuridica(
                    numeroOperacionUnica, clienteBeneficiario.PersonaJuridica, transferencia.CodigoUsuario,
                    TipoInteviniente.Beneficiario, fechaSistema);
                operacionUnica.AdicionarInterviniente(detalleOperaionBeneficiarioJuridico);
            }
            var detalleOperacionUnica = OperacionUnicaDetalle.GenerarIntervinienteConClienteExterno(
                numeroOperacionUnica, clienteOriginante, (int)TipoInteviniente.Ordenante, fechaSistema);
            operacionUnica.AdicionarInterviniente(detalleOperacionUnica);

            return operacionUnica;
        }

        /// <summary>
        /// Metodo que registra el Lavado de menor cuantia de las Transferencia Interbancaria Inmediata Entrante
        /// </summary>
        /// <param name="transferencia"></param>
        /// <param name="asiento"></param>
        /// <param name="clienteBeneficiario"></param>
        /// <param name="clienteOriginante"></param>
        /// <param name="banco"></param>
        /// <returns>Retorna el Registro del lavado de Menor Cuantia</returns>
        public IRegistroLavado RegistrarLavadoMenorCuantiaTransferenciaEntrante(
            SubTipoTransaccion subTipoTransaccion,
            Transferencia transferencia,
            AsientoContable asiento,
            Cliente clienteBeneficiario,
            ClienteExternoDTO clienteOriginante,
            EntidadFinancieraInmediata banco,
            int numeroMenorCuantia)
        {
            var menorCuantia = MenorCuantiaEncabezado.Crear(numeroMenorCuantia, transferencia.CodigoAgencia
                    , General.ModalidadOtrosMediosNoPresenciales, General.SubModalidadCajeroCCE
                    , transferencia.FechaTransferencia, General.Activo, transferencia.CodigoUsuario);
            var detalleCuantiaDetalle = MenorCuantiaDetalle.Crear(numeroMenorCuantia, Sistema.CuentaEfectivo,
                    transferencia.NumeroMovimiento, transferencia.MontoTransferencia,
                    subTipoTransaccion.CodigoTipoTransaccion, subTipoTransaccion.CodigoSubTipoTransaccion, 
                    Movimiento.Ingreso, transferencia.FechaTransferencia, transferencia.NumeroCuenta, 
                    transferencia.CodigoMoneda, General.FormaPagoCuentaCorriente, 0, asiento.NumeroAsiento, 0,
                    clienteOriginante.CodigoCuentaInterbancaria)
                .EstablecerCodigoEntidadSBS(banco.CodigoEntidadSBS);
            var detalleCuantiaBeneficiario = MenorCuantiaInterviniente.Crear(numeroMenorCuantia,
                clienteBeneficiario, (int)TipoInteviniente.Beneficiario);
            var detalleCuantiaOrdenante = MenorCuantiaInterviniente.RegistrarDesdeClienteExterno(
                menorCuantia, TipoInteviniente.Ordenante, clienteOriginante);

            menorCuantia.AdicionarDetalle(detalleCuantiaDetalle);
            menorCuantia.AdicionarInterviniente(detalleCuantiaBeneficiario);
            menorCuantia.AdicionarInterviniente(detalleCuantiaOrdenante);

            return menorCuantia;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Completa la operación de lavado de transferencia Interbancaria Inmediata Saliente
        /// </summary>
        /// <param name="registroLavado"></param>
        /// <param name="operacionesLavado"></param>
        /// <param name="tiposTransaccionesOrigen"></param>
        /// <exception cref="Exception"></exception>
        public void CompletarLavado(IRegistroLavado registroLavado, IList<IOperacionLavado> operacionesLavado,
           IList<TipoOperacionCanalOrigen> tiposTransaccionesOrigen)
        {
            List<IOperacionLavado> movimientosOrigen = new List<IOperacionLavado>();
            IOperacionLavado movimientoDestino;

            if (!tiposTransaccionesOrigen.Any())
            {
                throw new Exception("No se definieron subtransaccines origen para este tipo de operación.");
            }

            if (operacionesLavado.Count == 1)
            {
                movimientoDestino = operacionesLavado.FirstOrDefault(p => !p.EsOperacionPrincipalLavado);
                if (movimientoDestino == null)
                {
                    throw new Exception("No se puede determinar el movimiento destino en el proceso de lavado.");
                }
                movimientosOrigen.Add(movimientoDestino);
            }
            else
            {
                foreach (var transaccionOrigen in tiposTransaccionesOrigen)
                {
                    movimientosOrigen.AddRange(operacionesLavado.Where(
                        t => t.SubTipoTransaccionMovimiento.CodigoSistema == transaccionOrigen.CodigoSistema
                        && t.SubTipoTransaccionMovimiento.CodigoTipoTransaccion == transaccionOrigen.CodigoTipoTransaccion
                        && t.SubTipoTransaccionMovimiento.CodigoSubTipoTransaccion == transaccionOrigen.CodigoSubTipoTransaccion));
                }

                var subTransaccionesOrigen = movimientosOrigen.GroupBy(t => t.SubTipoTransaccionMovimiento).Select(t => t.Key);

                if (subTransaccionesOrigen.Count() > 1)
                {
                    throw new Exception("Los movimientos origen para el lavado tienen diferentes subtransacciones.");
                }

                movimientoDestino = operacionesLavado.FirstOrDefault(p => p != movimientosOrigen);
                if (movimientoDestino.SubTipoTransaccionMovimiento.EsTransferenciaCCE)
                    movimientosOrigen = operacionesLavado.Where(p => p.EsOperacionPrincipalLavado).ToList();
            }

            if (registroLavado == null)
                throw new Exception("No se pudo encontrar el lavado de activos de la operación.");
            if (operacionesLavado.Count() <= 0)
                throw new Exception("No existen datos para completar el registro de lavado.");
            if (registroLavado.IndicadorEstado == General.No)
                throw new Exception("El lavado de activo ya fue anulado.");
            if (movimientosOrigen.Count() <= 0)
                throw new Exception("No se puede determinar el movimiento principal para el proceso de la lavado.");

            registroLavado.CompletarDatosDetalle(movimientosOrigen, movimientoDestino, tiposTransaccionesOrigen.First().CodigoCanal);
        }

        /// <summary>
        /// Anula una operaciones de lavado de transferencia interbancaria Inmediata Saliente
        /// </summary>
        /// <param name="lavado"></param>
        /// <exception cref="Exception"></exception>
        public void AnularLavadoActivo(IRegistroLavado lavado)
        {
            if (lavado == null)
                throw new Exception("No se pudo encontrar el lavado de activos de la operación.");
            if (lavado.IndicadorEstado == General.No)
                throw new Exception("El lavado de activo ya fue anulado.");
            lavado.InaplicarLavado();
        }

        #endregion
    }
}
