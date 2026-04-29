using Takana.Transferencias.CCE.Api.Common.Constantes;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF
{
    public class ComisionCCE
    {
        /// <summary>
        /// Identificador de comision
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// identificador de tipo de transferencia
        /// </summary>
        public int IdTipoTransferencia { get; private set; }
        /// <summary>
        /// Codigo de comisión
        /// </summary>
        public string CodigoComision { get; private set; }
        /// <summary>
        /// Codigo de Moneda
        /// </summary>
        public string CodigoMoneda { get; private set; }
        /// <summary>
        /// Codigo de aplicacion de tarifa
        /// </summary>
        public string CodigoAplicacionTarifa { get; private set; }
        /// <summary>
        /// Porcentaje
        /// </summary>
        public decimal Porcentaje { get; private set; }
        /// <summary>
        /// Minimo de comision
        /// </summary>
        public decimal Minimo { get; private set; }
        /// <summary>
        /// Maximo de comision
        /// </summary>
        public decimal Maximo { get; private set; }
        /// <summary>
        /// Indicador de porcentaje
        /// </summary>
        public string IndicadorPorcentaje { get; private set; }
        /// <summary>
        /// Indicador de fijo
        /// </summary>
        public string IndicadorFijo { get; private set; }
        /// <summary>
        /// Porcentaje de CCE
        /// </summary>
        public decimal PorcentajeCCE { get; private set; }
        /// <summary>
        /// Minimo de la CCE
        /// </summary>
        public decimal MinimoCCE { get; private set; }
        /// <summary>
        /// Maximo de la CCE
        /// </summary>
        public decimal MaximoCCE { get; private set; }
        /// <summary>
        /// Entidad de Moneda
        /// </summary>
        public virtual Moneda Moneda { get; private set; }
        /// <summary>
        /// Entidad de Tipo de transferencia
        /// </summary>
        public virtual TipoTransferencia TipoTransferencia { get; private set; }
        /// <summary>
        /// Propiedad que indica si es misma plaza
        /// </summary>
        public bool EsMismaPlaza => CodigoAplicacionTarifa == General.MismaPlaza;
    }
}
