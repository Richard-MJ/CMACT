using AutorizadorCanales.Domain.Entidades.CL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.CL;

public class TipoBiometriaConfiguracion : IEntityTypeConfiguration<TipoBiometria>
{
    public void Configure(EntityTypeBuilder<TipoBiometria> builder)
    {
        ConfigurarTipoBiometria(builder);
    }

    private static void ConfigurarTipoBiometria(EntityTypeBuilder<TipoBiometria> builder)
    {
        builder.ToTable("CL_TIPO_BIOMETRIA", "CL");
        builder.HasKey(k => new { k.NumeroTipoBiometria });
        builder.Property(p => p.NumeroTipoBiometria).HasColumnName("NUM_TIPO_BIOMETRIA")
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property(p => p.DescripcionAfiliacion).HasColumnName("DESCRIPCION_AFILIACION");
        builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO").HasMaxLength(1);
        builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO");
        builder.Property(p => p.CodigoUsuario).HasColumnName("COD_USUARIO");
    }
}
