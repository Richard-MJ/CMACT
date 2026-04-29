using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.SG
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio Usuario de la tabla SG_USUARIOS
    /// </summary>
    public class UsuarioConfiguracion : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("SG_USUARIOS", "SG");
            builder.HasKey(k => new { k.CodigoEmpresa, k.CodigoAgencia, k.CodigoUsuario });

            builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA");
            builder.Property(p => p.CodigoUsuario).HasColumnName("COD_USUARIO");
            builder.Property(p => p.CodigoAgencia).HasColumnName("COD_AGENCIA");
            builder.Property(p => p.IndicadorHabilitado).HasColumnName("IND_HABILITADO");
            builder.Property(p => p.IndicadorActivo).HasColumnName("IND_ACTIVO");
            builder.Property(p => p.CodigoCliente).HasColumnName("COD_CLIENTE").HasMaxLength(15);
            builder.Property(p => p.Clave).HasColumnName("PALABRA_PASO");
            builder.Property(p => p.NombreUsuario).HasColumnName("NOM_USUARIO");
            builder.Property(p => p.CodigoPuesto).HasColumnName("COD_PUESTO");

            builder.HasOne(c => c.Agencia).WithMany().HasForeignKey(f => new { f.CodigoEmpresa, f.CodigoAgencia });
        }
    }
}
