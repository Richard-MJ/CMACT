using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio ConfiguracionComision de la tabla CC_CONFIGURACION_COMISION_TRANSACCION_ENCA
    /// </summary>
    internal class ConfiguracionComisionMonedaConfiguracion : IEntityTypeConfiguration<ConfiguracionComisionMoneda>
    {
        public void Configure(EntityTypeBuilder<ConfiguracionComisionMoneda> builder)
        {
            builder.ToTable("CC_CONFIGURACION_COMISION_DETA_MONEDA", "CC");
            builder.HasKey(k => new { k.CodigoConfiguracionMoneda });

            builder.Property(p => p.CodigoConfiguracionMoneda).HasColumnName("ID_CONFIGURACION_DETA_MONEDA");
            builder.Property(p => p.CodigoConfiguracion).HasColumnName("ID_CONFIGURACION");
            builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA");
            builder.Property(p => p.CodigoMoneda).HasColumnName("COD_MONEDA");
            builder.Property(p => p.MontoBase).HasColumnName("MON_BASE");
            builder.Property(p => p.MontoFijo).HasColumnName("MON_FIJO");
            builder.Property(p => p.MontoMaximo).HasColumnName("MON_MAX");
            builder.Property(p => p.PorcentajeTransaccion).HasColumnName("POR_TRANSAC");
            builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO");
            builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO");
            builder.Property(p => p.CodigoUsuarioRegistro).HasColumnName("COD_USUARIO");
            builder.Property(p => p.FechaModificacion).HasColumnName("FEC_MODIFICACION_ULT");
            builder.Property(p => p.CodigoUsuarioModificacion).HasColumnName("COD_USUARIO_ULT");

            builder.HasOne(c => c.Moneda).WithMany().HasForeignKey(c => new { c.CodigoMoneda });
        }
    }
}
