using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio OperacionesVinculosMotivos de la tabla CC_OPERACIONES_VINCULOS_MOTIVOS
    /// </summary>
    public class OperacionesVicnculosMotivosConfiguracion : IEntityTypeConfiguration<OperacionesVinculosMotivos>
    {
        public void Configure(EntityTypeBuilder<OperacionesVinculosMotivos> builder)
        {
            builder.ToTable("CC_OPERACIONES_VINCULOS_MOTIVOS", "CC");
            builder.HasKey(k => new { k.IdOperacionVinculoMotivo });

            builder.Property(p => p.IdOperacionVinculoMotivo).HasColumnName("ID_OPERACION_VINCULO_MOTIVO");
            builder.Property(p => p.NumeroOperacion).HasColumnName("NUM_OPERACION");
            builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA");
            builder.Property(p => p.CodigoAgencia).HasColumnName("COD_AGENCIA");
            builder.Property(p => p.IdVinculoMovimiento).HasColumnName("ID_VINCULO");
            builder.Property(p => p.EspecificarDetalleVinculo).HasColumnName("DES_VINCULO").HasMaxLength(100);
            builder.Property(p => p.IdMotivoMovimiento).HasColumnName("ID_MOTIVO");
            builder.Property(p => p.EspecificarDetalleMotivo).HasColumnName("DES_MOTIVO").HasMaxLength(100);
            builder.Property(p => p.FechaRegistro).HasColumnName("FEC_INGRESO");
            builder.Property(p => p.IdNacionalidad).HasColumnName("COD_PAIS_BENEF");
            
            builder.HasOne(r => r.MotivoMovimiento).WithMany().HasForeignKey(f => new { f.IdMotivoMovimiento });
            builder.HasOne(r => r.VinculoMovimiento).WithMany().HasForeignKey(f => new { f.IdVinculoMovimiento });
            builder.HasOne(r => r.Nacion).WithMany().HasForeignKey(f => new { f.IdNacionalidad });
        }
    }
}
