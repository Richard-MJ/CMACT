using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CL
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio AfiliadoServicio de la tabla CL_AFILIACIONES_SERVICIOS
    /// </summary>
    public class AfiliadoServicioConfiguracion : IEntityTypeConfiguration<AfiliadoServicio>
    {
        public void Configure(EntityTypeBuilder<AfiliadoServicio> builder)
        {
            builder.ToTable("CL_AFILIACIONES_SERVICIOS", "CL");
            builder.HasKey(m => new { m.NumeroAfiliado, m.CodigoServicio });

            builder.Property(m => m.NumeroAfiliado).HasColumnName("NUM_AFILIACION").IsRequired();
            builder.Property(m => m.CodigoServicio).HasColumnName("COD_SERVICIO");
            builder.Property(m => m.IndicadorEstado).HasColumnName("IND_ESTADO");
            builder.Property(m => m.FechaDesafiliacion).HasColumnName("FEC_DESAFILIACION");
            builder.Property(m => m.CodigoAgenciaDesafiliacion).HasColumnName("COD_AGENCIA_DESAFILIACION");
            builder.Property(m => m.CodigoUsuarioDesafiliacion).HasColumnName("COD_USUARIO_DESAFILIACION");
            builder.Property(m => m.FechaModificacion).HasColumnName("FEC_MODIFICACION");
            builder.Property(m => m.FechaAfiliacion).HasColumnName("FEC_AFILIACION");

            builder.HasOne(m => m.Afiliado).WithMany(a => a.ServiciosAfiliado).HasForeignKey(m => m.NumeroAfiliado);
        }
    }
}
