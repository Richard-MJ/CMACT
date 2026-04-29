using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CL;

/// <summary>
/// Clase que representa el mapeo de la clase de Dominio DireccionCliente de la tabla CL_DIR_CLIENTES
/// </summary>
public class DireccionClienteConfiguracion : IEntityTypeConfiguration<DireccionCliente>
{
   public void Configure(EntityTypeBuilder<DireccionCliente> builder)
    {
        builder.ToTable("CL_DIR_CLIENTES", "CL");
        builder.HasKey(k => new { k.CodigoEmpresa, k.CodigoCliente, k.CodigoDireccion });

        builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA").IsRequired();
        builder.Property(p => p.CodigoCliente).HasColumnName("COD_CLIENTE").IsRequired();
        builder.Property(p => p.CodigoDireccion).HasColumnName("COD_DIRECCION").IsRequired();
        builder.Property(p => p.CodigoTipoDireccion).HasColumnName("TIP_DIRECCION")
               .HasColumnType("varchar").IsRequired();
        builder.Property(p => p.DetalleDireccion).HasColumnName("DET_DIRECCION").IsRequired();
        builder.Property(p => p.CodigoPais).HasColumnName("COD_PAIS").IsRequired();
        builder.Property(p => p.CodigoProvincia).HasColumnName("COD_PROVINCIA");
        builder.Property(p => p.CodigoCanton).HasColumnName("COD_CANTON");
        builder.Property(p => p.CodigoDistrito).HasColumnName("COD_DISTRITO");
        builder.Property(p => p.CodigoVia).HasColumnName("COD_VIA");
        builder.Property(p => p.CodigoZona).HasColumnName("COD_ZONA");
        builder.Property(p => p.NombreVia).HasColumnName("NOM_VIA");
        builder.Property(p => p.NombreZona).HasColumnName("NOM_ZONA");
        builder.Property(p => p.Referencia).HasColumnName("REFERENCIA");

        builder.HasOne(x => x.Cliente).WithMany(x => x.Direcciones).HasForeignKey(x => new { x.CodigoEmpresa, x.CodigoCliente });
        builder.HasOne(x => x.Distrito).WithMany().HasForeignKey(x => new { x.CodigoPais, x.CodigoProvincia, x.CodigoCanton });
        builder.HasOne(x => x.Provincia).WithMany().HasForeignKey(x => new { x.CodigoPais, x.CodigoProvincia });
        builder.HasOne(x => x.TipoDireccion).WithMany().HasForeignKey(x => x.CodigoTipoDireccion);
    }     
}
