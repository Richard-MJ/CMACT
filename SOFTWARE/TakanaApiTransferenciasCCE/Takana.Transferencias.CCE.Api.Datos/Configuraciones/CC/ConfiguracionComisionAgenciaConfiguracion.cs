using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio ConfiguracionComision de la tabla CC_CONFIGURACION_COMISION_TRANSACCION_ENCA
    /// </summary>
    internal class ConfiguracionComisionAgenciaConfiguracion : IEntityTypeConfiguration<ConfiguracionComisionAgencia>
    {
        public void Configure(EntityTypeBuilder<ConfiguracionComisionAgencia> builder)
        {
            builder.ToTable("CC_CONFIGURACION_COMISION_DETA_AGENCIA", "CC");
            builder.HasKey(k => new { k.CodigoConfiguracionAgencia });

            builder.Property(p => p.CodigoConfiguracionAgencia).HasColumnName("ID_CONFIGURACION_DETA_AGENCIA");
            builder.Property(p => p.CodigoConfiguracionProducto).HasColumnName("ID_CONFIGURACION_DETA_PRODUCTO");
            builder.Property(p => p.CodigoAgencia).HasColumnName("COD_AGENCIA");
            builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA");
            builder.Property(p => p.IndicadorAplicaOperacionesLibres).HasColumnName("IND_APLICA_OPER_LIBRES");
            builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO");
            builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO");
            builder.Property(p => p.CodigoUsuarioRegistro).HasColumnName("COD_USUARIO");
            builder.Property(p => p.FechaModificacion).HasColumnName("FEC_MODIFICACION_ULT");
            builder.Property(p => p.CodigoUsuarioModificacion).HasColumnName("COD_USUARIO_ULT");

            builder.HasOne(c => c.Agencia).WithMany().HasForeignKey(c => new { c.CodigoEmpresa, c.CodigoAgencia });
        }
    }
}
