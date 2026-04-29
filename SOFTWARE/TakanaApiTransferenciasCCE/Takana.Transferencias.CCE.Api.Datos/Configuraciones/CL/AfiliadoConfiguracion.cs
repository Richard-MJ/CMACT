using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CL
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio Afiliado de la tabla CL_AFILIACIONES
    /// </summary>
    public class AfiliadoConfiguracion : IEntityTypeConfiguration<Afiliado>
    {
        public void Configure(EntityTypeBuilder<Afiliado> builder)
        {
            builder.ToTable("CL_AFILIACIONES", "CL");
            builder.HasKey(m => new { m.NumeroAfiliado });
            builder.Property(m => m.NumeroAfiliado).HasColumnName("NUM_AFILIACION").IsRequired();
            builder.Property(m => m.FechaAfiliacion).HasColumnName("FEC_AFILIACION");
            builder.Property(m => m.NivelAfiliacion).HasColumnName("NIVEL_AFILIACION");
            builder.Property(m => m.CodigoEmpresa).HasColumnName("COD_EMPRESA");
            builder.Property(m => m.CodigoCliente).HasColumnName("COD_CLIENTE");
            builder.Property(m => m.NumeroTarjeta).HasColumnName("NUM_TARJETA");
            builder.Property(m => m.CodigoAgencia).HasColumnName("COD_AGENCIA");
            builder.Property(m => m.CodigoUsuario).HasColumnName("COD_USUARIO");
            builder.Property(m => m.FechaRegistra).HasColumnName("FEC_REGISTRO");
            builder.Property(m => m.IndicadorActivo).HasColumnName("IND_ACTIVO");
            builder.Property(m => m.NumeroMovimiento).HasColumnName("NUM_MOVIMIENTO").IsRequired();

            builder.HasOne(m => m.Cliente)
                .WithMany(m => m.Afiliaciones)
                .HasForeignKey(m => new { m.CodigoEmpresa, m.CodigoCliente });
        }
    }
}
