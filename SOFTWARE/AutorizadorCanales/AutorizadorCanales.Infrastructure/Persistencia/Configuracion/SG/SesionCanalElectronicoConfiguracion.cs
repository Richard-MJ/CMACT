using AutorizadorCanales.Domain.Entidades.SG;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.SG;

public class SesionCanalElectronicoConfiguracion : IEntityTypeConfiguration<SesionCanalElectronico>
{
    public void Configure(EntityTypeBuilder<SesionCanalElectronico> builder)
    {
        ConfigurarSesionCanalElectronico(builder);
    }

    private void ConfigurarSesionCanalElectronico(EntityTypeBuilder<SesionCanalElectronico> builder)
    {
        builder.ToTable("SG_SESIONES_CANALES_ELECTRONICOS", "SG");

        builder.HasKey(k => k.IdSesion);

        builder.Property(p => p.IdSesion)
            .HasColumnName("ID_SESION")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(p => p.NumeroTarjeta).HasColumnName("NUM_TARJETA");
        builder.Property(p => p.CodigoCliente).HasColumnName("COD_CLIENTE");
        builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA");
        builder.Property(p => p.DispositivoId).HasColumnName("DISPOSITIVO_ID");
        builder.Property(p => p.DireccionIp).HasColumnName("DIRECCION_IP");
        builder.Property(p => p.SistemaOperativo).HasColumnName("SISTEMA_OPERATIVO");
        builder.Property(p => p.Navegador).HasColumnName("NAVEGADOR");
        builder.Property(p => p.ModeloDispositivo).HasColumnName("MODELO_DISPOSITIVO");
        builder.Property(p => p.TokenGuid).HasColumnName("ID_TOKEN");
        builder.Property(p => p.IdConexion).HasColumnName("ID_CONEXION");
        builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO");
        builder.Property(p => p.IndicadorCanal).HasColumnName("IND_CANAL");
        builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO");
        builder.Property(p => p.FechaModificacion).HasColumnName("FEC_MODIFICACION");

        builder.HasOne(d => d.DispositivoCanalElectronico)
            .WithMany()
            .HasForeignKey(d => new { d.DispositivoId, d.NumeroTarjeta, d.CodigoCliente})
            .IsRequired();

        builder.HasOne(c => c.Cliente)
            .WithMany()
            .HasForeignKey(c => new { c.CodigoEmpresa, c.CodigoCliente })
            .IsRequired();
    }
}
