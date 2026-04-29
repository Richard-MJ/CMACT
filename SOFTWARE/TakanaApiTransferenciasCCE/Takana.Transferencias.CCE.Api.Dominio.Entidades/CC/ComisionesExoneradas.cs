namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    public class ComisionExonerada : Empresa
    {
        /// <summary>
        /// Id de la comiisón exonerada
        /// </summary>
        public long CodigoComisionExonerada { get; private set; }
        /// <summary>
        /// Número del movimiento (diario o mensual) origen
        /// </summary>
        public decimal NumeroMovimientoOrigen { get; private set; }
        /// <summary>
        /// Código de la comisión exonerada
        /// </summary>
        public string CodigoComision { get; private set; }
        /// <summary>
        /// Codigo del estado
        /// </summary>
        public bool IndicadorEstado { get; private set; }
        /// <summary>
        /// fecha del registro
        /// </summary>
        public DateTime FechaRegistro { get; private set; }
        /// <summary>
        /// usuario que registro
        /// </summary>
        public string CodigoUsuarioRegistro { get; private set; }
        /// <summary>
        /// fecha de la ultima modificación
        /// </summary>
        public DateTime? FechaModificacion { get; private set; }
        /// <summary>
        /// ultimo usuario en modificar
        /// </summary>
        public string CodigoUsuarioModificacion { get; private set; }
        /// <summary>
        /// Creación de la comision exonerada
        /// </summary>
        /// <param name="movimientoDiario">movimiento diario</param>
        /// <param name="codigoComision">código de comisión exonerada</param>
        /// <returns>entidad de comision exonerada creada</returns>
        public static ComisionExonerada Crear(
            MovimientoDiario movimientoDiario,
            string codigoComision)
        {
            return new ComisionExonerada()
            {
                NumeroMovimientoOrigen = movimientoDiario.NumeroMovimiento,
                CodigoComision = codigoComision,
                IndicadorEstado = true,
                FechaRegistro = movimientoDiario.FechaMovimiento,
                CodigoUsuarioRegistro = movimientoDiario.CodigoUsuario,
                CodigoEmpresa = CodigoPrincipal,
            };
        }
    }
}
