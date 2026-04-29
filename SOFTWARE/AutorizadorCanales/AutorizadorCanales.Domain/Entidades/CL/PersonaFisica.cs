namespace AutorizadorCanales.Domain.Entidades.CL;

public class PersonaFisica : EntidadEmpresa
{
    public string CodigoCliente { get; private set; } = null!;
    public string DireccionCorreoElectronico { get; private set; } = null!;
    public string IndicadorSexo { get; private set; } = null!;
    public string PrimerNombre { get; private set; } = null!;
    public string SegundoNombre { get; private set; } = null!;
    public string PrimerApellido { get; private set; } = null!;
    public string? CodigoSector { get; private set; } = null!;
    public string CodigoSubActividad { get; private set; } = null!;
    public string CodigoSubSubActividad { get; private set; } = null!;
    public byte IdSectorActividad { get; private set; }
    public string? CodigoActividad { get; private set; } = null!;
    public string CodigoOcupacion { get; private set; } = null!;
    public int? CodigoCargo { get; private set; }
    public string? Nacionalidad { get; private set; } = null!;
    public string CodigoEstadoCivil { get; private set; } = null!;
    public string ApellidoCasado { get; private set; } = null!;
    public string SegundoApellido { get; private set; } = null!;
    public string? IndicadorDeclaraNoEmail { get; private set; } = null!;
    public DateTime FechaNacimiento { get; private set; }

    public string? CodigoProfesion { get; private set; } = null!;
    public string? LugarNacimiento { get; private set; } = null!;
    public string IndicadorPersonaPolitica { get; private set; } = null!;

    /// <summary>
    /// Indicador de sujeto obligado
    /// </summary>
    public string IndicadorSujetoObligado { get; private set; } = null!;

    /// <summary>
    /// Observaciones de la persona física
    /// </summary>
    public string Observaciones { get; private set; } = null!;

    /// <summary>
    /// Código de cliente del cónyuge
    /// </summary>
    public string? CodigoClienteConyugue { get; private set; } = null!;

    public virtual Cliente Cliente { get; private set; } = null!;
    public virtual Cliente Conyugue { get; private set; } = null!;

    public string SegundoApellidoCuantia
    {
        get
        {
            string[] matrimonio = { "C", "B", "P", "D" };

            string segundoApellido = SegundoApellido;

            if (matrimonio.Any(CodigoEstadoCivil.Contains) && IndicadorSexo == "F" && ApellidoCasado.Length > 0)
            {
                segundoApellido = SegundoApellido + " DE " + ApellidoCasado;
            }
            else if (CodigoEstadoCivil == "V" && IndicadorSexo == "F" && ApellidoCasado.Length > 0)
            {
                segundoApellido = SegundoApellido + " VDA. DE " + ApellidoCasado;
            }

            return segundoApellido;
        }
    }

    // Menor cuantía
    public string Nombres => PrimerNombre + " " + SegundoNombre;

    public string ApellidoPaterno => PrimerApellido;

    public string ApellidoMaterno => SegundoApellidoCuantia;
    public string TipoCliente { get; private set; } = "CL";
    public string NivelEducativo { get; private set; } = null!;
    public int? TipoPersona { get; private set; }

    public const string SECTOR_GENERAL = "00";
}