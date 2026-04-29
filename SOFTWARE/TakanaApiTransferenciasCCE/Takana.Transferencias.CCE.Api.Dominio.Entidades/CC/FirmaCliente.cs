using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
public class FirmaCliente
{
    #region Constantes
    /// <summary>
    /// Cuenta individual
    /// </summary>
    public const string Individual = "I";
    /// <summary>
    /// Cuenta mancomunada
    /// </summary>
    public const string Mancomunada = "M";
    /// <summary>
    /// Cuenta titular
    /// </summary>
    public const string Titular = "T";
    /// <summary>
    /// Apoderado
    /// </summary>
    public const string Apoderado = "A";
    /// <summary>
    /// Representante
    /// </summary>
    public const string Representante = "R";

    #endregion Constantes

    #region Propiedades
    /// <summary>
    /// Codigo de empresa
    /// </summary>
    public string CodigoEmpresa { get; private set; }
    /// <summary>
    /// Codigo de cliente
    /// </summary>
    public string CodigoCliente { get; private set; }
    /// <summary>
    /// Numero de cuenta
    /// </summary>
    public string NumeroCuenta { get; private set; }
    /// <summary>
    /// Codigo de categoria firma
    /// </summary>
    public string CodigoCategoriaFirma { get; private set; }
    /// <summary>
    /// Codifo de tipo cliente
    /// </summary>
    public string CodigoTipoCliente { get; private set; }
    /// <summary>
    /// Indicador de tipo de firmante
    /// </summary>
    public string IndicadorTipoFirmante { get; private set; }
    /// <summary>
    /// Indicador de tipo firma
    /// </summary>
    public string IndicadorTipoFirma { get; private set; }
    /// <summary>
    /// Indicador de cero papel
    /// </summary>
    public string? IndicadorCeroPapel { get; private set; }
    /// <summary>
    /// Datos de cliente
    /// </summary>
    private Cliente _cliente;
    /// <summary>
    ///  Datos de cliente
    /// </summary>
    virtual public Cliente Cliente
    {
        get { return _cliente; }
        set
        {
            _cliente = value;
            CodigoCliente = value.CodigoCliente;
        }
    }
    /// <summary>
    /// Datos de cuenta efectivo
    /// </summary>
    private CuentaEfectivo _cuenta;
    /// <summary>
    /// Datos de cuenta efectivo
    /// </summary>
    virtual public CuentaEfectivo Cuenta
    {
        get { return _cuenta; }
        set
        {
            _cuenta = value;
            NumeroCuenta = value.NumeroCuenta;
        }
    }
    #endregion

    #region Métodos
    /// <summary>
    /// Valida si es titular
    /// </summary>
    /// <returns>True si es titular</returns>
    public bool EsTitular()
    {
        return IndicadorTipoFirmante == Titular;
    }

    /// <summary>
    /// Método que indica si es Propietario
    /// </summary>
    /// <returns></returns>
    public bool EsPropietario()
    {
        return IndicadorTipoFirma == Individual;
    }
    #endregion
}

