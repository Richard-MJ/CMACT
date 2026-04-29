using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CL;

/// <summary>
/// Clase que representa el mapeo de la clase de Dominio TipoDocumento de la tabla CL_TIPOS_ID
/// </summary>
public class TipoDocumentoConfiguracion : IEntityTypeConfiguration<TipoDocumento>
{
   public void Configure(EntityTypeBuilder<TipoDocumento> builder)
    {
        builder.ToTable("CL_TIPOS_ID", "CL");
        builder.HasKey(k =>  k.CodigoTipoDocumento );

        builder.Property(p => p.CodigoTipoDocumento).HasColumnName("COD_TIPO_ID");
        builder.Property(p => p.DescripcionTipoDocumento).HasColumnName("DES_TIPO_ID");   
        builder.Property(p => p.CodigoTipoDocumentoInmediataCce).HasColumnName("COD_TIPO_ID_CCE_INMEDIATA");
        builder.Property(p => p.IndicadorPrioridadPersonaNatural).HasColumnName("IND_PRIORIDAD_CARTILLA_PN");      
        builder.Property(p => p.IndicadorPrioridadPersonaJuridica).HasColumnName("IND_PRIORIDAD_CARTILLA_PJ");
        builder.Property(p => p.IndicadorPersonaNatural).HasColumnName("IND_NATURAL");
        builder.Property(p => p.IndicadorPersona).HasColumnName("IND_PERSONA");
        builder.Property(p => p.IndicadorAplicaCCE).HasColumnName("IND_APLICA_CCE");
        builder.Property(p => p.LongitudDocumentoCCE).HasColumnName("LONGITUD_TIP_DOC_EQUIVALENTE_CAMARA");
    }     
}
