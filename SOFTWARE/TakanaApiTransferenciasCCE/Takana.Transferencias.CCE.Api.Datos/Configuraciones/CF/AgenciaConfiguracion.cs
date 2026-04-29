using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio Agencia con la entidad CF_AGENCIAS
    /// </summary>
    public class AgenciaConfiguracion : IEntityTypeConfiguration<Agencia>
    {
        public void Configure(EntityTypeBuilder<Agencia> builder)
        {
            builder.ToTable("CF_AGENCIAS", "CF");
            builder.HasKey(m => new { m.CodigoEmpresa, m.CodigoAgencia });

            builder.Property(m => m.CodigoEmpresa).HasColumnName("COD_EMPRESA");
            builder.Property(m => m.CodigoAgencia).HasColumnName("COD_AGENCIA");
            builder.Property(m => m.NombreAgencia).HasColumnName("DES_AGENCIA");
            builder.Property(m => m.Direccion).HasColumnName("DIR_FISICA");
            builder.Property(m => m.Activo).HasColumnName("IND_FUNCIONAMIENTO");
            builder.Property(m => m.Estado).HasColumnName("IND_ESTADO");
            builder.Property(m => m.NombreCiudad).HasColumnName("NOM_CIUDAD");
            builder.Property(m => m.CodigoUbigeo).HasColumnName("COD_UBIGEO");
        }
    }
}