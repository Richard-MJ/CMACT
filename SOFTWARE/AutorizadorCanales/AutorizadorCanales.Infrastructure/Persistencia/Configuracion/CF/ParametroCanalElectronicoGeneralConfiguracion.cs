using AutorizadorCanales.Domain.Entidades.CF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.CF;

public class ParametroCanalElectronicoGeneralConfiguracion : IEntityTypeConfiguration<ParametroCanalElectronicoGeneral>
{
    public void Configure(EntityTypeBuilder<ParametroCanalElectronicoGeneral> builder)
    {
        ConfigurarParametroCanalElectronicoGeneral(builder);
    }

    private static void ConfigurarParametroCanalElectronicoGeneral(EntityTypeBuilder<ParametroCanalElectronicoGeneral> builder)
    {
        builder.ToTable("CF_PARAMETROS_CANALES_ELEC_GENERALES", "CF");
        builder.HasKey(m => new { m.IdParametroCanalElectronico });

        builder.Property(m => m.IdParametroCanalElectronico).HasColumnName("ID_PAR_CANAL_ELEC_GEN");
        builder.Property(m => m.IndicadorEstado).HasColumnName("IND_ESTADO");
        builder.Property(m => m.FechaRegistro).HasColumnName("FEC_REGISTRO");
        builder.Property(m => m.IdParametroCanalElectronicoGeneralEquivalente).HasColumnName("ID_PAR_CANAL_ELEC_GEN_EQUIVALENTE");

        builder.Property(m => m.DescripcionParametro).HasColumnName("DESC_PARAMETRO");
        builder.Property(m => m.ValorParametro).HasColumnName("VAL_PARAMETRO");
        builder.Property(m => m.IndicadorCanal).HasColumnName("IND_CANAL");
        builder.Property(m => m.NumeroSubcanal).HasColumnName("NUM_SUBCANAL");
        builder.Property(m => m.CodigoUsuarioRegistro).HasColumnName("COD_USUARIO_REGISTRO");
    }
}
