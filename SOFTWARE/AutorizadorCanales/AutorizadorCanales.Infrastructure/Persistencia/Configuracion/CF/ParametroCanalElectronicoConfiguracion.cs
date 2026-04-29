using AutorizadorCanales.Domain.Entidades.CF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.CF;

public class ParametroCanalElectronicoConfiguracion : IEntityTypeConfiguration<ParametroCanalElectronico>
{
    public void Configure(EntityTypeBuilder<ParametroCanalElectronico> builder)
    {
        ConfigurarParametroCanalElectronico(builder);
    }

    private void ConfigurarParametroCanalElectronico(EntityTypeBuilder<ParametroCanalElectronico> builder)
    {
        builder.ToTable("CF_PARAMETROS_ATM", "CF");
        builder.HasKey(p => new { p.CodigoParametro, p.CodigoMoneda, p.CodigoCanal, p.CodigoSubCanal });

        builder.Property(p => p.CodigoParametro).HasColumnName("COD_PARAMETRO");
        builder.Property(p => p.CodigoMoneda).HasColumnName("COD_MONEDA");
        builder.Property(p => p.CodigoCanal).HasColumnName("IND_CANAL");
        builder.Property(p => p.CodigoSubCanal).HasColumnName("NUM_SUBCANAL");
        builder.Property(p => p.ValorParametro).HasColumnName("VAL_PARAMETRO").HasPrecision(20, 2);
    }
}
