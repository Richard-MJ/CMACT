using AutorizadorCanales.Domain.Entidades.CL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.CL;

public class ClienteApiConfiguracion : IEntityTypeConfiguration<ClienteApi>
{
    public void Configure(EntityTypeBuilder<ClienteApi> builder)
    {
        ConfigurarClienteApi(builder);
    }

    private static void ConfigurarClienteApi(EntityTypeBuilder<ClienteApi> builder)
    {
        builder.ToTable("CL_API_USUARIOS", "CL");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.FechaPrimerIntentoFallido).HasColumnName("FEC_PRIMER_INTENTO_FAL");
        builder.Property(a => a.FechaFinBloqueo).HasColumnName("FEC_FIN_BLOQUEO");
        builder.Property(p => p.NumeroTarjeta).HasColumnName("NUM_TARJETA").HasPrecision(16, 0);
        builder.Property(a => a.CodigoUsuarioSaf).HasColumnName("COD_USUARIO_SAF").HasMaxLength(15);
        builder.Property(a => a.DescripcionMotivoFallo).HasColumnName("DESC_MOTIVO_FALLO").HasMaxLength(150);
        builder.Property(a => a.FechaRegistro).HasColumnName("FEC_REGISTRO");
        builder.Property(a => a.FechaModificacion).HasColumnName("FEC_MODIFICACION");
        builder.Property(m => m.FechaRegistroIntentoFallidoSms).HasColumnName("FEC_REGISTRO_INTENTO_FALLIDO");
        builder.Property(a => a.FechaFinBloqueoSms).HasColumnName("FEC_FIN_BLOQUEO_SMS");

        builder.Property(a => a.Id).HasColumnName("ID").IsRequired().ValueGeneratedOnAdd();
        builder.Property(a => a.IdVisual).HasColumnName("ID_VISUAL").IsRequired().HasMaxLength(128);
        builder.Property(a => a.IdSistemaCliente).HasColumnName("ID_AUDIENCIA");
        builder.Property(a => a.CodigoEmpresa).HasColumnName("COD_EMPRESA");
        builder.Property(a => a.CodigoCliente).HasColumnName("COD_CLIENTE");
        builder.Property(a => a.IndicadorEstado).HasColumnName("IND_ESTADO");
        builder.Property(a => a.NumeroIntentosFallidos).HasColumnName("NUM_INTENTO_FALLIDO");
        builder.Property(a => a.IndicadorBloqueoSms).HasColumnName("IND_BLOQUEO_SMS");
        builder.Property(a => a.NumeroIntentoFallidoSms).HasColumnName("NUM_INTENTO_FALLIDO_SMS");

        builder.HasOne(a => a.SistemaCliente)
            .WithMany()
            .HasForeignKey(a => a.IdSistemaCliente);
    }
}
