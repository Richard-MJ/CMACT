using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF;

/// <summary>
/// Clase que representa el mapeo de la clase de Dominio CodigoRespuesta de la tabla CF_TIN_INMEDIATA_CODIGO_RESPUESTA
/// </summary>
public class CodigoRespuestaConfiguracion: IEntityTypeConfiguration<CodigoRespuesta>
{
    public void Configure(EntityTypeBuilder<CodigoRespuesta> builder)
    {
        builder.ToTable("CF_TIN_INMEDIATA_CODIGO_RESPUESTA", "CF");
        builder.HasKey(m => new { m.Codigo });
        
        builder.Property(p => p.Codigo).HasColumnName("COD_CODIGO_RESPUESTA");
        builder.Property(p => p.Nombre).HasColumnName("NOM_CODIGO_RESPUESTA");
        builder.Property(p => p.Descripcion).HasColumnName("DES_CODIGO_RESPUESTA");
        builder.Property(p => p.MensajeCliente).HasColumnName("DES_MENSAJE_RESPUESTA");
        builder.Property(p => p.DescripcionEstadoCuenta).HasColumnName("DES_ESTADO_CUENTA");
        builder.Property(p => p.TipoCodigoRespuesta).HasColumnName("TIP_CODIGO_RESPUESTA");
    }
}
