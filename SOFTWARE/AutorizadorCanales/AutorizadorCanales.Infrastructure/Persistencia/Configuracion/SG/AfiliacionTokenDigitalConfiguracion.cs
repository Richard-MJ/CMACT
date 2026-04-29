using AutorizadorCanales.Domain.Entidades.SG;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.SG;

public class AfiliacionTokenDigitalConfiguracion : IEntityTypeConfiguration<AfiliacionTokenDigital>
{
    public void Configure(EntityTypeBuilder<AfiliacionTokenDigital> builder)
    {
        ConfigurarAfiliacionTokenDigital(builder);
    }

    private void ConfigurarAfiliacionTokenDigital(EntityTypeBuilder<AfiliacionTokenDigital> builder)
    {
        builder.ToTable("SG_AFILIACIONES_TOKEN_DIGITAL", "SG");
        builder.HasKey(m => new { m.IdAfilacion });

        builder.Property(m => m.IdAfilacion).HasColumnName("NUM_AFILIACION")
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property(m => m.CodigoEmpresa).HasColumnName("COD_EMPRESA");
        builder.Property(m => m.NumeroTarjeta).HasColumnName("NUM_TARJETA");
        builder.Property(m => m.IdentificadorDispositivo).HasColumnName("UUID_DISPOSITIVO");

        builder.Property(m => m.ModeloDispositivo).HasColumnName("MODELO_DISPOSITIVO");
        builder.Property(m => m.FabricanteDispositivo).HasColumnName("MARCA_DISPOSITIVO");
        builder.Property(m => m.NombreDispositivo).HasColumnName("NOMBRE_DISPOSITIVO");
        builder.Property(m => m.PlataformaDispositivo).HasColumnName("PLATAFORMA_DISPOSITIVO");
        builder.Property(m => m.IdiomaDispositivo).HasColumnName("CATEGORIA_DISPOSITIVO");
        builder.Property(m => m.TipoDispositivo).HasColumnName("TIPO_DISPOSITIVO");
        builder.Property(m => m.FechaCreacion).HasColumnName("FECHA_CREACION");
        builder.Property(m => m.FechaModificacion).HasColumnName("ULTIMA_FECHA_ACTUALIZACION");
        builder.Property(m => m.EstadoDispositivo).HasColumnName("ESTADO_DISPOSITIVO");
    }
}
