using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio TransferenciaDetalleEntranteCCE de la tabla CC_TRANSFERENCIAS_DETALLE_ENTRANTES_CCE
    /// </summary>
    public class TransferenciaDetalleEntranteConfiguracion : IEntityTypeConfiguration<TransferenciaDetalleEntranteCCE>
    {
        public void Configure(EntityTypeBuilder<TransferenciaDetalleEntranteCCE> builder)
        {
            builder.ToTable("CC_TRANSFERENCIAS_DETALLE_ENTRANTES_CCE", "CC");
            builder.HasKey(k => new { k.NumeroTransferencia, k.CodigoCuentaInterbancario });

            builder.Property(p => p.NumeroTransferencia).HasColumnName("NUM_TRANSFERENCIA").IsRequired();
            builder.Property(p => p.CodigoCuentaInterbancario).HasColumnName("NUM_CUENTA_CCI").IsRequired();
            builder.Property(p => p.CodigoCliente).HasColumnName("COD_CLIENTE").IsRequired();
            builder.Property(p => p.IDEntidadFinanciera).HasColumnName("ID_ENTIDAD").IsRequired();
            builder.Property(p => p.CodigoTipoDocumento).HasColumnName("COD_TIPO_DOCUMENTO").IsRequired();
            builder.Property(p => p.NumeroDocumento).HasColumnName("NUM_DOCUMENTO").IsRequired();
            builder.Property(p => p.Ordenante).HasColumnName("ORDENANTE").IsRequired();
            builder.Property(p => p.Nombres).HasColumnName("NOMBRES_ORDENANTE");
            builder.Property(p => p.ApellidoPaterno).HasColumnName("PATERNO_ORDENANTE");
            builder.Property(p => p.ApellidoMaterno).HasColumnName("MATERNO_ORDENANTE");
            builder.Property(p => p.MismoTitular).HasColumnName("IND_TITULAR").IsRequired();
            builder.Property(p => p.MontoOperacion).HasColumnName("MON_TRANSFERENCIA").IsRequired();
            builder.Property(p => p.MontoComision).HasColumnName("MON_COMISION").IsRequired();
            builder.Property(p => p.CodigoTarifario).HasColumnName("COD_TARIFA").IsRequired();
            builder.Property(p => p.EstaActivo).HasColumnName("IND_ACTIVO").IsRequired();

            builder.HasOne(m => m.Transferencia).WithMany(d => d.DetallesEntrantes).HasForeignKey(f => f.NumeroTransferencia);
        }
    }
}