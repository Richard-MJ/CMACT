using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio Reportes con la tabla CC_TIN_INMEDIATA_REPORTES
    /// </summary>
    public class TipoReporteConfiguracion : IEntityTypeConfiguration<TipoReporte>
    {
        public void Configure(EntityTypeBuilder<TipoReporte> builder)
        {
            builder.ToTable("CF_TIN_INMEDIATA_TIPO_REPORTE", "CF");
            builder.HasKey(m => new { m.Id });

            builder.Property(g => g.Id).HasColumnName("ID_TIPO_REPORTE");
            builder.Property(g => g.Descripcion).HasColumnName("DES_TIPO_REPORTE");
            builder.Property(g => g.CodigoAnexo).HasColumnName("COD_ANEXO");
            builder.Property(g => g.TipoFrecuencia).HasColumnName("TIP_FRECUENCIA");
        }
    }
}