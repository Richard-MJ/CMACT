using Takana.Transferencias.CCE.Api.Common.DTOs.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.PA;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CJ;

/// <summary>
/// Clase de dominio que representa el detalle de la operación única detalle
/// </summary>
public class OperacionUnicaDetalle : ILavadoInterviniente
{
    #region Consntantes
    /// <summary>
    /// Operacion Activa
    /// </summary>
    public const string Activo = "A";
    #endregion Consntantes

    #region Propiedades
    /// <summary>
    /// Representa el número del detalle de la operación de lavado
    /// </summary>
    public int NumeroSecuencia { get; private set; }
    /// <summary>
    /// Representa el número de operación de lavado
    /// </summary>
    public decimal? NumeroMovimientoLavado { get; private set; }
    /// <summary>
    /// Representa el tipo de cliente
    /// </summary>
    public string TipoCliente { get; private set; }
    /// <summary>
    /// Representa el apellido paterno o razón social
    /// </summary>
    public string ApellidoPaterno { get; private set; }
    /// <summary>
    /// Representa el apellido materno
    /// </summary>
    public string ApellidoMaterno { get; private set; }
    /// <summary>
    /// Representa el nombre
    /// </summary>
    public string Nombres { get; private set; }
    /// <summary>
    /// Código del cliente
    /// </summary>
    public string CodigoCliente { get; private set; }
    /// <summary>
    /// Tipo de Interviniente
    /// </summary>
    public string TipoInterviniente { get; private set; }
    /// <summary>
    /// Tipo de documento
    /// </summary>
    public string TipoDocumento { get; private set; }
    /// <summary>
    /// Tipo de persona
    /// </summary>
    public string TipoPersona { get; private set; }
    /// <summary>
    /// Número de RUC
    /// </summary>
    public string NumeroRuc { get; private set; }
    /// <summary>
    /// Numero de documento
    /// </summary>
    public string NumeroDocumento { get; private set; }
    /// <summary>
    /// Fecha de nacimiento
    /// </summary>
    public DateTime? FechaNacimiento { get; private set; }
    /// <summary>
    /// Nacionalidad
    /// </summary>
    public string Nacionalidad { get; private set; }
    /// <summary>
    /// Codigo de residencia
    /// </summary>
    public int? CodigoResidencia { get; private set; }
    /// <summary>
    /// Detalle de direccion
    /// </summary>
    public string DetalleDireccion { get; private set; }
    /// <summary>
    /// Telefono
    /// </summary>
    public string Telefono { get; private set; }
    /// <summary>
    /// Codigo de ocupacion
    /// </summary>
    public string CodigoOcupacion { get; private set; }
    /// <summary>
    /// Codigo de actividad
    /// </summary>
    public string CodigoActividad { get; private set; }
    /// <summary>
    /// Codigo de sub actividad
    /// </summary>
    public string CodigoSubactividad { get; private set; }
    /// <summary>
    /// Codigo de cargo
    /// </summary>
    public int? CodigoCargo { get; private set; }
    /// <summary>
    /// Codigo de departamento
    /// </summary>
    public string CodigoDepartamento { get; private set; }
    /// <summary>
    /// Codigo de provincia
    /// </summary>
    public string CodigoProvincia { get; private set; }
    /// <summary>
    /// Codigo de distrito
    /// </summary>
    public string CodigoDistrito { get; private set; }
    /// <summary>
    /// Indicador de estado
    /// </summary>
    public string IndicadorEstado { get; private set; }
    /// <summary>
    /// Codigo de usuario
    /// </summary>
    public string CodigoUsuario { get; private set; }
    /// <summary>
    /// Fecha de servidor
    /// </summary>
    public DateTime FechaServidor { get; private set; }
    /// <summary>
    /// Codigo de pais residencia
    /// </summary>
    public string CodigoPaisResidencia { get; private set; }
    #endregion

    #region Métodos
    /// <summary>
    /// Genera detalle de operacion unica de persona fisica
    /// </summary>
    /// <param name="numeroLavado"></param>
    /// <param name="personaFisica"></param>
    /// <param name="codigoUsuario"></param>
    /// <param name="inteviniente"></param>
    /// <returns>Retorna detalle de operacion unica</returns>
    public static OperacionUnicaDetalle GenerarDesdePersonaFisica(decimal numeroLavado, PersonaFisica personaFisica,
        string codigoUsuario, TipoInteviniente inteviniente, DateTime fechaSistema)
    {
        var direccion = personaFisica.Cliente.DireccionPorDefecto;
        var documento = personaFisica.Cliente.DocumentoCartillaPorDefecto;
        var telefono = string.Empty;
        telefono += !string.IsNullOrWhiteSpace(personaFisica.Cliente.NumeroTelefonoPrincipal)
            ? (personaFisica.Cliente.NumeroTelefonoPrincipal + "/") : string.Empty;
        telefono += !string.IsNullOrWhiteSpace(personaFisica.Cliente.NumeroTelefonoSecundario)
            ? (personaFisica.Cliente.NumeroTelefonoSecundario + "/") : string.Empty;
        telefono += !string.IsNullOrWhiteSpace(personaFisica.Cliente.NumeroTelefonoOtro)
            ? (personaFisica.Cliente.NumeroTelefonoOtro + "/") : string.Empty;
        if (!string.IsNullOrWhiteSpace(telefono))
            telefono = telefono.Substring(0, telefono.Length - 1);

        return new OperacionUnicaDetalle()
        {
            ApellidoMaterno = personaFisica.ApellidoMaterno,
            ApellidoPaterno = personaFisica.ApellidoPaterno,
            CodigoActividad = personaFisica.CodigoActividad,
            CodigoCargo = personaFisica.CodigoCargo,
            CodigoCliente = personaFisica.CodigoCliente,
            CodigoDepartamento = direccion.CodigoPais,
            CodigoDistrito = direccion.CodigoCanton,
            CodigoProvincia = direccion.CodigoProvincia,
            CodigoOcupacion = personaFisica.CodigoOcupacion,
            CodigoSubactividad = personaFisica.CodigoSubActividad,
            CodigoUsuario = codigoUsuario,
            DetalleDireccion = direccion.DetalleDireccion,
            FechaNacimiento = personaFisica.FechaNacimiento,
            FechaServidor = fechaSistema,
            IndicadorEstado = Activo,
            Nacionalidad = personaFisica.Nacionalidad,
            Nombres = personaFisica.Nombres,
            NumeroDocumento = documento.NumeroDocumento,
            NumeroMovimientoLavado = numeroLavado,
            Telefono = telefono,
            TipoCliente = personaFisica.TipoCliente,
            TipoDocumento = documento.TipoDocumento.CodigoTipoDocumento,
            TipoInterviniente = ((int)inteviniente).ToString(),
            TipoPersona = personaFisica.TipoPersona.ToString(),
            CodigoPaisResidencia = personaFisica.Cliente.CodigoPaisResidencia,
            CodigoResidencia = personaFisica.Cliente.CodigoResidencia,
            NumeroRuc = personaFisica.Cliente.DocumentoRuc?.NumeroDocumento
        };
    }
    /// <summary>
    /// Método que genera detalle de operacion unica de persona juridica
    /// </summary>
    /// <param name="numeroLavado"></param>
    /// <param name="personaJuridica"></param>
    /// <param name="codigoUsuario"></param>
    /// <param name="inteviniente"></param>
    /// <returns>Retorna detalle de operacion unica</returns>
    public static OperacionUnicaDetalle GenerarDesdePersonaJuridica(
        decimal numeroLavado, PersonaJuridica personaJuridica,
        string codigoUsuario, TipoInteviniente inteviniente, DateTime fechaSistema)
    {
        var direccion = personaJuridica.Cliente.Direcciones.First();
        var documento = personaJuridica.Cliente.DocumentoCartillaPorDefecto;
        var telefono = string.Empty;
        telefono += !string.IsNullOrWhiteSpace(personaJuridica.Cliente.NumeroTelefonoPrincipal)
            ? (personaJuridica.Cliente.NumeroTelefonoPrincipal + "/") : string.Empty;
        telefono += !string.IsNullOrWhiteSpace(personaJuridica.Cliente.NumeroTelefonoSecundario)
            ? (personaJuridica.Cliente.NumeroTelefonoSecundario + "/") : string.Empty;
        telefono += !string.IsNullOrWhiteSpace(personaJuridica.Cliente.NumeroTelefonoOtro)
            ? (personaJuridica.Cliente.NumeroTelefonoOtro + "/") : string.Empty;
        if (!string.IsNullOrWhiteSpace(telefono))
            telefono = telefono.Substring(0, telefono.Length - 1);

        return new OperacionUnicaDetalle()
        {
            NumeroMovimientoLavado = numeroLavado,
            TipoInterviniente = ((int)inteviniente).ToString(),
            TipoCliente = personaJuridica.TipoCliente,
            CodigoCliente = personaJuridica.CodigoCliente,
            TipoDocumento = documento.TipoDocumento.CodigoTipoDocumento,
            NumeroDocumento = documento.NumeroDocumento,
            TipoPersona = 0.ToString(),
            FechaNacimiento = personaJuridica.Cliente.FechaIngreso,
            ApellidoPaterno = personaJuridica.RazonSocial,
            ApellidoMaterno = string.Empty,
            Nombres = string.Empty,
            CodigoResidencia = personaJuridica.Cliente?.CodigoResidencia,
            Nacionalidad = Nacion.NombrePeru,
            DetalleDireccion = direccion.DetalleDireccion,
            Telefono = telefono,
            IndicadorEstado = Activo,
            CodigoUsuario = codigoUsuario,
            FechaServidor = fechaSistema,
            CodigoPaisResidencia = personaJuridica.Cliente?.CodigoPaisResidencia 
                ?? string.Empty,
            CodigoActividad = personaJuridica.CodigoActividad,
            CodigoSubactividad = personaJuridica.CodigoSubactividad,
            CodigoDepartamento = direccion.CodigoPais,
            CodigoDistrito = direccion.CodigoCanton,
            CodigoProvincia = direccion.CodigoProvincia,
            CodigoOcupacion = string.Empty,
            CodigoCargo = 0,
            NumeroRuc = documento.NumeroDocumento
        };
    }
    /// <summary>
    /// Genera detalle de operacion unica con cliente externo
    /// </summary>
    /// <param name="numeroLavado"></param>
    /// <param name="clienteOriginante"></param>
    /// <param name="codigoInteviniente"></param>
    /// <param name="fechaSistema"></param>
    /// <returns>Retorna detalle de operacion unica</returns>
    public static OperacionUnicaDetalle GenerarIntervinienteConClienteExterno(
        decimal numeroLavado,
        ClienteExternoDTO clienteOriginante,
        int codigoInteviniente, 
        DateTime fechaSistema)
    {
        return new OperacionUnicaDetalle()
        {
            NumeroMovimientoLavado = numeroLavado,
            CodigoCliente = clienteOriginante.CodigoCliente ?? string.Empty,
            TipoCliente = clienteOriginante.TipoCliente,
            TipoDocumento = clienteOriginante.CodigoTipoDocumento,
            NumeroDocumento = clienteOriginante.NumeroDocumento,
            ApellidoPaterno = clienteOriginante.EsClienteExterno
                ? clienteOriginante.Nombres
                : clienteOriginante.ApellidoPaterno ?? string.Empty,

            ApellidoMaterno = clienteOriginante.EsPersonaJuridica
                ? string.Empty
                : clienteOriginante.ApellidoMaterno ?? string.Empty,

            Nombres = clienteOriginante.EsPersonaJuridica || clienteOriginante.EsClienteExterno
                ? string.Empty
                : clienteOriginante.Nombres ?? string.Empty,

            IndicadorEstado = Activo,
            TipoInterviniente = codigoInteviniente.ToString(),
            FechaNacimiento = clienteOriginante.FechaNacimiento,
            Nacionalidad = clienteOriginante.Nacionalidad ?? string.Empty,
            CodigoResidencia = clienteOriginante.CodigoResidencia,
            CodigoPaisResidencia = Nacion.Peru,
            Telefono = clienteOriginante.Telefono ?? string.Empty,
            FechaServidor = fechaSistema,
            CodigoDepartamento = clienteOriginante.CodigoDepartamento ?? string.Empty,
            CodigoDistrito = clienteOriginante.CodigoDistrito ?? string.Empty,
            CodigoProvincia = clienteOriginante.CodigoProvincia ?? string.Empty,
            CodigoOcupacion = clienteOriginante.CodigoOcupacion ?? string.Empty,
            CodigoCargo = clienteOriginante.CodigoCargo,
            CodigoActividad = clienteOriginante.CodigoActividad ?? string.Empty,
            CodigoUsuario = clienteOriginante.CodigoUsuario ?? string.Empty,
            CodigoSubactividad = clienteOriginante.CodigoSubactividad ?? string.Empty,
            DetalleDireccion = clienteOriginante.DetalleDireccion ?? string.Empty,
            NumeroRuc = clienteOriginante.NumeroDocumentoRuc ?? string.Empty,
            TipoPersona = clienteOriginante.TipoPersona
        };
    }
    #endregion
}