using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC;

public class BitacoraInteroperabilidadConfiguracion : IEntityTypeConfiguration<BitacoraInteroperabilidadBarrido>
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio BitacoraInteroperabilidadBarrido de la tabla CC_BITACORA_INTEROPERABILIDAD_BARRIDO
    /// </summary>
    public void Configure(EntityTypeBuilder<BitacoraInteroperabilidadBarrido> builder)
    {
        builder.ToTable("CC_BITACORA_INTEROPERABILIDAD_BARRIDO", "CC");
        builder.HasKey(m => new { m.NumeroBitacora });

        builder.Property(p => p.NumeroBitacora).HasColumnName("NUM_BITACORA");
        builder.Property(p => p.CodigoCCI).HasColumnName("COD_CUENTA_INTERBANCARIO");
        builder.Property(p => p.CodigoEntidad).HasColumnName("COD_ENTIDAD_ORIGEN");
        builder.Property(p => p.NumeroCelularOrigen).HasColumnName("NUM_CELULAR_ORIGEN");
        builder.Property(p => p.NumeroCelularReceptor).HasColumnName("NUM_CELULAR_RECEPTOR");
        builder.Property(p => p.NumeroSeguimiento).HasColumnName("NUM_SEGUIMIENTO");
        builder.Property(p => p.IdentificadorInstruccion).HasColumnName("ID_INSTRUCION");
        builder.Property(p => p.CodigoRespuesta).HasColumnName("COD_RESPUESTA");
        builder.Property(p => p.FechaCreacion).HasColumnName("FEC_CREACION");
        builder.Property(p => p.FechaRespuesta).HasColumnName("FEC_RESPUESTA");
        builder.Property(p => p.CodigoUsuarioRegistro).HasColumnName("COD_USUARIO_REGISTRO");
        builder.Property(p => p.CodigoUsuarioModifico).HasColumnName("COD_USUARIO_MODIFICO");
        builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO");
        builder.Property(p => p.FechaModifico).HasColumnName("FEC_MODIFICO");
        builder.Property(p => p.ResultadoAceptado).HasColumnName("IND_RESULTADO_ACEPTADO");
    }
}
