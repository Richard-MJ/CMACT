using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio DirectorioInteroperabilidad de la tabla CC_INTEROPERABILIDAD_DIRECTORIO
    /// </summary>
    internal class DirectorioInteroperabilidadConfiguracion : IEntityTypeConfiguration<DirectorioInteroperabilidad>
    {
        public void Configure(EntityTypeBuilder<DirectorioInteroperabilidad> builder)
        {
            builder.ToTable("CC_INTEROPERABILIDAD_DIRECTORIO", "CC");
            builder.HasKey(k => new { k.CodigoDirectorio });
            
            builder.Property(p => p.CodigoDirectorio).HasColumnName("COD_DIRECTORIO");
            builder.Property(p => p.NombreDirectorio).HasColumnName("DES_NOMBRE");
            builder.Property(p => p.Estado).HasColumnName("IND_ESTADO");
            builder.Property(p => p.CodigoUsuarioRegistro).HasColumnName("COD_USUARIO_REGISTRO");
            builder.Property(p => p.CodigoUsuarioModifico).HasColumnName("COD_USUARIO_MODIFICO");
            builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO");
            builder.Property(p => p.FechaModifico).HasColumnName("FEC_MODIFICO");
        }
    }
}
