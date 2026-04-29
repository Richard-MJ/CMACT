using AutorizadorCanales.Domain.Entidades.CL;

namespace AutorizadorCanales.Domain.Entidades.TJ;

public class Tarjeta
{
    public decimal NumeroTarjeta { get; set; }
    public string? CodigoAgencia { get; private set; }
    public string? CodigoCliente { get; private set; }
    public string CodigoTipoTarjeta { get; private set; } = null!;
    public string CodigoEstadoTarjeta { get; private set; } = null!;
    public string TipoEstadoTarjeta { get; private set; } = null!;
    public DateTime? FechaVencimiento { get; private set; }
    public string? NumeroPvv1 { get; set; }
    public string? NumeroPvv2 { get; set; }
    public string? NumeroPvvHomebanking1 { get; set; }
    public string? NumeroPvvHomebanking2 { get; set; }
    public string? NumeroPvvHomebankingAnterior1 { get; set; }
    public string? NumeroPvvHomebankingAnterior2 { get; set; }
    public string IndicadorChip { get; set; } = null!;
    public string? CodigoUsuarioRecepcion { get; set; }
    public string CodigoEmpresa { get; set; } = null!;
    public string? IndicadorAfiliadoHomeBanking { get; set; }
    public DateTime? FechaAfiliacionHomeBanking { get; set; }
    public DateTime? FechaActivacion { get; set; }
    public string IndicadorAfiliadoAppMovil { get; set; } = null!;
    public DateTime? FechaUltimoCambioPassHomebanking { get; set; }
    public string? CodigoUsuarioDerivacion { get; set; }
    public string CodigoAgenciaDerivacion { get; set; } = null!;
    public string CodigoUsuarioRegistro { get; set; } = null!;
    public DateTime FechaServidor { get; set; }
    public string IndicadorContactless { get; set; } = null!;
    public string IndicadorMicroPagos { get; set; } = null!;

    /// <summary>
    /// Representa el código de Lote de tarjetas asociado con esta tarjeta
    /// </summary>
    public string CodigoLote { get; set; } = null!;

    /// <summary>
    /// Representa la fecha de emisión de la tarjeta
    /// </summary>
    public DateTime? FechaEmision { get; set; }

    /// <summary>
    /// Representa el código del motivo de anulación
    /// </summary>
    public string? CodigoMotivoAnulacion { get; private set; }

    /// <summary>
    /// Representa un detalle de la anulación
    /// </summary>
    public string? DescripcionAnulacion { get; private set; }

    /// <summary>
    /// Fecha e la anulación de la tarjeta
    /// </summary>
    public DateTime? FechaAnulacion { get; private set; }

    /// <summary>
    /// Codigo del usuario que realizo la anulación
    /// </summary>
    public string? CodigoUsuarioAnulacion { get; private set; }

    /// <summary>
    /// Representa la Fecha Derivacion asociado con esta tarjeta
    /// </summary>
    public DateTime? FechaDerivacion { get; private set; }
    /// <summary>
    /// Representa la Fecha Recepcion asociado con esta tarjeta
    /// </summary>
    public DateTime? FechaRecepcion { get; private set; }

    public virtual Cliente? Duenio { get; private set; } = null!;

    /// <summary>
    /// Representa los dispositivos registrados en la tarjeta
    /// </summary>
    public virtual ICollection<DispositivoCanalElectronico> DispositivosCanalElectronico { get; set; } = new List<DispositivoCanalElectronico>();
    /// <summary>
    /// Representa los dispositivos registrados en la tarjeta
    /// </summary>
    public virtual ICollection<AfiliacionCanalElectronico> AfiliacionesCanalElectronico { get; set; } = new List<AfiliacionCanalElectronico>();

    public int? NumeroComprobante { get; set; }

    public bool TarjetaVencida(DateTime fechaSistema)
    {
        if(FechaVencimiento == null)
            return true;

        return !( DateTime.Compare(fechaSistema.Date, FechaVencimiento?.Date ?? DateTime.Now) <= 0);
    }

    public bool Activa()
    {
        return TipoEstadoTarjeta == "TR" && CodigoEstadoTarjeta == "01";
    }

    public void PrepararHomeBankingEmpresarial(DateTime fecha)
    {
        IndicadorAfiliadoAppMovil = EstadoEntidad.NO;
        IndicadorAfiliadoHomeBanking = EstadoEntidad.NO;
        FechaAfiliacionHomeBanking = fecha;
    }

    public void HabilitarAfiliacionHomeBankingPersonal(DateTime fecha)
    {
        IndicadorAfiliadoAppMovil = EstadoEntidad.SI;
        IndicadorAfiliadoHomeBanking = EstadoEntidad.SI;
        FechaAfiliacionHomeBanking = fecha;
    }

    public void RealizarActualizacionPvvHomeBanking(string pvv1, string pvv2)
    {
        NumeroPvvHomebankingAnterior1 = NumeroPvvHomebanking1;
        NumeroPvvHomebankingAnterior2 = NumeroPvvHomebanking2;
        NumeroPvvHomebanking1 = pvv1;
        NumeroPvvHomebanking2 = pvv2;
    }

    public bool EstaAfiliadoHomeBanking()
        => IndicadorAfiliadoHomeBanking == EstadoEntidad.SI;

    public bool NoRegistraClaveHomeBanking()
        => NumeroPvvHomebanking1 == null || NumeroPvvHomebanking2 == null || AfiliacionCanalElectronico == null;


    public DispositivoCanalElectronico? DispositivoCorrespondiente(List<string> dispositivos)
    {
        return DispositivosCanalElectronico.Where(x => dispositivos.Contains(x.DispositivoId) &&
                 x.NumeroTarjeta == this.NumeroTarjeta &&
                 x.CodigoCliente == this.Duenio?.CodigoCliente &&
                 x.IndicadorEstado == EstadoEntidad.ACTIVO
                 )?.FirstOrDefault();
    }

    public AfiliacionCanalElectronico? AfiliacionCanalElectronico =>
        AfiliacionesCanalElectronico
                .Where(a => a.IndicadorActivo)
                .OrderByDescending(a => a.FechaAfiliacionPrincipal)
                .FirstOrDefault();

    public bool AfiliadaServicioSms => 
        AfiliacionCanalElectronico != null && AfiliacionCanalElectronico.EsAfiliacionSms;

    public string NumeroTarjetaString => NumeroTarjeta.ToString();

    public bool EsAnulada => CodigoEstadoTarjeta == "03";
}
