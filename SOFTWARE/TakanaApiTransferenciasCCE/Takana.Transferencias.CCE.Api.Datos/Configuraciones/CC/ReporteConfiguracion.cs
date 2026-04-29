using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio Reportes con la tabla CC_TIN_INMEDIATA_REPORTES
    /// </summary>
    public class ReporteConfiguracion : IEntityTypeConfiguration<Reporte>
    {
        public void Configure(EntityTypeBuilder<Reporte> builder)
        {
            builder.ToTable("CC_TIN_INMEDIATA_REPORTES", "CC");
            builder.HasKey(m => new { m.Id });

            builder.Property(g => g.Id).HasColumnName("ID_REPORTE");
            builder.Property(g => g.IdTipoReporte).HasColumnName("ID_TIPO_REPORTE");
            builder.Property(g => g.Nombre).HasColumnName("NOM_REPORTE");
            builder.Property(g => g.Contenido).HasColumnName("DES_CONTENIDO_REPORTE");
            builder.Property(g => g.IdPeriodo).HasColumnName("ID_PERIODO");
            builder.Property(g => g.IndicadorSubidoSFTP).HasColumnName("IND_SUBIDO_SFTP");
            builder.Property(g => g.CodigoUsuarioRegistro).HasColumnName("COD_USUARIO_REGISTRO");
            builder.Property(g => g.FechaReporte).HasColumnName("FEC_REPORTE");
            builder.Property(g => g.FechaRegistro).HasColumnName("FEC_REGISTRO");
            builder.Property(g => g.FechaSubidoSFTP).HasColumnName("FEC_SUBIDO_SFTP");

            builder.HasOne(c => c.TipoReporte).WithMany().HasForeignKey(c => new { c.IdTipoReporte });
            builder.HasOne(c => c.Periodo).WithMany().HasForeignKey(c => new { c.IdPeriodo });
        }
    }
}