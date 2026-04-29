namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
/// <summary>
/// Clase que representa a la entidad de dominio de una Persona Fisica
/// </summary>
public class PersonaFisica : Empresa, IPersonaCuantia
{
    #region Constantes
    /// <summary>
    /// Estado civil viuda
    /// </summary>
    public const string EstadoCivilViuda = "V";
    /// <summary>
    /// Sexo femenino
    /// </summary>
    public const string SexoFemenido = "F";
    /// <summary>
    /// Divorciado
    /// </summary>
    public const string Divorciado = "D";
    /// <summary>
    /// Casado
    /// </summary>
    public const string Casado = "C";
    /// <summary>
    /// Estado desconocida con valor B
    /// </summary>
    public const string ConstanteDesconocidaB = "B";
    /// <summary>
    /// Estado desconocida con valor P
    /// </summary>
    public const string ConstanteDesconocidaP = "P";
    #endregion

    /// <summary>
    /// Codigo del cliente o
    /// </summary>
    public string CodigoCliente { get; private set; }
    /// <summary>
    /// Correo electronico
    /// </summary>
    public string DireccionCorreoElectronico { get; private set; }
    /// <summary>
    /// Indicador de sexo
    /// </summary>
    public string IndicadorSexo { get; private set; }
    /// <summary>
    /// Primer nombre
    /// </summary>
    public string PrimerNombre { get; private set; }
    /// <summary>
    /// Segundo nombre
    /// </summary>
    public string SegundoNombre { get; private set; }
    /// <summary>
    /// Primer apellido
    /// </summary>
    public string PrimerApellido { get; private set; }
    /// <summary>
    /// Codigo de sector
    /// </summary>
    public string CodigoSector { get; private set; }
    /// <summary>
    /// Codigo se sub actividad
    /// </summary>
    public string CodigoSubActividad { get; private set; }
    /// <summary>
    /// Codigo de sub sub actuvudad
    /// </summary>
    public string CodigoSubSubActividad { get; private set; }
    /// <summary>
    /// Identificador de se sector de actividad
    /// </summary>
    public byte IdSectorActividad { get; private set; }
    /// <summary>
    /// Codigo de actividad
    /// </summary>
    public string CodigoActividad { get; private set; }
    /// <summary>
    /// Codigo de ocuparacion 
    /// </summary>
    public string CodigoOcupacion { get; private set; }
    /// <summary>
    /// Codigo de cargo
    /// </summary>
    public int CodigoCargo { get; private set; }
    /// <summary>
    /// Nacionalidad
    /// </summary>
    public string Nacionalidad { get; private set; }
    /// <summary>
    /// Codigo de estado civil
    /// </summary>
    public string CodigoEstadoCivil { get; private set; }
    /// <summary>
    /// Apellido de casado
    /// </summary>
    public string ApellidoCasado { get; private set; }
    /// <summary>
    /// Segundo apellido
    /// </summary>
    public string SegundoApellido { get; private set; }
    /// <summary>
    /// Indicador de no declara email
    /// </summary>
    public string IndicadorDeclaraNoEmail { get; private set; }
    /// <summary>
    /// Fecha de nacimiento
    /// </summary>
    public DateTime FechaNacimiento { get; private set; }
    /// <summary>
    /// Codigo de profesion
    /// </summary>
    public string CodigoProfesion { get; private set; }
    /// <summary>
    /// lugar de nacimiento
    /// </summary>
    public string? LugarNacimiento { get; private set; }
    /// <summary>
    /// Indicador de persona politica
    /// </summary>
    public string IndicadorPersonaPolitica { get; private set; }
    /// <summary>
    /// Indicador de sujeto obligado
    /// </summary>
    public string IndicadorSujetoObligado { get; private set; }

    /// <summary>
    /// Observaciones de la persona física
    /// </summary>
    public string? Observaciones { get; private set; }

    /// <summary>
    /// Código de cliente del cónyuge
    /// </summary>
    public string? CodigoClienteConyugue { get; private set; }
    /// <summary>
    /// Datos del cliente
    /// </summary>
    public virtual Cliente Cliente { get; private set; }
    /// <summary>
    /// Datos del conyuge
    /// </summary>
    public virtual Cliente Conyugue { get; private set; }
    /// <summary>
    /// Segundo apellido de cuantia
    /// </summary>
    public string SegundoApellidoCuantia
    {
        get
        {
            string[] matrimonio = { Casado, ConstanteDesconocidaB, ConstanteDesconocidaP, Divorciado };

            string segundoApellido = SegundoApellido;

            if (matrimonio.Any(CodigoEstadoCivil.Contains) && IndicadorSexo == SexoFemenido && ApellidoCasado.Length > 0)
            {
                segundoApellido = SegundoApellido + " DE " + ApellidoCasado;
            }
            else if (CodigoEstadoCivil == EstadoCivilViuda && IndicadorSexo == SexoFemenido && ApellidoCasado.Length > 0)
            {
                segundoApellido = SegundoApellido + " VDA. DE " + ApellidoCasado;
            }

            return segundoApellido;
        }
    }
    /// <summary>
    /// Nombres
    /// </summary>
    public string Nombres => PrimerNombre + " " + SegundoNombre;
    /// <summary>
    /// Apellido paterno
    /// </summary>
    public string ApellidoPaterno => PrimerApellido;
    /// <summary>
    /// Apellido materno
    /// </summary>
    public string ApellidoMaterno => SegundoApellidoCuantia;
    /// <summary>
    /// Tipo de cliente
    /// </summary>
    public string TipoCliente { get; private set; } = Cliente.EsquemaCliente;
    /// <summary>
    /// Nivel adecuado
    /// </summary>
    public string NivelEducativo { get; private set; }
    /// <summary>
    /// Tipo de persona
    /// </summary>
    public int TipoPersona { get; private set; }
}

