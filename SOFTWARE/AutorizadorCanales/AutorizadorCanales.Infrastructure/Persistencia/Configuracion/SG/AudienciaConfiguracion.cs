using AutorizadorCanales.Domain.Entidades.SG;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.SG;

public class AudienciaConfiguracion : IEntityTypeConfiguration<Audiencia>
{
    public void Configure(EntityTypeBuilder<Audiencia> builder)
    {
        ConfigurarSistemaCliente(builder);
    }

    private void ConfigurarSistemaCliente(EntityTypeBuilder<Audiencia> builder)
    {
        builder.ToTable("SG_API_AUDIENCIA", "SG");
        builder.HasKey(a => new { a.Id });

        builder.Property(a => a.Id).HasColumnName("ID").IsRequired();
        builder.Property(a => a.IndicadorEstado).HasColumnName("IND_ESTADO").HasMaxLength(1);
        builder.Property(a => a.IdSecreto).HasColumnName("ID_SECRETA_64").HasMaxLength(80);
        builder.Property(a => a.Nombre).HasColumnName("NOMBRE").HasMaxLength(120);
        builder.Property(a => a.IndicadorCanal).HasColumnName("IND_CANAL").HasMaxLength(1);
        builder.Property(a => a.IndicadorTipoAppInt).HasColumnName("IND_TIPO_APP").HasColumnType("smallint");
        builder.Property(a => a.DireccionOrigenPermitido).HasColumnName("DIR_ORIGEN_PERMITIDO").HasMaxLength(1200);
        builder.Property(a => a.IndicadorAplicaInactividad).HasColumnName("IND_APLICA_INACTIVIDAD").IsRequired().HasMaxLength(1);

        builder.Property(a => a.MaximoIntentosFallidos).HasColumnName("NUM_MAX_INTENTOS_FAL").IsRequired();
        builder.Property(a => a.MinutosRangoIntentosFallidos).HasColumnName("NUM_MINU_RANGO_INT_FAL").IsRequired();
        builder.Property(a => a.HorasBloquedasPorIntentosFallidos).HasColumnName("NUM_HORAS_BLOQUEO").IsRequired();
        builder.Property(a => a.MinutosTiempoVidaTokenRefresco).HasColumnName("NUM_MINU_VIDA_TOK_REF").IsRequired();

        builder.Property(a => a.IndicadorBloqueoCanalElectronico).HasColumnName("IND_BLOQUEO_CAN_ELEC");

        builder.Ignore(a => a.IndicadorTipoApp);
    }
}
