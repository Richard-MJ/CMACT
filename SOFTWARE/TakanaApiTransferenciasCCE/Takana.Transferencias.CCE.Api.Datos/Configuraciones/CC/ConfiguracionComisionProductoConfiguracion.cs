using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio ConfiguracionComision de la tabla CC_CONFIGURACION_COMISION_TRANSACCION_ENCA
    /// </summary>
    internal class ConfiguracionComisionProductoConfiguracion : IEntityTypeConfiguration<ConfiguracionComisionProducto>
    {
        public void Configure(EntityTypeBuilder<ConfiguracionComisionProducto> builder)
        {
            builder.ToTable("CC_CONFIGURACION_COMISION_DETA_PRODUCTO", "CC");
            builder.HasKey(k => new { k.CodigoConfiguracionProducto });

            builder.Property(p => p.CodigoConfiguracionProducto).HasColumnName("ID_CONFIGURACION_DETA_PRODUCTO");
            builder.Property(p => p.CodigoConfiguracion).HasColumnName("ID_CONFIGURACION");
            builder.Property(p => p.CodigoProducto).HasColumnName("COD_PRODUCTO");
            builder.Property(p => p.NumeroOperacionesLibresSinComision).HasColumnName("NUM_OPER_LIBRES");
            builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA");
            builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA");
            builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO");
            builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO");
            builder.Property(p => p.CodigoUsuarioRegistro).HasColumnName("COD_USUARIO");
            builder.Property(p => p.FechaModificacion).HasColumnName("FEC_MODIFICACION_ULT");
            builder.Property(p => p.CodigoUsuarioModificacion).HasColumnName("COD_USUARIO_ULT");

            builder.HasOne(c => c.Producto).WithMany().HasForeignKey(c => new { c.CodigoEmpresa, c.CodigoSistema, c.CodigoProducto });

            builder.HasMany(p => p.ConfiguracionAgencias).WithOne().HasForeignKey(c => new { c.CodigoConfiguracionProducto });            
        }
    }
}
