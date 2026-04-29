using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF;

/// <summary>
/// Clase que representa el mapeo de la clase de Dominio ParametroPorAgencia de la tabla CF_PARAMETROS_X_AGENCIA
/// </summary>
public class ParametroPorAgenciaConfiguracion : IEntityTypeConfiguration<ParametroPorAgencia>
{
    public void Configure(EntityTypeBuilder<ParametroPorAgencia> builder)
    {
        builder.ToTable("CF_PARAMETROS_X_AGENCIA", "CF");
        builder.HasKey(m => new { m.CodigoEmpresa, m.CodigoAgencia, m.CodigoSistema, m.CodigoParametro });

        builder.Property(m => m.CodigoEmpresa).HasColumnName("COD_EMPRESA");
        builder.Property(m => m.CodigoAgencia).HasColumnName("COD_AGENCIA");
        builder.Property(m => m.CodigoSistema).HasColumnName("COD_SISTEMA");
        builder.Property(m => m.CodigoParametro).HasColumnName("COD_PARAMETRO");
        builder.Property(m => m.ValorParametro).HasColumnName("VAL_PARAMETRO");
    }
}
