using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF;
/// <summary>
/// Clase que representa el mapeo de la clase de Dominio SeriesPorEmpresa de la tabla CF_SERIES_X_EMPRESA
/// </summary>
public class SeriesPorEmpresaConfiguracion : IEntityTypeConfiguration<SeriesPorEmpresa>
{
    public void Configure(EntityTypeBuilder<SeriesPorEmpresa> builder)
    {
        builder.ToTable("CF_SERIES_X_EMPRESA", "CF");
        builder.HasKey(m => new { m.CodigoSerie });

        builder.Property(m => m.CodigoEmpresa).HasColumnName("COD_EMPRESA");
        builder.Property(m => m.CodigoSistema).HasColumnName("COD_SISTEMA");
        builder.Property(m => m.CodigoSerie).HasColumnName("COD_SERIE");
        builder.Property(m => m.DescripcionSerie).HasColumnName("DES_SERIE");
        builder.Property(m => m.ValorSiguiente).HasColumnName("VAL_SIGUIENTE");
    }
}
