using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.PA;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.PA;

/// <summary>
/// Clase que representa el mapeo de la clase de Dominio Cantones de la tabla PA_DISTRITO_INEI
/// </summary>
public class CantonesConfiguracion : IEntityTypeConfiguration<Cantones>
{
    public void Configure(EntityTypeBuilder<Cantones> builder)
    {
        builder.ToTable("PA_DISTRITO_INEI", "PA");
        builder.HasKey(k => new { k.CodigoDepartamento, k.CodigoProvincia, k.CodigoDistrito });
        builder.Property(p => p.CodigoDepartamento).HasColumnName("COD_DEPARTAMENTO").IsRequired();
        builder.Property(p => p.CodigoProvincia).HasColumnName("COD_PROVINCIA").IsRequired();
        builder.Property(p => p.CodigoDistrito).HasColumnName("COD_DISTRITO").IsRequired();
        builder.Property(p => p.DescripcionCanton).HasColumnName("NOM_DISTRITO").IsRequired();
        builder.Property(p => p.CodigoDepartamentoReniec).HasColumnName("COD_DEPARTAMENTO_RENIEC");
        builder.Property(p => p.CodigoProvinciaReniec).HasColumnName("COD_PROVINCIA_RENIEC");
        builder.Property(p => p.CodigoDistritoReniec).HasColumnName("COD_DISTRITO_RENIEC");
        builder.Property(p => p.CodigoUbigeo).HasColumnName("COD_UBIGEO");
    }     
}
