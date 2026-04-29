using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF;
/// <summary>
/// Clase que representa el mapeo de la clase de Dominio ParametroPorEmpresa de la tabla CF_PARAMETROS_X_EMPRESA
/// </summary>
public class ParametroPorEmpresaConfiguracion : IEntityTypeConfiguration<ParametroPorEmpresa>
{
    public void Configure(EntityTypeBuilder<ParametroPorEmpresa> builder)
    {
        builder.ToTable("CF_PARAMETROS_X_EMPRESA", "CF");
        builder.HasKey(m => new { m.CodigoEmpresa, m.CodigoSistema, m.CodigoParametro });

        builder.Property(m => m.CodigoEmpresa).HasColumnName("COD_EMPRESA");
        builder.Property(m => m.CodigoSistema).HasColumnName("COD_SISTEMA");
        builder.Property(m => m.CodigoParametro).HasColumnName("COD_PARAMETRO");
        builder.Property(m => m.DescripcionParametro).HasColumnName("DES_PARAMETRO");
        builder.Property(m => m.ValorParametro).HasColumnName("VAL_PARAMETRO");
    }
}
