using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC;

/// <summary>
/// Clase que representa el mapeo de la clase de Dominio CuentaEfectivo de la tabla CC_CUENTA_EFECTIVO
/// </summary>
public class CuentaEfectivoConfiguracion : IEntityTypeConfiguration<CuentaEfectivo>
{
    public void Configure(EntityTypeBuilder<CuentaEfectivo> builder)
    {
        builder.ToTable("CC_CUENTA_EFECTIVO", "CC");
        builder.HasKey(k => new { k.CodigoEmpresa, k.NumeroCuenta });

        builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA").IsRequired().HasMaxLength(5);
        builder.Property(p => p.NumeroCuenta).HasColumnName("NUM_CUENTA").IsRequired().HasMaxLength(15);
        builder.Property(p => p.CodigoProducto).HasColumnName("COD_PRODUCTO");
        builder.Property(p => p.CodigoCliente).HasColumnName("COD_CLIENTE").HasMaxLength(15);
        builder.Property(p => p.SaldoDisponible).HasColumnName("SAL_DISPONIBLE").IsConcurrencyToken();
        builder.Property(p => p.SaldoIntangible).HasColumnName("SAL_CONGELADO");
        builder.Property(p => p.SaldoTransito).HasColumnName("SAL_TRANSITO");
        builder.Property(p => p.SaldoReserva).HasColumnName("SAL_RESERVA");
        builder.Property(p => p.InteresDisponible).HasColumnName("INT_POR_PAGAR");
        builder.Property(p => p.InteresIntangible).HasColumnName("INT_PAGADO");
        builder.Property(p => p.CodigoEstado).HasColumnName("IND_ESTADO");
        builder.Property(p => p.CodigoAgencia).HasColumnName("COD_AGENCIA");
        builder.Property(p => p.CodigoModalidad).HasColumnName("COD_MODALIDAD");
        builder.Property(p => p.FechaEstado).HasColumnName("FEC_ESTADO").IsRequired();
        builder.Property(p => p.FechaApertura).HasColumnName("FEC_APERTURA").IsRequired();
        builder.Property(p => p.FechaInicialSobregiro).HasColumnName("FEC_INI_SOBGRO");
        builder.Property(p => p.FechaUltimaActualizacionIntereses).HasColumnName("FEC_ULT_ACT_INT");
        builder.Property(p => p.FechaUltimaCapitalizacionIntereses).HasColumnName("FEC_ULT_CAP_INT");
        builder.Property(p => p.IndicadorTipoComercial).HasColumnName("IND_TIPO_CUENTA");
        builder.Property(p => p.IndicadorTipoAsociacion).HasColumnName("IND_TIP_CUENTA");
        builder.Property(p => p.CodigoMoneda).HasColumnName("COD_MONEDA").HasMaxLength(5);
        builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA");
        builder.Property(p => p.CodigoCuentaInterbancario).HasColumnName("NUM_CUENTA_CCI");
        builder.Property(p => p.CodigoUsuarioApertura).HasColumnName("COD_USUARIO").HasMaxLength(15);
        builder.Property(p => p.CodigoClientePatrono).HasColumnName("COD_CLIENTE_PATRONO").HasMaxLength(15);
        builder.Property(p => p.NombreChequera).HasColumnName("NOM_CHEQUERA").HasMaxLength(80);
        builder.Property(p => p.IndicadorTipoCargos).HasColumnName("IND_TIP_CARGOS").HasMaxLength(1);
        builder.Property(p => p.IndicadorCuentaAlterna).HasColumnName("IND_CTA_ALTERNA").HasMaxLength(1);
        builder.Property(p => p.IndicadorPagaIntereses).HasColumnName("IND_PAG_INTERES").HasMaxLength(1);
        builder.Property(p => p.SaldoConsultado).HasColumnName("SAL_CONSULTADO").IsRequired();
        builder.Property(p => p.SaldoPromedio).HasColumnName("SAL_PROMEDIO").IsRequired();
        builder.Property(p => p.SaldoUltimoCorte).HasColumnName("SAL_ULT_CORTE").IsRequired();
        builder.Property(p => p.MontoSaldoEnReservaUtilizado).HasColumnName("MON_RESERVA_UTL").IsRequired();
        builder.Property(p => p.MontoSobreGiroPrePactado).HasColumnName("MON_SOBGRO_AUT").IsRequired();
        builder.Property(p => p.MontoSobreGiroTemporal).HasColumnName("MON_SOB_NO_AUT").IsRequired();
        builder.Property(p => p.MontoSobreGiroDisponible).HasColumnName("MON_SOBGRO_DISP").IsRequired();
        builder.Property(p => p.MontoTotalCargos).HasColumnName("MON_TOTAL_CARGO").IsRequired();
        builder.Property(p => p.InteresPorCapitalCongelado).HasColumnName("INT_CAP_CONGELA").IsRequired();
        builder.Property(p => p.InteresPorCapitalEnReserva).HasColumnName("INT_CAP_RESERVA").IsRequired();
        builder.Property(p => p.InteresPorSobreGiroPrePactado).HasColumnName("INT_SOBGRO_AUT").IsRequired();
        builder.Property(p => p.InteresPorReservaUtilizada).HasColumnName("INT_RESERVA_UTL").IsRequired();
        builder.Property(p => p.IndicadorSobreGiro).HasColumnName("IND_SOBGRO").HasMaxLength(1).IsRequired();
        builder.Property(p => p.NumeroCuentaRelacionada).HasColumnName("NUM_CTA_RELACIONADA").HasMaxLength(15);
        builder.Property(p => p.InteresGanadoMesActual).HasColumnName("INT_MES_ACTUAL").IsRequired();
        builder.Property(p => p.IndicadorTipoCorrespondencia).HasColumnName("IND_CORRESPONDENCIA").HasMaxLength(1).IsRequired();
        builder.Property(p => p.FechaUltimoMovimiento).HasColumnName("FEC_ULT_MOVIMIENTO");
        builder.Property(p => p.ObservacionEstadoCuenta).HasColumnName("OBS_ESTADO_CUENTA").HasMaxLength(255);
        builder.Property(p => p.IndicadorTipoCliente).HasColumnName("IND_TIPCLIENTE").HasMaxLength(1).IsRequired();
        builder.Property(p => p.NumeroCuentaCompleto).HasColumnName("NUM_CTA_COMPLETO").HasMaxLength(18).IsRequired();
        builder.Property(p => p.IndicadorCuentaContableIntermediacion).HasColumnName("IND_CTA_INTERMED").HasMaxLength(1).IsRequired();
        builder.Property(p => p.IndicadorEnvioEmail).HasColumnName("IND_POR_EMAIL").HasMaxLength(1).IsRequired();
        builder.Property(p => p.CodigoDireccion).HasColumnName("COD_DIRECCION").HasMaxLength(2);
        builder.Property(p => p.CodigoCategoria).HasColumnName("COD_CATEGORIA").HasMaxLength(5);
        builder.Property(p => p.CodigoEstadoCuenta).HasColumnName("COD_ESTADO").HasMaxLength(5);
        builder.Property(p => p.MontoTasaPreferencial).HasColumnName("MON_TASA_PREFERENCIAL").HasPrecision(11, 4);
        builder.Property(p => p.CodigoMotivoBloqueo).HasColumnName("COD_MOTIVO_BLOQUEO").HasPrecision(2, 0);
        builder.Property(p => p.NumeroMovimiento).HasColumnName("NUM_MOVIMIENTO");

        builder.Ignore(p => p.NumeroProducto);
        builder.Ignore(p => p.CodigoTipo);
        builder.Ignore(p => p.SimboloMoneda);
        builder.Ignore(p => p.DescripcionMonto);
        builder.Ignore(p => p.MontoDisponible);
        builder.Ignore(p => p.MontoContable);
        builder.Ignore(p => p.IdentificadorCci);
        builder.Ignore(p => p.FechaVencimiento);
        builder.Ignore(p => p.IndicadorPlanAhorro);

        builder.HasOne(c => c.Cliente).WithMany().HasForeignKey(c => new { c.CodigoEmpresa, c.CodigoCliente });
        builder.HasOne(c => c.Agencia).WithMany().HasForeignKey(c => new { c.CodigoEmpresa, c.CodigoAgencia });
        builder.HasOne(c => c.Moneda).WithMany().HasForeignKey(c => c.CodigoMoneda);
        builder.HasOne(c => c.EstadoCuenta).WithMany().HasForeignKey(c => c.CodigoEstado);
        builder.HasOne(c => c.Producto).WithMany().HasForeignKey(c => new { c.CodigoEmpresa, c.CodigoSistema, c.CodigoProducto });
        builder.HasOne(c => c.Caracteristicas).WithMany().HasForeignKey(c => new { c.CodigoEmpresa, c.CodigoSistema, c.CodigoProducto });
        builder.HasOne(c => c.CuentaExonerada).WithMany().HasForeignKey(c => new { c.CodigoEmpresa, c.CodigoAgencia, c.CodigoSistema, c.NumeroCuenta });
        builder.HasOne(c => c.DireccionCliente).WithMany().HasForeignKey(c => new { c.CodigoEmpresa, c.CodigoCliente, c.CodigoDireccion });
        builder.HasOne(c => c.TipoCuentaGrupo).WithMany().HasForeignKey(c => new { c.IndicadorTipoAsociacion });
        builder.HasOne(c => c.AliasProductoCliente).WithMany().HasForeignKey(c => new { c.NumeroCuenta, c.CodigoSistema });
        builder.HasOne(c => c.CuentaEfectivoSueldo).WithOne(c => c.CuentaEfectivo).HasForeignKey<CuentaEfectivoSueldo>(c => new { c.CodigoEmpresa, c.NumeroCuenta });
    }
}
