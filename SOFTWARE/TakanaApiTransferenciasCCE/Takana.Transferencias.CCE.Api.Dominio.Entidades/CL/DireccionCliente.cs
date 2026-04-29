using Takana.Transferencias.CCE.Api.Dominio.Entidades.PA;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

/// <summary>
/// Clase que representa a la entidad de dominio Direccion del Cliente
/// </summary>
public class DireccionCliente : Empresa
{
    /// <summary>
    /// Codigo de direccion del cliente
    /// </summary>
    public string CodigoDireccion { get;  private set; }
    /// <summary>
    /// Codigo del cliente
    /// </summary>
    public string CodigoCliente { get;  private set; }
    /// <summary>
    /// Codigo de tipo direccion
    /// </summary>
    public string CodigoTipoDireccion { get; private set; }
    /// <summary>
    /// Detalle de la direccion
    /// </summary>
    public string DetalleDireccion { get; private set; }
    /// <summary>
    /// Codigo de pais
    /// </summary>
    public string CodigoPais { get; private set; }
    /// <summary>
    /// Codigo de provincia
    /// </summary>
    public string CodigoProvincia { get; private set; }
    /// <summary>
    /// Codigo de canton
    /// </summary>
    public string CodigoCanton { get; private set; }
    /// <summary>
    /// Codigo del distrito
    /// </summary>
    public string CodigoDistrito { get; private set; }
    /// <summary>
    /// Codigo de la via
    /// </summary>
    public string CodigoVia { get; private set; }
    /// <summary>
    /// Codigo de la zona
    /// </summary>
    public string CodigoZona { get; private set; }
    /// <summary>
    /// Nombr de la via
    /// </summary>
    public string NombreVia { get; private set; }
    /// <summary>
    /// Nombre de la zona
    /// </summary>
    public string NombreZona { get; private set; }
    /// <summary>
    /// Referencia
    /// </summary>
    public string Referencia { get; private set; }

    /// <summary>
    /// Distrito de la dirección del cliente
    /// </summary>
    public virtual Cantones Distrito { get; private set; }
    /// <summary>
    /// Provincia de la dirección del cliente
    /// </summary>
    public virtual Provincia Provincia { get; private set; }
    /// <summary>
    /// Datos del tipo de direccion
    /// </summary>
    public virtual TipoDireccion TipoDireccion { get; private set; } = new TipoDireccion();
    /// <summary>
    /// Datos del cliente
    /// </summary>
    public virtual Cliente Cliente { get; private set; }
    
}