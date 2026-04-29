using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CL;

/// <summary>
/// Clase que representa el mapeo de la clase de Dominio ProductoExonerado de la tabla CL_PRODUCTOS_EXONERADOS
/// </summary>
public class ProductoExoneradoConfiguracion : IEntityTypeConfiguration<ProductoExonerado>
{
   public void Configure(EntityTypeBuilder<ProductoExonerado> builder)
    {
        builder.ToTable("CL_PRODUCTOS_EXONERADOS", "CL");
        builder.HasKey(k => new { k.CodigoEmpresa, k.CodigoAgencia, k.CodigoSistema, k.NumeroProducto });

        builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA").IsRequired();
        builder.Property(p => p.CodigoAgencia).HasColumnName("COD_AGENCIA").IsRequired();
        builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA").IsRequired();
        builder.Property(p => p.NumeroProducto).HasColumnName("NUM_PRODUCTO").IsRequired();
        builder.Property(p => p.TipoDeclaracion).HasColumnName("TIP_DECLARACION");
        builder.Property(p => p.CodigoOperacion).HasColumnName("COD_OPERACION");
        builder.Property(p => p.FechaSolicitudAlta).HasColumnName("FEC_SOLICITUD_ALTA");
        builder.Property(p => p.ContadorSolicitudes).HasColumnName("CONT_SOLICITUDES");
        builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO");
        builder.Property(p => p.IndicadorRemuneracion).HasColumnName("IND_REMUNERACION");
        builder.Property(p => p.NumeroCuentaPatrono).HasColumnName("NUM_CTA_PATRONO");
        builder.Property(p => p.CodigoAgenciaUsuario).HasColumnName("COD_AGENCIA_US");
        builder.Property(p => p.CodigoUsuario).HasColumnName("COD_USUARIO");
        builder.Property(p => p.CodigoInciso).HasColumnName("COD_INCISO");
        builder.Property(p => p.FechaDocumento).HasColumnName("FEC_DOCUMENTO");
        builder.Property(p => p.NumeroDocumento).HasColumnName("NUM_DOCUMENTO");

    }     
}
