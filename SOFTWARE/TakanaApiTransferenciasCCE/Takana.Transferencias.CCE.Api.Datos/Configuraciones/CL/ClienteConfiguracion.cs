using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CL;

/// <summary>
/// Clase que representa el mapeo de la clase de Dominio Cliente de la tabla CL_CLIENTES
/// </summary>
public class ClienteConfiguracion : IEntityTypeConfiguration<Cliente>
{
   public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("CL_CLIENTES", "CL");
        builder.HasKey(k => new { k.CodigoEmpresa, k.CodigoCliente });

        builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA").HasMaxLength(5);
        builder.Property(p => p.CodigoCliente).HasColumnName("COD_CLIENTE").HasMaxLength(15);
        builder.Property(p => p.NombreCliente).HasColumnName("NOM_CLIENTE").HasMaxLength(80);
        builder.Property(p => p.TipoPersona).HasColumnName("IND_PERSONA").HasMaxLength(1);
        builder.Property(p => p.NumeroTelefonoSecundario).HasColumnName("TEL_SECUNDARIO").HasMaxLength(15);
        builder.Property(p => p.DireccionCorreoElectronico).HasColumnName("DES_EMAIL").HasMaxLength(80);
        builder.Property(p => p.CodigoTipoRelacion).HasColumnName("IND_RELACION").HasMaxLength(1);
        builder.Property(p => p.NumeroTelefonoPrincipal).HasColumnName("TEL_PRINCIPAL").HasMaxLength(15);
        builder.Property(p => p.NumeroTelefonoOtro).HasColumnName("TEL_OTRO").HasMaxLength(15);
        builder.Property(p => p.CategoriaCliente).HasColumnName("CAT_CLIENTE").HasMaxLength(5);
        builder.Property(p => p.CodigoPais).HasColumnName("COD_PAIS").HasMaxLength(5);
        builder.Property(p => p.CodigoPaisResidencia).HasColumnName("COD_PAIS_RESIDENCIA").HasMaxLength(5);
        builder.Property(p => p.CodigoLugarActividad).HasColumnName("COD_LUGAR_ACTIV").HasMaxLength(5);
        builder.Property(p => p.FechaModificacion).HasColumnName("FEC_MODIFICACION");
        builder.Property(p => p.FechaConfirmacion).HasColumnName("FEC_CONFIRMACION");
        builder.Property(p => p.CodigoUsuario).HasColumnName("COD_USUARIO");
        builder.Property(p => p.IndicadorUsoDatosPersonales).HasColumnName("IND_USO_DATOS_PERSONALES").HasMaxLength(2);
        builder.Property(p => p.NumeroTelefonoCelular).HasColumnName("TEL_CELULAR").HasMaxLength(12);
        builder.Property(p => p.FechaIngreso).HasColumnName("FEC_INGRESO");
        builder.Property(p => p.CodigoResidencia).HasColumnName("COD_RESIDENCIA");
        builder.Property(p => p.CodigoAgencia).HasColumnName("COD_AGENCIA");

        builder.Ignore(p => p.EsTrabajador);
        builder.Ignore(p => p.DocumentoCartillaPorDefecto);
        builder.Ignore(p => p.DireccionPorDefecto);
        builder.Ignore(p => p.TipoCliente);

        builder.HasOne(c => c.PersonaFisica).WithOne(p => p.Cliente).HasForeignKey<PersonaFisica>(p => new { p.CodigoEmpresa, p.CodigoCliente });
        builder.HasOne(c => c.PersonaJuridica).WithOne(c => c.Cliente).HasForeignKey<PersonaJuridica>(c => new { c.CodigoEmpresa, c.CodigoCliente });
        builder.HasMany(p => p.Documentos).WithOne(p => p.Cliente).HasForeignKey(p => new { p.CodigoEmpresa, p.CodigoCliente, p.CodigoTipoDocumento });
        builder.HasOne(c => c.Nacion).WithMany().HasForeignKey(c => new { c.CodigoPais });

    }     
}
