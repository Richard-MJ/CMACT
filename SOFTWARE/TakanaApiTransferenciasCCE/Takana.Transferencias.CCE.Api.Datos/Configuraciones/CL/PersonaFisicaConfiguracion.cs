using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CL;

/// <summary>
/// Clase que representa el mapeo de la clase de Dominio PersonaFisica de la tabla CL_PERSONAS_FISICAS
/// </summary>
public class PersonaFisicaConfiguracion : IEntityTypeConfiguration<PersonaFisica>
{
   public void Configure(EntityTypeBuilder<PersonaFisica> builder)
    {
        builder.ToTable("CL_PERSONAS_FISICAS", "CL");
        builder.HasKey(k => new { k.CodigoEmpresa, k.CodigoCliente });
        builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA").HasMaxLength(5);
        builder.Property(p => p.CodigoCliente).HasColumnName("COD_CLIENTE").HasMaxLength(15);
        builder.Property(p => p.DireccionCorreoElectronico).HasColumnName("EMAIL").HasMaxLength(60);
        builder.Property(p => p.IndicadorSexo).HasColumnName("IND_SEXO").HasMaxLength(1);
        builder.Property(p => p.PrimerNombre).HasColumnName("PRIMER_NOMBRE").HasMaxLength(50);
        builder.Property(p => p.PrimerApellido).HasColumnName("PRIMER_APELLIDO").HasMaxLength(50);
        builder.Property(p => p.SegundoNombre).HasColumnName("SEGUNDO_NOMBRE").HasMaxLength(50);
        builder.Property(p => p.CodigoSector).HasColumnName("COD_SECTOR").HasMaxLength(5);
        builder.Property(p => p.CodigoSubActividad).HasColumnName("COD_SUBACTIV").HasMaxLength(5);
        builder.Property(p => p.CodigoSubSubActividad).HasColumnName("COD_SUBSUBACTIV").HasMaxLength(5);
        builder.Property(p => p.IdSectorActividad).HasColumnName("ID_SECTOR_ACTIVIDAD");
        builder.Property(p => p.CodigoActividad).HasColumnName("COD_ACTIVIDAD").HasMaxLength(5);
        builder.Property(p => p.CodigoOcupacion).HasColumnName("COD_OCUPACION");
        builder.Property(p => p.CodigoCargo).HasColumnName("COD_CARGO");
        builder.Property(p => p.Nacionalidad).HasColumnName("NACIONALIDAD").HasMaxLength(30);
        builder.Property(p => p.CodigoEstadoCivil).HasColumnName("EST_CIVIL").HasMaxLength(1);
        builder.Property(p => p.ApellidoCasado).HasColumnName("APELLIDO_CASADO").HasMaxLength(50);
        builder.Property(p => p.SegundoApellido).HasColumnName("SEGUNDO_APELLIDO").HasMaxLength(50);
        builder.Property(p => p.IndicadorDeclaraNoEmail).HasColumnName("IND_DECLARA_NO_EMAIL");
        builder.Property(p => p.CodigoProfesion).HasColumnName("COD_PROFESION").HasMaxLength(5);
        builder.Property(p => p.LugarNacimiento).HasColumnName("LUGAR_NACIMIENTO").HasMaxLength(99);
        builder.Property(p => p.IndicadorPersonaPolitica).HasColumnName("IND_PEP").HasMaxLength(1);
        builder.Property(p => p.FechaNacimiento).HasColumnName("FEC_NACIMIENTO");
        builder.Property(p => p.IndicadorSujetoObligado).HasColumnName("IND_OBLIGADO").HasMaxLength(1);
        builder.Property(p => p.Observaciones).HasColumnName("OBSERVACIONES").HasMaxLength(200);
        builder.Property(p => p.CodigoClienteConyugue).HasColumnName("COD_CONYUGE").HasMaxLength(15);
        builder.Property(p => p.NivelEducativo).HasColumnName("NIV_EDUCATIVO").HasMaxLength(2);
        builder.Property(p => p.TipoPersona).HasColumnName("TIP_PERSONA");

        builder.Ignore(p => p.SegundoApellidoCuantia);
        builder.Ignore(p => p.Nombres);
        builder.Ignore(p => p.ApellidoPaterno);
        builder.Ignore(p => p.ApellidoMaterno);
        builder.Ignore(p => p.TipoCliente);

        builder.HasOne(f => f.Cliente).WithOne(f => f.PersonaFisica).HasForeignKey<Cliente>(p => new { p.CodigoEmpresa, p.CodigoCliente });
        builder.HasOne(f => f.Conyugue).WithMany().HasForeignKey(f => new { f.CodigoEmpresa, f.CodigoClienteConyugue });

    }     
}
