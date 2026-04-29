using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.TJ;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.TJ
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio Tarjeta de la tabla TJ_TARJETAS
    /// </summary>
    public class TarjetaConfiguracion : IEntityTypeConfiguration<Tarjeta>
    {
        public void Configure(EntityTypeBuilder<Tarjeta> builder)
        {
            builder.ToTable("TJ_TARJETAS", "TJ");
            builder.HasKey(m => new { m.NumeroTarjeta });

            builder.Property(m => m.NumeroTarjeta).HasColumnName("NUM_TARJETA");
            builder.Property(m => m.CodigoAgencia).HasColumnName("COD_AGENCIA");
            builder.Property(m => m.CodigoCliente).HasColumnName("COD_CLIENTE");
            builder.Property(m => m.CodigoTipoTarjeta).HasColumnName("COD_TIPO_TARJETA");
            builder.Property(m => m.CodigoEstadoTarjeta).HasColumnName("COD_ESTADO_TAR");
            builder.Property(m => m.TipoEstadoTarjeta).HasColumnName("TIP_ESTADO_TAR");
            builder.Property(m => m.FechaVencimiento).HasColumnName("FEC_VENCIMIENTO");
            builder.Property(m => m.CodigoEmpresa).HasColumnName("COD_EMPRESA");

            builder.HasOne(t => t.Duenio).WithMany().HasForeignKey(t => new { t.CodigoEmpresa, t.CodigoCliente });
        }
    }
}
