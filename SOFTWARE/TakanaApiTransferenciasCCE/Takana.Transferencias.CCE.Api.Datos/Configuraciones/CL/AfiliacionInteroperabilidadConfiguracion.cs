using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CL;

public class AfiliacionInteroperabilidadConfiguracion : IEntityTypeConfiguration<AfiliacionInteroperabilidad>
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio AfiliacionInteroperabilidad de la tabla CL_AFILIACION_INTEROPERABILIDAD
    /// </summary>
    public void Configure(EntityTypeBuilder<AfiliacionInteroperabilidad> builder)
    {
        builder.ToTable("CL_AFILIACION_INTEROPERABILIDAD", "CL");
        builder.HasKey(k => new { k.CodigoCuentaInterbancario });

        builder.Property(p => p.CodigoServicio).HasColumnName("COD_SERVICIO").HasMaxLength(15);
        builder.Property(p => p.CodigoCliente).HasColumnName("COD_CLIENTE").HasMaxLength(15);
        builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA").HasMaxLength(5);
        builder.Property(p => p.NumeroCuenta).HasColumnName("NUM_CUENTA").HasMaxLength(15);
        builder.Property(p => p.CodigoCuentaInterbancario).HasColumnName("COD_CUENTA_INTERBANCARIO").HasMaxLength(20);
        builder.Property(p => p.CodigoUsuarioRegistro).HasColumnName("COD_USUARIO_REGISTRO").HasMaxLength(20);
        builder.Property(p => p.CodigoUsuarioModifico).HasColumnName("COD_USUARIO_MODIFICO").HasMaxLength(20);
        builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO");
        builder.Property(p => p.FechaModifico).HasColumnName("FEC_MODIFICO");

        builder.HasMany(p => p.Detalles).WithOne(p => p.afiliacion).HasForeignKey(p => p.CodigoCuentaInterbancario);
    }     
}
