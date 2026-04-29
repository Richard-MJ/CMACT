using AutorizadorCanales.Domain.Entidades.TJ;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.TJ;

public class TarjetaMovimientoConfiguracion : IEntityTypeConfiguration<TarjetaMovimiento>
{
    public void Configure(EntityTypeBuilder<TarjetaMovimiento> builder)
    {
        ConfigurarTarjetaMovimiento(builder);
    }

    private static void ConfigurarTarjetaMovimiento(EntityTypeBuilder<TarjetaMovimiento> builder)
    {
        builder.ToTable("TJ_MOVIMIENTOS", "TJ");
        builder.HasKey(m => new { m.NumeroMovimiento });

        builder.Property(m => m.NumeroMovimiento).HasColumnName("NUM_MOVIMIENTO").IsRequired();
        builder.Property(m => m.CodigoAgencia).HasColumnName("COD_AGENCIA").IsRequired();
        builder.Property(m => m.CodigoUsuario).HasColumnName("COD_USUARIO").IsRequired();
        builder.Property(m => m.CodigoCliente).HasColumnName("COD_CLIENTE").IsRequired();
        builder.Property(m => m.NumeroTarjeta).HasColumnName("NUM_TARJETA").IsRequired();
        builder.Property(m => m.CodigoTipoTransaccion).HasColumnName("TIP_TRANSACCION").IsRequired();
        builder.Property(m => m.CodigoSubTipoTransaccion).HasColumnName("SUBTIP_TRANSAC").IsRequired();
        builder.Property(m => m.FechaMovimiento).HasColumnName("FEC_MOVIMIENTO").IsRequired();
        builder.Property(m => m.IndicadorEstado).HasColumnName("IND_ESTADO").IsRequired();
        builder.Property(m => m.Observaciones).HasColumnName("OBS_MOVIMTO").IsRequired();
        builder.Property(m => m.IndicadorCanal).HasColumnName("IND_TTS").IsRequired(false);
    }
}
