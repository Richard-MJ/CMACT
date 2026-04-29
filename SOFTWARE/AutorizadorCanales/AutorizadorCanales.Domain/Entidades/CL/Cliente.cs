using System.ComponentModel.DataAnnotations;
using System;
using AutorizadorCanales.Excepciones;

namespace AutorizadorCanales.Domain.Entidades.CL;

public class Cliente : EntidadEmpresa
{
    #region Constantes
    /// <summary>
    /// Constante Tipo de Persona Natural
    /// </summary>
    public const string TIPO_PERSONA_NATURAL = "F";
    /// <summary>
    /// Constante Tipo de Persona Juridica
    /// </summary>
    public const string TIPO_PERSONA_JURIDICA = "J";
    /// <summary>
    /// Constante Tipo de Persona Juridica
    /// </summary>
    public const string CODIGO_PAIS_PERU = "4028";
    /// <summary>
    /// Constante Tipo de direccion de casa
    /// </summary>
    public const string DIRECCION_TIPO_CASA = "C";

    #endregion Constantes

    #region Propiedades

    /// <summary>
    /// Propiedad que representa el Codigo del cliente
    /// </summary>
    public string CodigoCliente { get; private set; } = null!;
    /// <summary>
    /// Propiedad que representa el nombre del cliente
    /// </summary>
    public string NombreCliente { get; private set; } = null!;
    /// <summary>
    /// Propiedad que representa el tipo de persona
    /// </summary>
    public string TipoPersona { get; private set; } = null!;
    /// <summary>
    /// Propiedad que representa el número de telefono secundario
    /// </summary>
    public string? NumeroTelefonoSecundario { get; private set; } = null!;
    /// <summary>
    /// Propiedad que representa el correo electrónico
    /// </summary>
    public string? DireccionCorreoElectronico { get; private set; } = null!;
    /// <summary>
    /// Propiedad que reprsenta el Tipo de Relación del Cliente
    /// </summary>
    public string CodigoTipoRelacion { get; private set; } = null!;
    /// <summary>
    /// Propiedad que representa la colección de documentos del cliente
    /// </summary>
    public virtual ICollection<DocumentoCliente> Documentos { get; private set; } = new List<DocumentoCliente>();
    /// <summary>
    /// Propiedad que indica si el cliente es trabajador
    /// </summary>
    public bool EsTrabajador => CodigoTipoRelacion == "T" || CodigoTipoRelacion == "F" || CodigoTipoRelacion == "D";
    /// <summary>
    /// Propiedad que representa el número de telefono Principal
    /// </summary>
    public string? NumeroTelefonoPrincipal { get; private set; } = null!;
    /// <summary>
    /// Propiedad que representa el número de telefono otro
    /// </summary>
    public string? NumeroTelefonoOtro { get; private set; } = null!;
    /// <summary>
    /// Propiedad que representa el número celular, especifico para Cero Papel
    /// </summary>
    public string? NumeroTelefonoCelular { get; private set; } = null!;
    /// <summary>
    /// Propiedad que representa el la categoria del cliente
    /// </summary>
    public string CategoriaCliente { get; private set; } = null!;
    /// <summary>
    /// Propiedad que representa el codigo de pais
    /// </summary>
    public string CodigoPais { get; private set; } = null!;
    /// <summary>
    /// Propiedad que representa el codigo de pais residencia
    /// </summary>
    public string CodigoPaisResidencia { get; private set; } = null!;
    /// <summary>
    /// Propiedad que representa el codigo de lugar actividad
    /// </summary>
    public string? CodigoLugarActividad { get; private set; } = null!;
    /// <summary>
    /// Propiedad que representa la fecha de modificacion
    /// </summary>
    public DateTime? FechaModificacion { get; private set; } = null!;
    /// <summary>
    /// Propiedad que representa la fecha de confirmacion cuando se actualizan datos desde takama
    /// </summary>
    public DateTime? FechaConfirmacion { get; private set; }

    public string? CodigoUsuario { get; private set; } = null!;
    /// <summary>
    /// Propiedad que indica el uso de datos personales
    /// </summary>
    public string? IndicadorUsoDatosPersonales { get; private set; } = null!;
    /// Fecha de ingreso
    /// </summary>
    public DateTime? FechaIngreso { get; set; }
    /// <summary>
    /// Código de residencia
    /// </summary>
    public int? CodigoResidencia { get; set; }
    /// <summary>
    /// Código de agencia
    /// </summary>
    public string? CodigoAgencia { get; set; }
    /// <summary>
    /// Persona Fisica
    /// </summary>
    public virtual PersonaFisica? PersonaFisica { get; private set; } = null!;
    /// <summary>
    /// Persona Juridica
    /// </summary>
    public virtual PersonaJuridica? PersonaJuridica { get; private set; } = null!;
    /// <summary>
    /// Obtiene el documento RUC del cliente.
    /// </summary>
    #endregion Propiedades

    #region Calculados
    public string? DireccionCorreoElectronicoDefecto =>
        PersonaFisica != null 
            && !string.IsNullOrEmpty(PersonaFisica.DireccionCorreoElectronico) 
            && !string.IsNullOrWhiteSpace(PersonaFisica.DireccionCorreoElectronico)
        ? PersonaFisica.DireccionCorreoElectronico
        : DireccionCorreoElectronico;

    public DocumentoCliente ObtenerTipoDocumento(string codigoTipoDocumento) =>
        Documentos.FirstOrDefault(d => d.CodigoTipoDocumento == codigoTipoDocumento)
            ?? throw ExcepcionAUsuario.ExcepcionAfiliacionInicioSesion();
    #endregion
}
