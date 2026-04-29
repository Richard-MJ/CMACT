using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio CuentaAfiliadaHistorica de la tabla CC_AFILIACION_SERVICIOS_CUENTAS_HIST
    /// </summary>
    public class CuentaAfiliadaHistoricaConfiguracion : IEntityTypeConfiguration<CuentaAfiliadaHistorica>
    {
        public void Configure(EntityTypeBuilder<CuentaAfiliadaHistorica> builder)
        {

            builder.ToTable("CC_AFILIACION_SERVICIOS_CUENTAS_HIST", "CC");
            builder.HasKey(m => new { m.NumeroAfiliado, m.CodigoServicio, m.NumeroCuenta, m.FechaDesafiliacion });

            builder.Property(m => m.NumeroAfiliado).HasColumnName("NUM_AFILIACION").IsRequired();
            builder.Property(m => m.CodigoEmpresa).HasColumnName("COD_EMPRESA");
            builder.Property(m => m.CodigoServicio).HasColumnName("COD_SERVICIO");
            builder.Property(m => m.NumeroCuenta).HasColumnName("NUM_CUENTA");
            builder.Property(m => m.CodigoAgenciaCuenta).HasColumnName("COD_AGENCIA_CTA");
            builder.Property(m => m.FechaDesafiliacion).HasColumnName("FEC_DESAFILIACION");
            builder.Property(m => m.CodigoUsuarioDesafiliacion).HasColumnName("COD_USUARIO_DESAFILIACION");
            builder.Property(m => m.CodigoAgenciaDesafiliacion).HasColumnName("COD_AGENCIA_DESAFILIACION");

            builder.HasOne(c => c.AfiliadoServicio).WithMany().HasForeignKey(c => new { c.NumeroAfiliado, c.CodigoServicio });
            builder.HasOne(c => c.Cuenta).WithMany().HasForeignKey(c => new { c.CodigoEmpresa, c.NumeroCuenta });
        }
    }
}
