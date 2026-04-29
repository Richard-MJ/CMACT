using Takana.Transferencias.CCE.Api.Common.Constantes;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF
{
    /// <summary>
    /// Clase de dominio que representa el catalogo de sub transacciones en el sistema
    /// </summary>
    public class SubTipoTransaccion : Empresa
    {
        #region Propiedades

        /// <summary>
        /// Propiedad que representa el código del sistema
        /// </summary>
        public string CodigoSistema { get; private set; }

        /// <summary>
        /// Propiedad que representa el Código de tipo de transacción
        /// </summary>
        public string CodigoTipoTransaccion { get; private set; }

        /// <summary>
        /// Propiedad que representa el Código de tipo de sub transacción
        /// </summary>
        public string CodigoSubTipoTransaccion { get; private set; }

        /// <summary>
        /// Propiedad que representa el detalle de la sub transacción
        /// </summary>
        public string DescripcionSubTransaccion { get; private set; }
        /// <summary>
        /// Tipo de operación
        /// </summary>
        public string TipoOperacion { get; private set; }
        /// <summary>
        /// Aplica contabilizacion
        /// </summary>
        public string AplicaContabilizadon { get; private set; }
        /// <summary>
        /// Codigo de aplicacion de lavado
        /// </summary>
        public string AplicaLavado { get; private set; }
        /// <summary>
        /// Indicador de contable principal
        /// </summary>
        public string IndicadorContablePrincipal { get; private set; }
        /// <summary>
        /// Indicador de comando principal
        /// </summary>
        public string IndicadorComandoPrincipal { get; private set; }
        /// <summary>
        /// Codigo de forma de pago de lavado
        /// </summary>
        public string CodigoFormaPagoLavado { get; private set; }
        /// <summary>
        /// Descripcion auxiliar a mostrar de la subtransaccion
        /// </summary>
        public string? DescripcionAuxiliar { get; private set; }
        /// <summary>
        /// Propiedad que representa el Indicador Movimiento Lavando de I = Ingreso , E = egreso
        /// </summary>
        public string? IndicadorMovimientoLavando { get; private set; }
        /// <summary>
        /// Propiedad que representa el tipo de transacción relacionado
        /// </summary>
        public virtual CatalogoTransaccion Transaccion { get; private set; }
        /// <summary>
        /// indicador de si es contabilizable
        /// </summary>
        public bool EsContabilizable { get { return AplicaContabilizadon == General.Si; } }
        /// <summary>
        /// Indicador si es detalle contable principal
        /// </summary>
        public bool EsDetalleContablePrincipal { get { return IndicadorContablePrincipal == General.Si; } }
        /// <summary>
        /// Indica que es una transferencia de CCE
        /// </summary>
        public bool EsTransferenciaCCE
            => (CodigoSistema == Sistema.CuentaEfectivo && CodigoTipoTransaccion == ((int)CatalogoTransaccionEnum.CodigoTransferenciaInmediataSaliente).ToString()
            &&
            (
                CodigoSubTipoTransaccion == ((int)SubTipoTransaccionEnum.TransaccionInmediataOrdinariaSalida).ToString()
            ||
                CodigoSubTipoTransaccion == ((int)SubTipoTransaccionEnum.TransaccionInmediataTarjetaSalida).ToString()
            ||
                CodigoSubTipoTransaccion == ((int)SubTipoTransaccionEnum.TransaccionInteroperabilidad).ToString()
            ));

        /// <summary>
        /// Indicador que es una comision de CCE
        /// </summary>
        public bool EsComisionCCE
            => CodigoTipoTransaccion ==
                ((int)CatalogoTransaccionEnum.CodigoTransferenciaInmediataComision).ToString()
            && CodigoSubTipoTransaccion ==
                ((int)SubTipoTransaccionEnum.CodigoTransaccionCargoComision).ToString();

        #endregion Propiedades
    }
}


