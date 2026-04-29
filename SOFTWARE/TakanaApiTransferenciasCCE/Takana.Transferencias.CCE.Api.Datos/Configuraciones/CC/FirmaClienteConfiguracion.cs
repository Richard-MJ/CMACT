using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio FirmaCliente de la tabla CC_FIRMAS_CLIENTE
    /// </summary>
    public class FirmaClienteConfiguracion : IEntityTypeConfiguration<FirmaCliente>
    {
        public void Configure(EntityTypeBuilder<FirmaCliente> builder)
        {
            builder.ToTable("CC_FIRMAS_CLIENTE", "CC");
            builder.HasKey(f => new { f.CodigoEmpresa, f.CodigoCliente, f.NumeroCuenta });

            builder.Property(f => f.CodigoEmpresa).HasColumnName("COD_EMPRESA").IsRequired().HasMaxLength(5);
            builder.Property(f => f.CodigoCliente).HasColumnName("COD_CLIENTE").HasMaxLength(15);
            builder.Property(f => f.NumeroCuenta).HasColumnName("NUM_CUENTA").HasMaxLength(15);
            builder.Property(f => f.CodigoCategoriaFirma).HasColumnName("CTG_FIRMA").HasMaxLength(1);
            builder.Property(f => f.CodigoTipoCliente).HasColumnName("TIP_CLIENTE").HasMaxLength(1);
            builder.Property(f => f.IndicadorTipoFirmante).HasColumnName("IND_TIP_FIRMANTE").HasMaxLength(1);
            builder.Property(f => f.IndicadorTipoFirma).HasColumnName("IND_TIP_FIRMA").HasMaxLength(1);
            builder.Property(p => p.IndicadorCeroPapel).HasColumnName("IND_CERO_PAPEL").HasMaxLength(2);

            builder.HasOne(f => f.Cliente).WithMany().HasForeignKey(f => new { f.CodigoEmpresa, f.CodigoCliente });
            builder.HasOne(f => f.Cuenta).WithMany(c => c.Firmas).HasForeignKey(f => new { f.CodigoEmpresa, f.NumeroCuenta });
        }
    }
}