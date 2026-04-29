using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.BA
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio EntidadFinancieraCCE de la tabla BA_ENTIDAD_FINANCIERA_CCE
    /// </summary>
    public class EntidadFinancieraDiferidaConfiguracion : IEntityTypeConfiguration<EntidadFinancieraDiferida>
    {
        public void Configure(EntityTypeBuilder<EntidadFinancieraDiferida> builder)
        {
            builder.ToTable("BA_ENTIDAD_FINANCIERA_CCE", "BA");
            builder.HasKey(k => k.IDEntidadFinanciera);

            builder.Property(p => p.IDEntidadFinanciera).HasColumnName("ID_ENTIDAD").IsRequired();
            builder.Property(p => p.NombreEntidad).HasColumnName("DES_ENTIDAD").IsRequired();
            builder.Property(p => p.CodigoEntidad).HasColumnName("COD_ENTIDAD_CCE").IsRequired();
            builder.Property(p => p.OficinaPagoTarjeta).HasColumnName("COD_OFICINA_TIN_225");
            builder.Property(p => p.EstaActivaCheque).HasColumnName("IND_CHEQUE_CCE").IsRequired();
            builder.Property(p => p.CodigoEntidadSbs).HasColumnName("COD_ENTIDAD_SBS");
        }
    }
}
