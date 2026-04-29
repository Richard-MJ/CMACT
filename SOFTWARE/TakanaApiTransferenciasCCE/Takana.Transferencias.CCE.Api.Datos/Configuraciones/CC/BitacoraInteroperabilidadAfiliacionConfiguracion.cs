using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC;

public class BitacoraInteroperabilidadAfiliacionConfiguracion : IEntityTypeConfiguration<BitacoraInteroperabilidadAfiliacion>
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio BitacoraInteroperabilidadAfiliacion de la tabla CC_BITACORA_INTEROPERABILIDAD_AFILIACION
    /// </summary>
    public void Configure(EntityTypeBuilder<BitacoraInteroperabilidadAfiliacion> builder)
    {
        builder.ToTable("CC_BITACORA_INTEROPERABILIDAD_AFILIACION", "CC");
        builder.HasKey(m => new { m.NumeroBitacora });

        builder.Property(p => p.NumeroBitacora).HasColumnName("NUM_BITACORA");
        builder.Property(p => p.CodigoCCI).HasColumnName("COD_CUENTA_INTERBANCARIO");
        builder.Property(p => p.NumeroCelular).HasColumnName("NUM_CELULAR");
        builder.Property(p => p.IdenditificadorInstruccion).HasColumnName("ID_INSTRUCCION");
        builder.Property(p => p.NumeroSeguimiento).HasColumnName("NUM_SEGUIMIENTO");
        builder.Property(p => p.CodigoTipoInstruccion).HasColumnName("COD_TIPO_INSTRUCCION_ENVIADO");
        builder.Property(p => p.IndicadorEstadoAfiliacion).HasColumnName("IND_ESTADO_AFILIACION");
        builder.Property(p => p.Canal).HasColumnName("COD_CANAL");
        builder.Property(p => p.CodigoRespuesta).HasColumnName("COD_RESPUESTA");
        builder.Property(p => p.FechaCreacion).HasColumnName("FEC_CREACION");
        builder.Property(p => p.FechaRespuesta).HasColumnName("FEC_RESPUESTA");
        builder.Property(p => p.CodigoUsuarioRegistro).HasColumnName("COD_USUARIO_REGISTRO");
        builder.Property(p => p.CodigoUsuarioModifico).HasColumnName("COD_USUARIO_MODIFICO");
        builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO");
        builder.Property(p => p.FechaModifico).HasColumnName("FEC_MODIFICO");
    }
}
