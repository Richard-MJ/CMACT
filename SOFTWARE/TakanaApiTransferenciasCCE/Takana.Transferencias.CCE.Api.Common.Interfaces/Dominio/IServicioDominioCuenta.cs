using Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;

namespace Takana.Transferencias.CCE.Api.Common.Interfaces.Dominio
{
    public interface IServicioDominioCuenta
    {
        /// <summary>
        /// Interfaz de método que genera movimiento diario de comision
        /// </summary>
        /// <param name="movimientoPrincipal"></param>
        /// <param name="comisionAhorros"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        MovimientoDiario GenerarMovimientoDiarioDeComision(
            MovimientoDiario movimientoPrincipal,
            ComisionAhorrosAuxiliar comisionAhorros,
            Usuario usuario,
            int numeroMovimiento,
           bool indicadorCuentaSueldo);

        /// <summary>
        /// Interfaz que método que genera movimiento contable de comision
        /// </summary>
        /// <param name="movimientoComision"></param>
        /// <param name="cuentaContableComision"></param>
        /// <returns></returns>
        MovimientoAuxiliarLogico GenerarMovimientoContableComision(
           MovimientoDiario movimientoComision,
           string cuentaContableComision);

        /// <summary>
        /// Interfaz de metodo que reversa la transaferencia inmediata
        /// </summary>
        /// <param name="transferencia"></param>
        /// <param name="movimientoRelacionados"></param>
        /// <param name="indicadorCuentaSueldo"></param>
        /// <param name="codigoRespuesta"></param>
        /// <param name="indicadorReversarComision"></param>
        void ReversarTransferenciaInmediata(Transferencia transferencia,
            List<MovimientoDiario> movimientoRelacionados,
            string indicadorCuentaSueldo,
            CodigoRespuesta codigoRespuesta,
            bool indicadorReversarComision);
    }
}
