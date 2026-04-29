using AutorizadorCanales.Domain.Entidades.CF;
using AutorizadorCanales.Domain.Entidades.CL;

namespace AutorizadorCanales.Domain.Entidades.SG;

/// <summary>
/// Clase que representa la entidad del Usuario
/// </summary>
public class Usuario : EntidadEmpresa
{
    /// <summary>
    /// Codigo del usuario
    /// </summary>
    public string CodigoUsuario { get; private set; } = null!;
    /// <summary>
    /// Codigo de la agencia
    /// </summary>
    public string CodigoAgencia { get; private set; } = null!;
    /// <summary>
    /// Indicador habilitado
    /// </summary>
    public string? IndicadorHabilitado { get; private set; } = null!;
    /// <summary>
    /// Indicador activo
    /// </summary>
    public string IndicadorActivo { get; private set; } = null!;
    /// <summary>
    /// Nombre del usuario
    /// </summary>
    public string NombreUsuario { get; private set; } = null!;
    /// <summary>
    /// Codigo del cliente
    /// </summary>
    public string? CodigoCliente { get; private set; } = null!;
    /// <summary>
    /// Clave del usuario
    /// </summary>
    public string Clave { get; private set; } = null!;
    /// <summary>
    /// Código de Puesto
    /// </summary>
    public string CodigoPuesto { get; private set; } = null!;
    /// <summary>
    /// Coleccion de instancia de la entidad RolSeguridad
    /// </summary>
    public virtual ICollection<UsuarioRol> RolesAsignados { get; set; } = new List<UsuarioRol>();
    /// <summary>
    /// <summary>
    /// Coleccion de instancia de la entidad AccesosPorSistema
    /// </summary>
    public virtual ICollection<AccesosPorSistema> Accesos { get; set; } = new List<AccesosPorSistema>();
    /// <summary>
    /// Instancia de la entidad Agencia
    /// </summary>
    public virtual Agencia Agencia { get; private set; }
    /// <summary>
    /// Cliente
    /// </summary>
    public virtual Cliente Cliente { get; private set; }
    /// <summary>
    /// Metodo para validar el acceso al sistema
    /// </summary>
    /// <param name="sistema"></param>
    /// <returns></returns>
    public bool ValidaAccesoSistema(string sistema)
    {
        foreach (var acceso in Accesos)
        {
            if (acceso.CodigoSistema == sistema)
                return true;
        }
        return false;
    }

    /// <summary>
    /// Validar si puede realizar operaciones
    /// </summary>
    public bool PuedeRealizarOperaciones => IndicadorActivo == "A" && IndicadorHabilitado == "A";

    /// <summary>
    /// Metodo para obtener roles del usuario
    /// </summary>
    /// <returns></returns>
    public List<string> ObtenerRoles()
    {
        return RolesAsignados.Select(g => g.CodigoRol).ToList();
    }
}
