using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio ComisionCCE de la tabla CF_TIN_INMEDIATA_IMPORTE_COMISION
    /// </summary>
    public class ComisionCCEConfiguracion : IEntityTypeConfiguration<ComisionCCE>
    {
        public void Configure(EntityTypeBuilder<ComisionCCE> builder)
        {
            builder.ToTable("CF_TIN_INMEDIATA_IMPORTE_COMISION", "CF");
            builder.HasKey(m => new { m.Id });

            builder.Property(m => m.Id).HasColumnName("ID_IMPORTE_COMISION");
            builder.Property(m => m.IdTipoTransferencia).HasColumnName("ID_TIPO_TRANSFERENCIA");
            builder.Property(m => m.CodigoComision).HasColumnName("COD_COMISION")
               .HasColumnType("varchar")
               .HasMaxLength(4);
            builder.Property(m => m.CodigoMoneda).HasColumnName("COD_MONEDA")
               .HasColumnType("varchar")
               .HasMaxLength(5);
            builder.Property(m => m.CodigoAplicacionTarifa).HasColumnName("COD_TARIFA")
                .HasColumnType("varchar")
                .HasMaxLength(1);
            builder.Property(m => m.Porcentaje).HasColumnName("POR_TARIFA")
                .HasColumnType("decimal");
            builder.Property(m => m.Minimo).HasColumnName("MIN_TARIFA")
                .HasColumnType("decimal");
            builder.Property(m => m.Maximo).HasColumnName("MAX_TARIFA")
                .HasColumnType("decimal");
            builder.Property(m => m.IndicadorPorcentaje).HasColumnName("IND_PORCENTAJE")
                .HasColumnType("varchar")
                .HasMaxLength(1);
            builder.Property(m => m.IndicadorFijo).HasColumnName("IND_FIJO")
                .HasColumnType("varchar")
                .HasMaxLength(1);
            builder.Property(m => m.PorcentajeCCE).HasColumnName("POR_TARIFA_CCE")
                .HasColumnType("decimal");
            builder.Property(m => m.MinimoCCE).HasColumnName("MIN_TARIFA_CCE")
                .HasColumnType("decimal");
            builder.Property(m => m.MaximoCCE).HasColumnName("MAX_TARIFA_CCE")
                .HasColumnType("decimal");

            builder.HasOne(g => g.Moneda).WithMany().HasForeignKey(g => g.CodigoMoneda);
            builder.HasOne(m => m.TipoTransferencia).WithMany(d => d.Comisiones).HasForeignKey(f => f.IdTipoTransferencia);
            
        }
    }
}
