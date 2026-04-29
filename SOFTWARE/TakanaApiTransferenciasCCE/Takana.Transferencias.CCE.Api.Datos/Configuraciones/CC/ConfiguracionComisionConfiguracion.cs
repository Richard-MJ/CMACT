using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio ConfiguracionComision de la tabla CC_CONFIGURACION_COMISION_TRANSACCION_ENCA
    /// </summary>
    internal class ConfiguracionComisionConfiguracion : IEntityTypeConfiguration<ConfiguracionComision>
    {
        public void Configure(EntityTypeBuilder<ConfiguracionComision> builder)
        {
            builder.ToTable("CC_CONFIGURACION_COMISION_TRANSACCION_ENCA", "CC");
            builder.HasKey(k => new { k.CodigoConfiguracion });
            
            builder.Property(p => p.CodigoConfiguracion).HasColumnName("ID_CONFIGURACION");
            builder.Property(p => p.CodigoComision).HasColumnName("COD_COMISION");
            builder.Property(p => p.CodigoTipoTransaccion).HasColumnName("TIP_TRANSACCION");
            builder.Property(p => p.CodigoSubTipoTransaccion).HasColumnName("SUBTIP_TRANSAC");
            builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA");
            builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO");
            builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA");
            builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO");
            builder.Property(p => p.CodigoUsuarioRegistro).HasColumnName("COD_USUARIO");
            builder.Property(p => p.FechaModificacion).HasColumnName("FEC_MODIFICACION_ULT");
            builder.Property(p => p.CodigoUsuarioModificacion).HasColumnName("COD_USUARIO_ULT");

            builder.HasOne(c => c.SubTipoTransaccion).WithMany().HasForeignKey(c => 
                new { c.CodigoEmpresa, c.CodigoSistema, c.CodigoTipoTransaccion, c.CodigoSubTipoTransaccion});

            builder.HasMany(p => p.ConfiguracionProductos).WithOne(x => x.ConfiguracionTransaccion).HasForeignKey(c => c.CodigoConfiguracion);
            builder.HasMany(p => p.ConfiguracionMonedas).WithOne().HasForeignKey(c =>c.CodigoConfiguracion);
        }
    }
}
