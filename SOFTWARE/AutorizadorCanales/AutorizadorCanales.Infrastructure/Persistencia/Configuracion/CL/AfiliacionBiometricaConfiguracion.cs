using AutorizadorCanales.Domain.Entidades.CL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.CL;

public class AfiliacionBiometricaConfiguracion : IEntityTypeConfiguration<AfiliacionBiometrica>
{
    public void Configure(EntityTypeBuilder<AfiliacionBiometrica> builder)
    {
        ConfigurarAfiliacionBiometria(builder);
    }

    private static void ConfigurarAfiliacionBiometria(EntityTypeBuilder<AfiliacionBiometrica> builder)
    {
        builder.ToTable("CL_AFILIACION_BIOMETRICA", "CL");
        builder.HasKey(k => new { k.NumeroAfiliacionBiometria });

        builder.Property(p => p.NumeroAfiliacionBiometria).HasColumnName("NUM_AFILIACION_BIO")
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property(p => p.NumeroTipoBiometria).HasColumnName("NUM_TIPO_BIOMETRIA");
        builder.Property(p => p.NumeroAfiliacion).HasColumnName("NUM_AFILIACION");
        builder.Property(p => p.DispositivoId).HasColumnName("DISPOSITIVO_ID");
        builder.Property(p => p.ClaveAfiliacion).HasColumnName("CLAVE_AFILIACION");
        builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO").HasMaxLength(1);
        builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO");
        builder.Property(p => p.FechaModificacion).HasColumnName("FEC_MODIFICACION");
        builder.Property(p => p.CodigoUsuario).HasColumnName("COD_USUARIO");

        builder.HasOne(c => c.TipoBiometria).WithMany().HasForeignKey(c => c.NumeroTipoBiometria).IsRequired();
        builder.HasOne(c => c.AfiliacionTokenDigital).WithMany(x => x.AfiliacionesBiometricas).HasForeignKey(c => c.NumeroAfiliacion).IsRequired();
    }
}
