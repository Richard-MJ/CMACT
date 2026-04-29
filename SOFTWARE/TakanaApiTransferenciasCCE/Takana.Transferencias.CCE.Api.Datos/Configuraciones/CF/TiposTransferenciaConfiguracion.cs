using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF;

/// <summary>
/// Clase que representa el mapeo de la clase de Dominio TipoTransferencia de la tabla CF_TIN_INMEDIATA_TIPO_TRANSFERENCIA
/// </summary>
public class TiposTransferenciaConfiguracion: IEntityTypeConfiguration<TipoTransferencia>
{
    public void Configure(EntityTypeBuilder<TipoTransferencia> builder)
    {
        builder.ToTable("CF_TIN_INMEDIATA_TIPO_TRANSFERENCIA", "CF");
        builder.HasKey(m => new { m.IdTipoTransferencia });

        builder.Property(m => m.IdTipoTransferencia).HasColumnName("ID_TIPO_TRANSFERENCIA");
        builder.Property(m => m.Codigo).HasColumnName("COD_TIPO_TRANSFERENCIA")
            .IsUnicode(true)
            .HasColumnType("varchar")
            .HasMaxLength(3);
        builder.Property(m => m.Descripcion).HasColumnName("DES_TIPO_TRANSFERENCIA")
            .HasColumnType("varchar")
            .HasMaxLength(50);
        builder.Property(m => m.IndicadorEstado).HasColumnName("IND_ESTADO")
            .HasColumnType("varchar")
            .HasMaxLength(1);        
    }    
}