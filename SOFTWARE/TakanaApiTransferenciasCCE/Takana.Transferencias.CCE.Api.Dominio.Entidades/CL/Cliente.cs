using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.PA;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

/// <summary>
/// Clase que representa a la entidad de dominio Cliente
/// </summary>
public class Cliente : Empresa, IInterviniente
{
    #region Constantes
    /// <summary>
    /// Constante Mismo Titular
    /// </summary>
    public const string MismoTitular = "1";
    /// <summary>
    /// Constante Otro Titular
    /// </summary>
    public const string OtroTitular = "0";
    /// <summary>
    /// Constante Tipo de Persona Natural
    /// </summary>
    public const string TipoPersonaNatural = "F";

    /// <summary>
    /// Constante Tipo de Persona Juridica
    /// </summary>
    public const string TipoPersonaJuridica = "J";

    /// <summary>
    /// Constante Tipo de Persona Juridica
    /// </summary>
    public const string CodigoPaisPeru = "4028";
    /// <summary>
    /// Constante Tipo de direccion de casa
    /// </summary>
    public const string DireccionTipoCasa = "C";
    /// <summary>
    /// Esquema cliente
    /// </summary>
    public const string EsquemaCliente = "CL";
    /// <summary>
    /// Funcionarioa
    /// </summary>
    public const string Funcionario = "F";
    /// <summary>
    /// Trabajador
    /// </summary>
    public const string Trabajador = "T";
    /// <summary>
    /// Constante desconocida D
    /// </summary>
    public const string ConstanteDesconocidaD = "D";
    #endregion Constantes

    #region Propiedades

    /// <summary>
    /// Propiedad que representa el Codigo del cliente
    /// </summary>
    public string CodigoCliente { get; private set; }

    /// <summary>
    /// Propiedad que representa el nombre del cliente
    /// </summary>
    public string NombreCliente { get; private set; }

    /// <summary>
    /// Propiedad que representa el tipo de persona
    /// </summary>
    public string TipoPersona { get; private set; }

    /// <summary>
    /// Propiedad que representa el número de telefono secundario
    /// </summary>
    public string? NumeroTelefonoSecundario { get; private set; }

    /// <summary>
    /// Propiedad que representa el correo electrónico
    /// </summary>
    public string? DireccionCorreoElectronico { get; private set; }

    /// <summary>
    /// Propiedad que reprsenta el Tipo de Relación del Cliente
    /// </summary>
    public string CodigoTipoRelacion { get; private set; }

    /// <summary>
    /// Propiedad que representa la colección de documentos del cliente
    /// </summary>
    public virtual ICollection<DocumentoCliente> Documentos { get; private set; } = new List<DocumentoCliente>();

    /// <summary>
    /// Propiedad que representa la colección de direcciones del cliente
    /// </summary>
    public virtual ICollection<DireccionCliente> Direcciones { get; private set; } = new List<DireccionCliente>();

    /// <summary>
    /// Propiedad que indica si el cliente es trabajador
    /// </summary>
    public bool EsTrabajador => CodigoTipoRelacion == Trabajador
        || CodigoTipoRelacion == Funcionario || CodigoTipoRelacion == ConstanteDesconocidaD;

    /// <summary>
    /// Propiedad que representa el número de telefono Principal
    /// </summary>
    public string? NumeroTelefonoPrincipal { get; private set; }

    /// <summary>
    /// Propiedad que representa el número de telefono otro
    /// </summary>
    public string? NumeroTelefonoOtro { get; private set; }

    /// <summary>
    /// Propiedad que representa el número celular, especifico para Cero Papel
    /// </summary>
    public string? NumeroTelefonoCelular { get; private set; }

    /// <summary>
    /// Propiedad que representa el la categoria del cliente
    /// </summary>
    public string? CategoriaCliente { get; private set; }

    /// <summary>
    /// Propiedad que representa el codigo de pais
    /// </summary>
    public string? CodigoPais { get; private set; }

    /// <summary>
    /// Propiedad que representa el codigo de pais residencia
    /// </summary>
    public string CodigoPaisResidencia { get; private set; }

    /// <summary>
    /// Propiedad que representa el codigo de lugar actividad
    /// </summary>
    public string? CodigoLugarActividad { get; private set; }

    /// <summary>
    /// Propiedad que representa la fecha de modificacion
    /// </summary>
    public DateTime? FechaModificacion { get; private set; }

    /// <summary>
    /// Propiedad que representa la fecha de confirmacion cuando se actualizan datos desde takama
    /// </summary>
    public DateTime? FechaConfirmacion { get; private set; }
    /// <summary>
    /// Codigo de usuario
    /// </summary>
    public string? CodigoUsuario { get; private set; }
    /// <summary>
    /// Propiedad que indica el uso de datos personales
    /// </summary>
    public string? IndicadorUsoDatosPersonales { get; private set; }

    /// <summary>
    /// País al que pertenece el cliente
    /// </summary>
    public virtual Nacion Nacion { get; private set; }

    /// <summary>
    /// Documento por defecto del cliente para generar cartillas en aperturas.
    /// </summary>
    public DocumentoCliente DocumentoCartillaPorDefecto
    {
        get
        {
            return
                Documentos.OrderBy(
                    d =>
                        TipoPersona == TipoPersonaNatural
                            ? d.TipoDocumento.IndicadorPrioridadPersonaNatural
                            : d.TipoDocumento.IndicadorPrioridadPersonaJuridica).FirstOrDefault();
        }
    }

    /// <summary>
    /// Direccion por defecto de un cliente.
    /// </summary>
    public DireccionCliente DireccionPorDefecto
    {
        get
        {
            return
                Direcciones.OrderBy(
                    d =>
                        TipoPersona == TipoPersonaNatural
                            ? d.TipoDireccion.IndicadorPrioridadNatural
                            : d.TipoDireccion.IndicadorPrioridadJuridica).FirstOrDefault();
        }
    }
    /// <summary>
    /// Tipo de cliente
    /// </summary>
    public string TipoCliente => TipoPersona == TipoPersonaNatural ? "FISICA" : "JURIDICA";

    /// <summary>
    /// Fecha de ingreso
    /// </summary>
    public DateTime? FechaIngreso { get; private set; }

    /// <summary>
    /// Código de residencia
    /// </summary>
    public int? CodigoResidencia { get; private set; }

    /// <summary>
    /// Código de agencia
    /// </summary>
    public string? CodigoAgencia { get; private set; }
    /// <summary>
    /// Persona Fisica
    /// </summary>
    public virtual PersonaFisica PersonaFisica { get; private set; }
    /// <summary>
    /// Persona Juridica
    /// </summary>
    public virtual PersonaJuridica PersonaJuridica { get; private set; }
    /// <summary>
    /// Persona Juridica
    /// </summary>
    public virtual ICollection<Afiliado> Afiliaciones { get; private set; }
    /// <summary>
    /// Obtiene el documento RUC del cliente.
    /// </summary>
    public DocumentoCliente DocumentoRuc
        => Documentos.FirstOrDefault(f => f.EsRUC);

    #endregion Propiedades

    #region IInterviniente
    /// <summary>
    /// Codigo de tipo interviniente
    /// </summary>
    public int CodigoTipoInterviniente => throw new NotImplementedException();
    /// <summary>
    /// Codigo de tipo documento
    /// </summary>
    public string CodigoTipoDocumento => DocumentoCartillaPorDefecto.CodigoTipoDocumento;
    /// <summary>
    /// Numero de documento
    /// </summary>
    public string NumeroDocumento => DocumentoCartillaPorDefecto.NumeroDocumento;
    /// <summary>
    /// Valida si es cliente o no
    /// </summary>
    public bool EsCliente => true;
    /// <summary>
    /// Apellido parterno del cliente
    /// </summary>
    public string ApellidoPaterno => TipoPersona == TipoPersonaNatural ? PersonaFisica?.ApellidoPaterno! : PersonaJuridica.RazonSocial;
    /// <summary>
    /// Apellido materno del cliente
    /// </summary>
    public string ApellidoMaterno => TipoPersona == TipoPersonaNatural ? PersonaFisica?.ApellidoMaterno! : string.Empty;
    /// <summary>
    /// Nombres del cliente
    /// </summary>
    public string Nombres => TipoPersona == TipoPersonaNatural ? PersonaFisica?.Nombres! : string.Empty;
    /// <summary>
    /// Nomre completo del cliente
    /// </summary>
    public string NombreCompletoCliente => ApellidoPaterno + " " + ApellidoMaterno + ", " + Nombres;
    /// <summary>
    /// Nombres y apellidos del cliente
    /// </summary>
    public string NombresApellidosCliente => TipoPersona == TipoPersonaNatural ?
                                            PersonaFisica.Nombres + " " + PersonaFisica.ApellidoPaterno + " " + ApellidoMaterno
                                            : PersonaJuridica.RazonSocial;
    /// <summary>
    /// Indicador es cliente natural
    /// </summary>
    public bool EsClienteNatural => TipoPersona.Trim() == TipoPersonaNatural;
    /// <summary>
    /// Indicador es cliente juridico
    /// </summary>
    public bool EsClienteJuridico => TipoPersona.Trim() == TipoPersonaJuridica;
    #endregion

    #region Metodos

    /// <summary>
    /// Obtiene la persona para realizar operaciones con cuantia.
    /// </summary>
    /// <returns>Interfaz de cuantia.</returns>
    public IPersonaCuantia ObtenerPersonaCuantia()
    {
        switch (TipoPersona)
        {
            case TipoPersonaNatural:
                return PersonaFisica;
            case TipoPersonaJuridica:
                return PersonaJuridica;
            default:
                throw new ValidacionException("No es valido el tipo persona.");
        }
    }

    #endregion Metodos
}