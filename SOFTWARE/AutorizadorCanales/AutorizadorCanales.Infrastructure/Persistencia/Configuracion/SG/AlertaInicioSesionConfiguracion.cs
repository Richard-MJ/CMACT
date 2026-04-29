using AutorizadorCanales.Domain.Entidades.SG;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.SG;

public class AlertaInicioSesionConfiguracion : IEntityTypeConfiguration<AlertaInicioSesion>
{
    public void Configure(EntityTypeBuilder<AlertaInicioSesion> builder)
    {
        ConfigurarAlertaInicioSesion(builder);
    }

    private static void ConfigurarAlertaInicioSesion(EntityTypeBuilder<AlertaInicioSesion> builder)
    {
        builder.ToTable("SG_ALERTAS_INICIO_SESION", "SG");
        builder.HasKey(m => new { m.NumeroIdentificador });

        builder.Property(m => m.NumeroIdentificador).HasColumnName("NUM_IDENTIFICADOR").IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(m => m.NumeroTarjeta).HasColumnName("NUM_TARJETA");
        builder.Property(m => m.IndicadorEstado).HasColumnName("IND_ESTADO");
        builder.Property(m => m.IndicadorCanal).HasColumnName("IND_CANAL");
        builder.Property(m => m.DireccionIp).HasColumnName("DIRECCION_IP");
        builder.Property(m => m.IdRegistroDispositivo).HasColumnName("ID_REGISTRO_DISPOSITIVO");
        builder.Property(m => m.SistemaOperativo).HasColumnName("SISTEMA_OPERATIVO");
        builder.Property(m => m.Navegador).HasColumnName("NAVEGADOR");
        builder.Property(m => m.Ubicacion).HasColumnName("UBICACION_GEO");
        builder.Property(m => m.FechaRegistro).HasColumnName("FEC_REGISTRO");
        builder.Property(m => m.ModeloDispositivo).HasColumnName("MODELO_DISPOSITIVO");
        builder.Property(m => m.NumeroMovimiento).HasColumnName("NUM_MOVIMIENTO");
    }
}
