using AutorizadorCanales.Domain.Entidades.CL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.CL;

public class TipoDocumentoConfiguracion : IEntityTypeConfiguration<TipoDocumento>
{
    public void Configure(EntityTypeBuilder<TipoDocumento> builder)
    {
        ConfigurarTipoDocumento(builder);
    }

    private static void ConfigurarTipoDocumento(EntityTypeBuilder<TipoDocumento> builder)
    {
        builder.ToTable("CL_TIPOS_ID", "CL");
        builder.HasKey(k => new { k.CodigoEmpresa, k.CodigoTipoDocumento });
        builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA").HasMaxLength(5);
        builder.Property(p => p.CodigoTipoDocumento).HasColumnName("COD_TIPO_ID").HasMaxLength(5);
        builder.Property(p => p.DescripcionTipoDocumento).HasColumnName("DES_TIPO_ID").HasMaxLength(60);
        builder.Property(p => p.Mascara).HasColumnName("MASCARA");
        builder.Property(p => p.IndicadorPrioridad).HasColumnName("NUM_PRIORIDAD");
        builder.Property(p => p.IndicadorUsoEnLavado).HasColumnName("IND_LAVADO").HasMaxLength(1);
        builder.Property(p => p.IndicadorPersona).HasColumnName("IND_PERSONA").HasMaxLength(1);
        builder.Property(p => p.IndicadorPrioridadCce).HasColumnName("IND_PRIORIDAD_CCE");
        builder.Property(p => p.IndicadorHomeBankingAppCanales).HasColumnName("IND_HB_APP_CANALES");
        builder.Property(p => p.CodigoTipoDocumentoEquivalenteCamara).HasColumnName("COD_TIPO_ID_EQUIVALENTE_CAMARA");
        builder.Property(p => p.LongitudTipoDocumentoEquivalenteCamara).HasColumnName("LONGITUD_TIP_DOC_EQUIVALENTE_CAMARA");
        
        builder.Property(p => p.IndicadorPersonaNatural).HasColumnName("IND_NATURAL");
        builder.Property(p => p.CodigoTipoDocumentoCce).HasColumnName("COD_TIPO_ID_CCE").HasMaxLength(1);
        builder.Property(p => p.IndicadorPrioridadPersonaNatural).HasColumnName("IND_PRIORIDAD_CARTILLA_PN");
        builder.Property(p => p.IndicadorPrioridadPersonaJuridica).HasColumnName("IND_PRIORIDAD_CARTILLA_PJ");
        builder.Property(p => p.CodigoTipoUnibanca).HasColumnName("COD_TIP_UNIBANCA").IsRequired(false);
        builder.Property(p => p.CodigoTipoMinisterioTrabajo).HasColumnName("COD_TIPO_ID_MINTRA");
        builder.Property(p => p.IndicadorMostrarBusquedaCliente).HasColumnName("IND_MOSTRAR_BUSQUEDA");
        builder.Property(p => p.IndicadorEmisionGiros).HasColumnName("IND_GIRO");
    }
}
