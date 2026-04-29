using AutorizadorCanales.Domain.Entidades.CL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.CL;

public class DispositivoCanalElectronicoConfiguracion : IEntityTypeConfiguration<DispositivoCanalElectronico>
{
    public void Configure(EntityTypeBuilder<DispositivoCanalElectronico> builder)
    {
        ConfigurarDispositivoCanalElectronico(builder);
    }

    private static void ConfigurarDispositivoCanalElectronico(EntityTypeBuilder<DispositivoCanalElectronico> builder)
    {
        builder.HasKey(k => new { k.DispositivoId, k.NumeroTarjeta, k.CodigoCliente });
        builder.ToTable("CL_DISPOSITIVOS_CANAL_ELECTRONICOS", "CL");

        builder.Property(p => p.IndicadorCanal).HasColumnName("IND_CANAL");
        builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA");
        builder.Property(p => p.FechaGeneracion).HasColumnName("FEC_GENERACION");
        builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO");
        builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO");

        builder.Property(p => p.DispositivoId).HasColumnName("DISPOSITIVO_ID");
        builder.Property(p => p.NumeroTarjeta).HasColumnName("NUM_TARJETA");
        builder.Property(p => p.CodigoCliente).HasColumnName("COD_CLIENTE");

        builder
            .HasOne(tm => tm.Tarjeta)
            .WithMany(d => d.DispositivosCanalElectronico)
            .HasForeignKey(tm => tm.NumeroTarjeta)
            .IsRequired();
    }
}
