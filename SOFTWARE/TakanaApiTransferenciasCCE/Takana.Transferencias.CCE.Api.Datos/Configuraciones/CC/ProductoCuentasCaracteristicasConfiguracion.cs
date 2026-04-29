using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.SG
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio ProductoCuentasCaracteristicas de la tabla CC_CARA_X_PRODUCTO
    /// </summary>
    public class ProductoCuentasCaracteristicasConfiguracion : IEntityTypeConfiguration<ProductoCuentasCaracteristicas>
    {
        public void Configure(EntityTypeBuilder<ProductoCuentasCaracteristicas> builder)
        {
            builder.ToTable("CC_CARA_X_PRODUCTO", "CC");
            builder.HasKey(k => new { k.CodigoEmpresa, k.CodigoSistema, k.CodigoProducto });

            builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA").IsRequired();
            builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA").IsRequired();
            builder.Property(p => p.CodigoProducto).HasColumnName("COD_PRODUCTO").IsRequired();
            builder.Property(p => p.IndCtaAlterna).HasColumnName("IND_CTA_ALTERNA");
            builder.Property(p => p.IndPagInteres).HasColumnName("IND_PAG_INTERES");
            builder.Property(p => p.TipoAsignacionTasa).HasColumnName("TIP_ASIGNA_TASA");
            builder.Property(p => p.TipoCapitalizacion).HasColumnName("TIP_CAPITALIZACION");
            builder.Property(p => p.CarManejo).HasColumnName("CAR_MANEJO");
            builder.Property(p => p.CarSobregiro).HasColumnName("CAR_SOBREGIRO");
            builder.Property(p => p.CarReserva).HasColumnName("CAR_RESERVA");
            builder.Property(p => p.LimCksGirados).HasColumnName("LIM_CKS_GIRADOS");
            builder.Property(p => p.MonCkAdicion).HasColumnName("MON_CK_ADICION");
            builder.Property(p => p.TasaSobregiro).HasColumnName("TAS_SOBREGIRO");
            builder.Property(p => p.TasaReserva).HasColumnName("TAS_RESERVA");
            builder.Property(p => p.MontoMinimoSaldoApertura).HasColumnName("MON_MIN_APERTURA_CTA");
            builder.Property(p => p.CodigoProductoAsociado).HasColumnName("COD_PRODUCTO_ASOC");
            builder.Property(p => p.DiaBaseReserva).HasColumnName("DIA_BASE_RESERVA");
            builder.Property(p => p.DiaBaseSobregiro).HasColumnName("DIA_BASE_SOBREGIRO");
            builder.Property(p => p.TasaInteres).HasColumnName("TAS_INTERES");
            builder.Property(p => p.IndChequera).HasColumnName("IND_CHEQUERA");
            builder.Property(p => p.IndModApertura).HasColumnName("IND_MOD_APERTURA");
            builder.Property(p => p.CodigoCuentaContable).HasColumnName("CUENTA_CONTABLE");
            builder.Property(p => p.IndReservaProm).HasColumnName("IND_RESERVA_A_PROM");
            builder.Property(p => p.IndCalInteres).HasColumnName("IND_CAL_INTERES");
            builder.Property(p => p.DiaBaseInteres).HasColumnName("DIA_BASE_INTERES");
            builder.Property(p => p.DiaBaseCalculo).HasColumnName("DIA_BASE_CALCULO");
            builder.Property(p => p.PorDispCTS).HasColumnName("POR_DISP_CTS");
            builder.Property(p => p.IndOrdenPago).HasColumnName("IND_ORDEN_PAGO");
            builder.Property(p => p.IndSorteo).HasColumnName("IND_SORTEO");
            builder.Property(p => p.MontoMinimoSaldo).HasColumnName("MON_MIN_SALDO_CTA");
            builder.Property(p => p.CodigoUnidad).HasColumnName("COD_UNIDAD");
            builder.Property(p => p.IndCtaExcedente).HasColumnName("IND_CTA_EXCEDENTE");
            builder.Property(p => p.IndTipoCuenta).HasColumnName("IND_TIPO_CUENTA");
            builder.Property(p => p.CatCliente).HasColumnName("CAT_CLIENTE");
            builder.Property(p => p.CodigoCliente).HasColumnName("COD_CLIENTE");
            builder.Property(p => p.CtaIntCTS).HasColumnName("CTA_INT_CTS");
            builder.Property(p => p.CodigoProductoGeneral).HasColumnName("COD_PRODUCTO_GRAL");
            builder.Property(p => p.CuentaContableInactiva).HasColumnName("CTA_CONTABLE_INACTIVA");
            builder.Property(p => p.MontoMinimoTransaccion).HasColumnName("MON_MIN_TRANSACCION");
            builder.Property(p => p.PorDispIntCTS).HasColumnName("POR_DISP_INT_CTS");
            builder.Property(p => p.IndComiExcesoRet).HasColumnName("IND_COMI_EXCESO_RET");
            builder.Property(p => p.PorDispExec).HasColumnName("POR_DISP_EXEC");
            builder.Property(p => p.NumRemuCal).HasColumnName("NUM_REMU_CAL");
            builder.Property(p => p.IndTerceroML).HasColumnName("IND_TERCERO_ML");
            builder.Property(p => p.IndAfilMicroseguro).HasColumnName("IND_AFIL_MICROSEGURO");
            builder.Property(p => p.MontoMaximoSaldo).HasColumnName("MON_MAX_SALDO_CTA");
            builder.Property(p => p.MontoMaximoTransaccionDia).HasColumnName("MON_MAX_TRANSAC_X_DIA");
            builder.Property(p => p.MontoMaximoTransaccionMes).HasColumnName("MON_MAX_TRANSAC_X_MES");
            builder.Property(p => p.IndAplComChequeOtraPlaza).HasColumnName("IND_APL_COM_CHE_OTRA_PLAZA");
            builder.Property(p => p.IndPagoTCCCEHB).HasColumnName("IND_PAGO_TC_CCE_HB");
            builder.Property(p => p.IndTransfCCEHB).HasColumnName("IND_TRANSF_CCE_HB");
            builder.Property(p => p.AplicaParaDebitoAutomaticoCuentas).HasColumnName("IND_DEB_AUT_CREDITO").HasMaxLength(1).IsRequired();
            builder.Property(p => p.IndicadorProductoExoneradoComision).HasColumnName("IND_EXON_COMISION").HasMaxLength(1);
            builder.Property(p => p.NumeroMaximoAperturas).HasColumnName("NUM_MAX_APERTURAS");
            builder.Property(p => p.IndTransfCCETIN).HasColumnName("IND_TRANSF_CCE_TIN");

            builder.HasOne(c => c.Producto).WithMany().HasForeignKey(c => new { c.CodigoEmpresa, c.CodigoSistema, c.CodigoProducto });
        }
    }
}
