using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio CuentaAfiliada de la tabla CC_AFILIACION_SERVICIOS_CUENTAS
    /// </summary>
    public class CuentaAfiliadaConfiguracion : IEntityTypeConfiguration<CuentaAfiliada>
    {
        public void Configure(EntityTypeBuilder<CuentaAfiliada> builder)
        {
            builder.ToTable("CC_AFILIACION_SERVICIOS_CUENTAS", "CC");
            builder.HasKey(m => new { m.NumeroAfiliado, m.CodigoServicio, m.NumeroCuenta });

            builder.Property(m => m.NumeroAfiliado).HasColumnName("NUM_AFILIACION").IsRequired();
            builder.Property(m => m.CodigoEmpresa).HasColumnName("COD_EMPRESA");
            builder.Property(m => m.CodigoServicio).HasColumnName("COD_SERVICIO");
            builder.Property(m => m.NumeroCuenta).HasColumnName("NUM_CUENTA");
            builder.Property(m => m.CodigoAgenciaCuenta).HasColumnName("COD_AGENCIA_CTA");

            builder.HasOne(c => c.AfiliadoServicio)
                .WithMany(s => s.CuentasAfiliadas)
                .HasForeignKey(c => new { c.NumeroAfiliado, c.CodigoServicio });
            builder.HasOne(c => c.Cuenta).WithMany().HasForeignKey(c => new { c.CodigoEmpresa, c.NumeroCuenta });
        }
    }
}
