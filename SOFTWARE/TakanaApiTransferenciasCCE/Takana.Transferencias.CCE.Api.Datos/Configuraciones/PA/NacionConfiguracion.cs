using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.PA;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.PA;

/// <summary>
/// Clase que representa el mapeo de la clase de Dominio Nacion de la tabla PA_PAISES
/// </summary>
public class NacionConfiguracion : IEntityTypeConfiguration<Nacion>
{
    public void Configure(EntityTypeBuilder<Nacion> builder)
    {
        builder.ToTable("PA_PAISES", "PA");
        builder.HasKey(k => new { k.Codigo });

        builder.Property(p => p.Codigo).HasColumnName("COD_PAIS").HasMaxLength(5).IsRequired();
        builder.Property(p => p.Nombre).HasColumnName("NOM_PAIS").HasMaxLength(60).IsRequired();
        builder.Property(p => p.IndicadorTipo).HasColumnName("IND_TIPO").HasMaxLength(1).IsRequired();

    }     
}
