using AutorizadorCanales.Domain.Entidades.CL;

namespace AutorizadorCanales.Domain.Entidades.SG;

public class AfiliacionTokenDigital
{
    #region Propiedades
    /// <summary>
    /// Propiedad que almacena el ID del registro del dispositivo 
    /// </summary>
    public int? IdAfilacion { get; private set; }
    /// <summary>
    /// Propiedad que almacena el codigo de empresa 
    /// </summary>
    public string CodigoEmpresa { get; private set; } = null!;
    /// <summary>
    /// Propiedad que almacena el documento de cliente 
    /// </summary>
    public string NumeroTarjeta { get; private set; } = null!;
    /// <summary>
    /// Propiedad que almacena el modelo del dispositivo 
    /// </summary>
    public string? ModeloDispositivo { get; private set; } = null!;
    /// <summary>
    /// Propiedad que almacena la marca del dispositivo 
    /// </summary>
    public string? FabricanteDispositivo { get; private set; } = null!;
    /// <summary>
    /// Propiedad que almacena el nombre del dispositivo 
    /// </summary>
    public string? NombreDispositivo { get; private set; } = null!;
    /// <summary>
    /// Propiedad que almacena la plataforma del dispositivo 
    /// </summary>
    public string? PlataformaDispositivo { get; private set; } = null!;
    /// <summary>
    /// Propiedad que almacena la categoria del dispositivo 
    /// </summary>
    public string? IdiomaDispositivo { get; private set; } = null!;
    /// <summary>
    /// Propiedad que almacena el tipo del dispositivo 
    /// </summary>
    public string? TipoDispositivo { get; private set; } = null!;
    /// <summary>
    /// Propiedad que almacena el uuid del dispositivo 
    /// </summary>
    public string IdentificadorDispositivo { get; private set; } = null!;
    /// <summary>
    /// Propiedad que almacena la fecha de creacion del uuid del dispositivo 
    /// </summary>
    public DateTime? FechaCreacion { get; private set; }
    /// <summary>
    /// Propiedad que almacena la ultima fecha de modificacion del estado del uuid del dispositivo 
    /// </summary>
    public DateTime? FechaModificacion { get; private set; }
    /// <summary>
    /// Propiedad que almacena el estado del uuid del dispositivo 
    /// </summary>
    public string? EstadoDispositivo { get; private set; } = null!;

    public virtual ICollection<AfiliacionBiometrica> AfiliacionesBiometricas { get; private set; } = new List<AfiliacionBiometrica>();

    public AfiliacionBiometrica? AfiliacionBiometricaActiva =>
        AfiliacionesBiometricas.FirstOrDefault(x => x.IndicadorEstado == EstadoEntidad.ACTIVO);

    #endregion Propiedades

    #region Métodos
    public IEnumerable<AfiliacionBiometrica> AfiliacionesBiometricasActivas()
    {
        return AfiliacionesBiometricas.Where(x => x.IndicadorEstado == EstadoEntidad.ACTIVO);
    }
    #endregion
}
