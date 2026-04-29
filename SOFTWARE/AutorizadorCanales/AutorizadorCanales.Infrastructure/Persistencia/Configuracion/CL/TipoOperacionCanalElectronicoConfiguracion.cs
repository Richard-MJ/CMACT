using AutorizadorCanales.Domain.Entidades.CL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.CL;

public class TipoOperacionCanalElectronicoConfiguracion : IEntityTypeConfiguration<TipoOperacionCanalElectronico>
{
    public void Configure(EntityTypeBuilder<TipoOperacionCanalElectronico> builder)
    {
        ConfigurarTipoOperacionCanalElectronico(builder);
    }

    private void ConfigurarTipoOperacionCanalElectronico(EntityTypeBuilder<TipoOperacionCanalElectronico> builder)
    {
        builder.ToTable("CL_TIPO_OPERACION_CANALES_ELECTRONICOS", "CL");
        builder.HasKey(m => new { m.IdTipoOperacionCanalElectronico });

        builder.Property(m => m.IdTipoOperacionCanalElectronico).HasColumnName("ID_TIP_OPE_CAN_ELEC")
            .IsRequired();

        builder.Property(m => m.IndicadorEstado).HasColumnName("IND_ESTADO");
        builder.Property(m => m.FechaRegistro).HasColumnName("FEC_REGISTRO");
        
        builder.Property(m => m.CodigoUsuarioRegistro).HasColumnName("COD_USUARIO_REGISTRO");
        builder.Property(m => m.DescripcionOperacion).HasColumnName("DESC_OPERACION");
    }
}
