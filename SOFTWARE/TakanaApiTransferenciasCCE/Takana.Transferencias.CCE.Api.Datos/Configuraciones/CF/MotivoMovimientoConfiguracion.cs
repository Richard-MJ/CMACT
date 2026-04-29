using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio MotivoMovimiento de la tabla CF_MOTIVO_MOVIMIENTO
    /// </summary>
    public class MotivoMovimientoConfiguracion : IEntityTypeConfiguration<MotivoMovimiento>
    {
        public void Configure(EntityTypeBuilder<MotivoMovimiento> builder)
        {
            builder.ToTable("CF_MOTIVO_MOVIMIENTO", "CF");
            builder.HasKey(k => new { k.IdMotivoMovimiento });

            builder.Property(p => p.IdMotivoMovimiento).HasColumnName("ID_MOTIVO_MOVIMIENTO");
            builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA").HasMaxLength(2);
            builder.Property(p => p.Descripcion).HasColumnName("DESCRIPCION").HasMaxLength(100);
            builder.Property(p => p.IndicadorActivo).HasColumnName("IND_ACTIVO");
            builder.Property(p => p.IndicadorEspecificar).HasColumnName("IND_ESPECIFICAR");
            builder.Property(p => p.CodigoUsuarioModificador).HasColumnName("COD_USUARIO_MODIFICADOR").HasMaxLength(15);
            builder.Property(p => p.FechaModificacion).HasColumnName("FEC_MODIFICACION");
        }
    }
}
