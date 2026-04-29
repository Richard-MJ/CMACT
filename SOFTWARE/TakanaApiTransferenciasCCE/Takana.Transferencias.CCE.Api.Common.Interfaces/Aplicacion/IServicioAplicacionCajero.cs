using Takana.Transferencias.CCE.Api.Common.DTOs.CG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;

namespace Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion
{
    public interface IServicioAplicacionCajero
    {
        /// <summary>
        /// Obtiene la tasa de cambio
        /// </summary>
        /// <param name="codigoTipoTasaCambio">Codigo del tipo de tasa de cambio</param>
        /// <param name="fechaSistema">fecha del sistema</param>
        /// <returns>Tasa de cambio</returns>
        public ITasaCambio ObtenerTasaCambio(string codigoTipoTasaCambio, DateTime fechaSistema);

        /// <summary>
        /// Obtener listado de cuenta puente
        /// </summary>
        /// <param name="codigoCuentaContable"></param>
        /// <param name="codigoCuentaContableComision"></param>
        /// <returns>Retorna listado de cuentas puente</returns>
        List<ParametroPorEmpresa> ObtenerListaCuentaPuente(
            string codigoCuentaContable, string codigoCuentaContableComision);

        /// <summary>
        /// Método que obtiene el monto de ITF de un Movimiento Diario
        /// </summary>
        /// <param name="movimientoDiario">movimiento diario</param>
        /// <returns>monto del ITF</returns>
        decimal ObtenerMontoItfMovimiento(MovimientoDiario movimientoDiario);

        /// <summary>
        /// Metodo que obtiene le monto itf 
        /// </summary>
        /// <param name="monto">Monto de la operacion</param>
        /// <param name="cuentaExonerada">Indica si es cuenta exonerada</param>
        /// <param name="esCuentaSueldo">Indica si es cuenta sueldo</param>
        /// <param name="fecha">Fecha de la operacion</param>
        /// <returns>Retorna  el monto ITF</returns>
        decimal ObtenerMontoITF(decimal monto, bool cuentaExonerada,
            bool esCuentaSueldo, DateTime fecha, string tipoTitular);

        /// <summary>
        /// Obtiene la tasa de cambio local asociada a la cuenta contable indicada.
        /// </summary>
        decimal ObtenerTasaCambioLocal(CuentaContable cuentaContable, string codigoAgenciaOrigen, DateTime fechaSistema);

        /// <summary>
        /// Obtiene la tasa de cambio correspondiente a la cuenta contable indicada.
        /// </summary>
        decimal ObtenerTasaCambioCuenta(CuentaContable cuentaContable, string codigoAgenciaOrigen, DateTime fechaSistema);

        /// <summary>
        /// Obtiene la información de la cuenta contable según los criterios proporcionados.
        /// </summary>
        CuentaContableInfoDTO ObtenerDatosCuentaContable(
            string codigoCuenta,
            string codigoAgencia,
            string codigoUnidadEjecutora,
            string tipoAsiento,
            string detalleCuenta,
            DateTime fechaSistema);
    }
}
