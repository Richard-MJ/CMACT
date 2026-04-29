using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.PA;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.PA;

/// <summary>
/// Clase que representa el mapeo de la clase de Dominio Provincia de la tabla PA_PROVINCIA_INEI
/// </summary>
public class ProvinciaConfiguracion : IEntityTypeConfiguration<Provincia>
{
   public void Configure(EntityTypeBuilder<Provincia> builder)
    {
        builder.ToTable("PA_PROVINCIA_INEI", "PA");
        builder.HasKey(k => new { k.CodigoDepartamento, k.CodigoProvincia });
        builder.Property(p => p.CodigoDepartamento).HasColumnName("COD_DEPARTAMENTO").IsRequired();
        builder.Property(p => p.CodigoProvincia).HasColumnName("COD_PROVINCIA").IsRequired();
        builder.Property(p => p.DescripcionProvincia).HasColumnName("NOM_PROVINCIA").IsRequired();
        builder.Property(p => p.CodigoDepartamentoReniec).HasColumnName("COD_DEPARTAMENTO_RENIEC");
        builder.Property(p => p.CodigoProvinciaReniec).HasColumnName("COD_PROVINCIA_RENIEC");
        builder.Ignore(p => p.CodigoEmpresa);

    }     
}
