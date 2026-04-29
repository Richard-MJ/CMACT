using AutorizadorCanales.Domain.Entidades.CF;
using AutorizadorCanales.Domain.Entidades.SG;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.CF;

public class CanalElectronicoConfiguracion : IEntityTypeConfiguration<CanalElectronico>
{
    public void Configure(EntityTypeBuilder<CanalElectronico> builder)
    {
        ConfigurarCanalElectronico(builder);
    }

    private static void ConfigurarCanalElectronico(EntityTypeBuilder<CanalElectronico> builder)
    {
        builder.ToTable("CF_ORIGEN_TTS", "CF");
        builder.HasKey(o => o.CodigoCanal);
        builder.Property(o => o.CodigoCanal).HasColumnName("IND_TTS").IsRequired().HasMaxLength(1);
        builder.Property(o => o.DescripcionTTS).HasColumnName("DES_TTS");
        builder.Property(o => o.IndicadorOperacionCanalPermitido).HasColumnName("IND_OPERACION_PERMITIDA_CANALES");
        
        builder.Property(o => o.DescripcionCanal).HasColumnName("DES_CANAL").IsRequired().HasMaxLength(30);

        builder.HasOne(a => a.SistemaCliente).WithOne(b => b.CanalElectronico).HasForeignKey<Audiencia>(c => c.IndicadorCanal);
    }
}
