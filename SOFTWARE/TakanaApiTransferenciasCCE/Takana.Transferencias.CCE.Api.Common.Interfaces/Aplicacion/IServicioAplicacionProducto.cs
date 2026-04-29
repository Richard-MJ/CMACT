using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.TJ;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;

namespace Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion
{
    public interface IServicioAplicacionProducto
    {
        /// <summary>
        /// Obtiene la entidad cliente a partir de el número de su tarjeta de debito.
        /// </summary>
        /// <param name="numeroTarjeta">Número de tarjeta de debito de cliente.</param>
        /// <returns>Entidad cliente dueña de la tarjeta de debito.</returns>
        Tarjeta ObtenerClienteAPartirDeTarjeta(string numeroTarjeta);

        /// <summary>
        /// Obtiene las operaciones frecuentes del cliente
        /// </summary>
        List<OperacionFrecuente> ObtenerOperacionesFrecuentesDeCliente(string codigoCliente);

        /// <summary>
        /// Obtiene Las cuentas del cliente donde este figure como TITULAR de la misma
        /// </summary>
        /// <param name="codigoCliente"></param>
        /// <returns></returns>
        ICollection<CuentaEfectivo> ObtenerProductosPasivosDeGrupoDebito(string codigoCliente, string codigoGrupoProductos);

        /// <summary>
        /// Metodo que crea el debito por transferencia inmediata
        /// </summary>
        /// <param name="cuenta">Cuenta efectivo de la transaccion</param>
        /// <param name="detalle">datos de la transaccion</param>
        /// <param name="subTransaccion">subtransaccion</param>
        /// <param name="subTransaccionITF">subtransaccion ITF</param>
        /// <param name="usuario">Usuario del sistema</param>
        /// <param name="fechaSistema">fecha del sistema</param>
        /// <returns>Retorna los movimientos debitados</returns>
        IList<MovimientoDiario> GenerarMovimientoTransferenciaSaliente(
            CuentaEfectivo cuenta, 
            RealizarTransferenciaInmediataDTO detalle,
            SubTipoTransaccion subTransaccion,
            SubTipoTransaccion subTransaccionITF, 
            Usuario usuario, 
            DateTime fechaSistema);

        /// <summary>
        /// Método para agregar el movimiento ITF si es necesario
        /// </summary>
        /// <param name="movimientosDiariosCuentaEfectivo"></param>
        /// <param name="usuario"></param>
        /// <param name="subTipoTransaccionITF"></param>
        /// <param name="transaccion"></param>
        /// <param name="cuentaEfectivo"></param>
        /// <param name="movimientoEnCuentaEfectivo"></param>
        void GenerarMovimientoITF(
            List<MovimientoDiario> movimientosDiariosCuentaEfectivo,
            Usuario usuario,
            SubTipoTransaccion subTipoTransaccionITF,
            TransaccionOrdenTransferenciaInmediata transaccion,
            CuentaEfectivo cuentaEfectivo,
            MovimientoDiario movimientoEnCuentaEfectivo);
    }
}
