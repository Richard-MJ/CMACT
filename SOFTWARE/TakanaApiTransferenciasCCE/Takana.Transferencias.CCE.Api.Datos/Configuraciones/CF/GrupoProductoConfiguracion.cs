using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio Producto de la tabla CF_PRODUCTOS
    /// </summary>
    public class GrupoProductoConfiguracion : IEntityTypeConfiguration<GrupoProducto>
    {
        public void Configure(EntityTypeBuilder<GrupoProducto> builder)
        {
            builder.ToTable("CF_PRODUCTOS_OPER_INT", "CF");
            builder.HasKey(p => new { p.IdProducto });

            builder.Property(p => p.IdProducto).HasColumnName("ID_PRODUCTO");
            builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA");
            builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA");
            builder.Property(p => p.CodigoProducto).HasColumnName("COD_PRODUCTO");
            builder.Property(p => p.IndicadorGrupo).HasColumnName("IND_GRUPO");
            builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO");
        }
    }
}
