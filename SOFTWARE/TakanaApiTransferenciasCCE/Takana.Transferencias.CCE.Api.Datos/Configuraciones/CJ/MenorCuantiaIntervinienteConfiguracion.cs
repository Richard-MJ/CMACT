using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CJ;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CJ
{/// <summary>
 /// Clase que representa el mapeo de la clase de Dominio MenorCuantiaInterviniente de la tabla CJ_MENOR_CUANTIA_INTERVI
 /// </summary>
    public class MenorCuantiaIntervinienteConfiguracion : IEntityTypeConfiguration<MenorCuantiaInterviniente>
    {
        public void Configure(EntityTypeBuilder<MenorCuantiaInterviniente> builder)
        {
            builder.ToTable("CJ_MENOR_CUANTIA_INTERVI", "CJ");
            builder.HasKey(k => new { k.IdInterviniente });

            builder.Property(p => p.IdInterviniente).HasColumnName("ID_MENOR_CUANTIA_INTERVI").IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.NumeroOperacion).HasColumnName("NUM_OPERACION");
            builder.Property(p => p.TipoInterviniente).HasColumnName("TIPO_INTERVINIENTE");
            builder.Property(p => p.TipoDocumento).HasColumnName("TIP_DOC");
            builder.Property(p => p.NumeroDocumento).HasColumnName("NUM_DOC");
            builder.Property(p => p.TipoCliente).HasColumnName("TIP_CLIENTE");
            builder.Property(p => p.CodigoCliente).HasColumnName("COD_CLIENTE");
            builder.Property(p => p.ApellidoPaterno).HasColumnName("APELLIDO_PATERNO");
            builder.Property(p => p.ApellidoMaterno).HasColumnName("APELLIDO_MATERNO");
            builder.Property(p => p.Nombres).HasColumnName("NOMBRES");
            builder.Property(p => p.EstadoRegistro).HasColumnName("IND_ESTADO");

            builder.HasOne(m => m.Encabezado).WithMany(d => d.Intervinientes).HasForeignKey(f => f.NumeroOperacion);
        }
    }
}
