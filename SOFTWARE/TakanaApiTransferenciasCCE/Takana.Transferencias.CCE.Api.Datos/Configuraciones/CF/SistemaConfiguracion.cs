using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio Sistema de la tabla CF_SISTEMAS
    /// </summary>
    public class SistemaConfiguracion : IEntityTypeConfiguration<Sistema>
    {
        public void Configure(EntityTypeBuilder<Sistema> builder)
        {
            builder.ToTable("CF_SISTEMAS", "CF");
            builder.HasKey(k => new { k.CodigoSistema });

            builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA").IsRequired();
            builder.Property(p => p.DescripcionSistema).HasColumnName("DES_SISTEMA").IsRequired();
            builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO").IsRequired();
        }
    }
}