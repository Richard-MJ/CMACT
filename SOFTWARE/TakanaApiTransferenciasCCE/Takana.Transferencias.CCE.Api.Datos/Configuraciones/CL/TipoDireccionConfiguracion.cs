using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CL;

/// <summary>
/// Clase que representa el mapeo de la clase de Dominio TipoDireccion de la tabla CL_TIP_DIRECCIONES
/// </summary>
public class TipoDireccionConfiguracion : IEntityTypeConfiguration<TipoDireccion>
{
   public void Configure(EntityTypeBuilder<TipoDireccion> builder)
    {
        builder.ToTable("CL_TIP_DIRECCIONES", "CL");
        builder.HasKey(k => new { k.CodigoTipoDireccion });

        builder.Property(p => p.CodigoTipoDireccion).HasColumnName("COD_TIP_DIRECCION").HasColumnType("varchar").IsRequired();
        builder.Property(p => p.DescripcionTipoDireccion).HasColumnName("DES_TIP_DIRECCION").HasMaxLength(500);
        builder.Property(p => p.IndicadorPrioridadJuridica).HasColumnName("IND_PRIORIDAD_JURIDICA");
        builder.Property(p => p.IndicadorPrioridadNatural).HasColumnName("IND_PRIORIDAD_NATURAL");

    }     
}
