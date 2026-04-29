using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CL;
/// <summary>
/// Clase que representa el mapeo de la clase de Dominio AfiliacionInteroperabilidadDetalle de la tabla CL_AFILIACION_INTEROPERABILIDAD_DETALLE
/// </summary>
public class AfiliacionInteroperabilidadDetalleConfiguracion : IEntityTypeConfiguration<AfiliacionInteroperabilidadDetalle>
{
    public void Configure(EntityTypeBuilder<AfiliacionInteroperabilidadDetalle> builder)
    {
        builder.ToTable("CL_AFILIACION_INTEROPERABILIDAD_DETALLE", "CL");
        builder.HasKey(k => new { k.NumeroCelular, k.CodigoCuentaInterbancario });

        builder.Property(p => p.NumeroAfiliacion).HasColumnName("NUM_AFILIACION");
        builder.Property(p => p.CodigoCuentaInterbancario).HasColumnName("COD_CUENTA_INTERBANCARIO").HasMaxLength(20);
        builder.Property(p => p.NumeroCelular).HasColumnName("NUM_CELULAR").HasMaxLength(15);
        builder.Property(p => p.IndicadorEstadoAfiliado).HasColumnName("IND_ESTADO_AFILIADO").HasMaxLength(1);
        builder.Property(p => p.FechaAfiliacion).HasColumnName("FEC_AFILIACION");
        builder.Property(p => p.NumeroSeguimiento).HasColumnName("NUM_SEGUIMIENTO").HasMaxLength(6);
        builder.Property(p => p.ContadorBarridosContacto).HasColumnName("CON_BARRIDOS_CONTACTO");
        builder.Property(p => p.FechaBloqueo).HasColumnName("FEC_BLOQUEO");
        builder.Property(p => p.CodigoUsuarioRegistro).HasColumnName("COD_USUARIO_REGISTRO").HasMaxLength(20);
        builder.Property(p => p.CodigoUsuarioModifico).HasColumnName("COD_USUARIO_MODIFICO").HasMaxLength(20);
        builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO");
        builder.Property(p => p.FechaModifico).HasColumnName("FEC_MODIFICO");
        builder.Property(p => p.IdentificadorQR).HasColumnName("ID_QR_CCE");
        builder.Property(p => p.CadenaHash).HasColumnName("COD_CADENA_HASH");
        builder.Property(p => p.Canal).HasColumnName("COD_CANAL");

        builder.Property(p => p.IndicadoRecibirNotificacion).HasColumnName("IND_RECIBIR_NOTIFICACION");
        builder.Property(p => p.IndicadorEnviarNotificacion).HasColumnName("IND_ENVIAR_NOTIFICACION");
    }
}
