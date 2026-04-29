using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio TransferenciaDetalleSalienteCCE de la tabla CC_TRANSFERENCIAS_DETALLE_CCE
    /// </summary>
    public class TransferenciaDetalleCCEConfiguracion : IEntityTypeConfiguration<TransferenciaDetalleSalienteCCE>
    {
        public void Configure(EntityTypeBuilder<TransferenciaDetalleSalienteCCE> builder)
        {
            builder.ToTable("CC_TRANSFERENCIAS_DETALLE_CCE", "CC");
            builder.HasKey(k => new { k.NumeroTransferencia, k.NumeroDetalle });

            builder.Property(p => p.NumeroTransferencia).HasColumnName("NUM_TRANSFERENCIA").IsRequired();
            builder.Property(p => p.NumeroDetalle).HasColumnName("NUM_DETALLE").IsRequired();
            builder.Property(p => p.CodigoCuentaInterbancario).HasColumnName("NUM_CUENTA_CCI").IsRequired();
            builder.Property(p => p.IDEntidadFinanciera).HasColumnName("ID_ENTIDAD").IsRequired();
            builder.Property(p => p.CodigoTipoDocumento).HasColumnName("COD_TIPO_DOCUMENTO").IsRequired();
            builder.Property(p => p.NumeroDocumento).HasColumnName("NUM_DOCUMENTO").IsRequired();
            builder.Property(p => p.Beneficiario).HasColumnName("BENEFICIARIO").IsRequired();
            builder.Property(p => p.Nombres).HasColumnName("NOMBRES_BENEFICIARIO");
            builder.Property(p => p.ApellidoPaterno).HasColumnName("PATERNO_BENEFICIARIO");
            builder.Property(p => p.ApellidoMaterno).HasColumnName("MATERNO_BENEFICIARIO");
            builder.Property(p => p.MismoTitular).HasColumnName("IND_TITULAR").IsRequired();
            builder.Property(p => p.RequiereConfirmacion).HasColumnName("IND_CONFIRMACION").IsRequired();
            builder.Property(p => p.MontoOperacion).HasColumnName("MON_TRANSFERENCIA").IsRequired();
            builder.Property(p => p.PeriodoPagoCTS).HasColumnName("PER_PAGO_REMUNERACION");
            builder.Property(p => p.MontoRemuneraciones).HasColumnName("MON_REMUNERACION");
            builder.Property(p => p.MontoComision).HasColumnName("MON_COMISION").IsRequired();
            builder.Property(p => p.CodigoTarifario).HasColumnName("COD_TARIFA").IsRequired();
            builder.Property(p => p.EstaActivo).HasColumnName("IND_ACTIVO").IsRequired();

            builder.HasOne(m => m.Transferencia).WithMany(d => d.DetallesSalientes).HasForeignKey(f => f.NumeroTransferencia);
            builder.HasOne(m => m.EntidadDestino).WithMany().HasForeignKey(f => f.IDEntidadFinanciera);
        }
    }
}
