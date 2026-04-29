using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio EntidadFinancieraCCEInmediata de la tabla BA_ENTIDAD_FINANCIERA_CCE_INMEDIATA
    /// </summary>
    public class EntidadFinancieraInmediataConfiguracion : IEntityTypeConfiguration<EntidadFinancieraInmediata>
    {
        public void Configure(EntityTypeBuilder<EntidadFinancieraInmediata> builder)
        {
            builder.ToTable("BA_ENTIDAD_FINANCIERA_CCE_INMEDIATA", "BA");
            builder.HasKey(m => new { m.CodigoEntidad });
            
            builder.Property(p => p.IdentificadorEntidad).HasColumnName("ID_ENTIDAD");
            builder.Property(p => p.CodigoEntidad).HasColumnName("COD_ENTIDAD");
            builder.Property(p => p.NombreEntidad).HasColumnName("DES_ENTIDAD");
            builder.Property(p => p.CodigoEstadoSign).HasColumnName("COD_ESTADO_SIGN");
            builder.Property(p => p.CodigoEstadoCCE).HasColumnName("COD_ESTADO_CCE");
            builder.Property(p => p.CodigoEntidadSBS).HasColumnName("COD_OFICINA_SBS");
            builder.Property(p => p.OficinaPagoTarjeta).HasColumnName("COD_OFICINA_325");

            builder.HasOne(c => c.EstadoSign).WithMany().HasForeignKey(c => new {c.CodigoEstadoSign});
            builder.HasOne(d => d.EstadoCCE).WithMany().HasForeignKey(c => new {c.CodigoEstadoCCE});
        }
    }
}