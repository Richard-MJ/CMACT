using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CJ;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CJ
{/// <summary>
 /// Clase que representa el mapeo de la clase de Dominio MenorCuantiaEncabezado de la tabla CJ_MENOR_CUANTIA_ENCA
 /// </summary>
    public class MenorCuantiaEncabezadoConfiguracion : IEntityTypeConfiguration<MenorCuantiaEncabezado>
    {
        public void Configure(EntityTypeBuilder<MenorCuantiaEncabezado> builder)
        {
            builder.ToTable("CJ_MENOR_CUANTIA_ENCA", "CJ");
            builder.HasKey(k => new { k.NumeroLavado });

            builder.Property(p => p.NumeroLavado).HasColumnName("NUM_OPERACION").IsRequired().ValueGeneratedNever();
            builder.Property(p => p.CodigoAgencia).HasColumnName("COD_AGENCIA").HasMaxLength(5);
            builder.Property(p => p.CodigoModalidad).HasColumnName("COD_MODALIDAD");
            builder.Property(p => p.CodigoSubModalidad).HasColumnName("SUB_MODALIDAD");
            builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO");
            builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO").HasMaxLength(5);
            builder.Property(p => p.CodigoUsuario).HasColumnName("COD_USUARIO").HasMaxLength(15);
            builder.Property(p => p.IndicadorFormaPago).HasColumnName("IND_FORMA_PAGO");
            builder.Property(p => p.CodigoUbigeo).HasColumnName("COD_UBIGEO");
        }
    }
}
