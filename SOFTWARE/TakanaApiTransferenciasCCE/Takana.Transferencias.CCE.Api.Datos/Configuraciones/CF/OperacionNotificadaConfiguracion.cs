using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF;

public class OperacionNotificadaConfiguracion : IEntityTypeConfiguration<OperacionNotificada>
{
    public void Configure(EntityTypeBuilder<OperacionNotificada> builder)
    {
        ConfigurarOperacionNotificada(builder);
    }

    private static void ConfigurarOperacionNotificada(EntityTypeBuilder<OperacionNotificada> builder)
    {
        builder.ToTable("CF_OPERACION_NOTIFICADA", "CF");
        builder.HasKey(m => new { m.IdOperacionNotificada });

        builder.Property(m => m.IdOperacionNotificada).HasColumnName("ID_OPERACION_NOTIFICADA").IsRequired();

        builder.Property(m => m.NumeroMovimiento).HasColumnName("NUM_MOVIMIENTO").IsRequired().HasMaxLength(100);
        builder.Property(m => m.CodigoTipoTransaccion).HasColumnName("TIP_TRANSACCION").IsRequired().HasMaxLength(5);
        builder.Property(m => m.CodigoSubtipoTransaccion).HasColumnName("SUBTIP_TRANSACCION").IsRequired().HasMaxLength(5);
        builder.Property(m => m.CodigoSistema).HasColumnName("COD_SISTEMA").IsRequired().HasMaxLength(5);
        builder.Property(m => m.CodigoCanal).HasColumnName("COD_CANAL").IsRequired().HasMaxLength(5);
        builder.Property(m => m.CodigoSubCanal).HasColumnName("COD_SUB_CANAL").IsRequired().HasMaxLength(5);
        builder.Property(m => m.FechaRegistro).HasColumnName("FEC_REGISTRO").IsRequired();
        builder.Property(m => m.IndicadorEstado).HasColumnName("IND_ESTADO").IsRequired().HasMaxLength(5);
        builder.Property(m => m.FechaUltimaActualizacion).HasColumnName("FEC_ULT_ACTUALIZACION").IsRequired(false);
    }
}
