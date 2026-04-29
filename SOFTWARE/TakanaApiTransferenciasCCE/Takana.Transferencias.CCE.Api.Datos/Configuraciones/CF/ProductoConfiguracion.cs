using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio Producto de la tabla CF_PRODUCTOS
    /// </summary>
    public class ProductoConfiguracion : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("CF_PRODUCTOS", "CF");
            builder.HasKey(p => new { p.CodigoEmpresa, p.CodigoSistema, p.CodigoProducto });

            builder.Property(m => m.CodigoEmpresa).HasColumnName("COD_EMPRESA").IsRequired();
            builder.Property(m => m.CodigoSistema).HasColumnName("COD_SISTEMA").IsRequired();
            builder.Property(m => m.CodigoProducto).HasColumnName("COD_PRODUCTO").IsRequired();
            builder.Property(m => m.NombreProducto).HasColumnName("NOM_PRODUCTO");
            builder.Property(m => m.CodigoTipoProductoReporte).HasColumnName("COD_TIP_PRODUCTO_RPT");
            builder.Property(m => m.CodigoMoneda).HasColumnName("COD_MONEDA");
            builder.Property(m => m.CodigoProductoAsociado).HasColumnName("COD_PROD_ASOCIADO");
            builder.Property(m => m.CodigoEstado).HasColumnName("IND_ESTADO");
            builder.Property(m => m.DescripcionProducto).HasColumnName("DES_PRODUCTO");
            builder.Property(m => m.NombreComercial).HasColumnName("NOM_COMERCIAL");

            builder.HasOne(p => p.Moneda).WithMany().HasForeignKey(c => new { c.CodigoMoneda });
        }
    }
}
