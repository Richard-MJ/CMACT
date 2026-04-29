using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF;

public class CalendarioConfiguracion: IEntityTypeConfiguration<Calendario>
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio Calendario de la tabla CF_CALENDARIOS
    /// </summary>
    public void Configure(EntityTypeBuilder<Calendario> builder)
    {
        builder.ToTable("CF_CALENDARIOS", "CF");
        builder.HasKey(k => new { k.CodigoEmpresa, k.CodigoAgencia, k.CodigoSistema });

        builder.Property(m => m.CodigoEmpresa).HasColumnName("COD_EMPRESA");
        builder.Property(m => m.CodigoAgencia).HasColumnName("COD_AGENCIA");
        builder.Property(m => m.CodigoSistema).HasColumnName("COD_SISTEMA");
        builder.Property(m => m.FechaSistema).HasColumnName("FEC_HOY");
    }
}
