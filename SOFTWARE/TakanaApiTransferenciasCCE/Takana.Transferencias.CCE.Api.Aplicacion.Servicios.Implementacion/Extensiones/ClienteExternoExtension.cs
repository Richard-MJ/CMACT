using Takana.Transferencias.CCE.Api.Common.DTOs.CL;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Extensiones
{
    public static class ClienteExternoExtension
    {
        /// <summary>
        /// Extension de cliente origen existente en CMAC TACNA
        /// </summary>
        /// <param name="codigoCuentaInterbancario"></param>
        /// <param name="datosClienteOrigen"></param>
        /// <returns>Retorna datos de cliente</returns>
        public static ClienteExternoDTO ClienteExistente(
            this string codigoCuentaInterbancario,
            IPersonaCuantia datosClienteOrigen)
        {
            var telefono = string.Empty;
            telefono += !string.IsNullOrWhiteSpace(datosClienteOrigen.Cliente.NumeroTelefonoPrincipal)
                ? (datosClienteOrigen.Cliente.NumeroTelefonoPrincipal + "/") : string.Empty;
            telefono += !string.IsNullOrWhiteSpace(datosClienteOrigen.Cliente.NumeroTelefonoSecundario)
                ? (datosClienteOrigen.Cliente.NumeroTelefonoSecundario + "/") : string.Empty;
            telefono += !string.IsNullOrWhiteSpace(datosClienteOrigen.Cliente.NumeroTelefonoOtro)
                ? (datosClienteOrigen.Cliente.NumeroTelefonoOtro + "/") : string.Empty;
            if (!string.IsNullOrWhiteSpace(telefono))
                telefono = telefono.Substring(0, telefono.Length - 1);
            return new ClienteExternoDTO()
            {
                CodigoCliente = datosClienteOrigen.CodigoCliente,
                CodigoCuentaInterbancaria = codigoCuentaInterbancario,
                Nombres = string.IsNullOrEmpty(datosClienteOrigen.Nombres)
                    ? datosClienteOrigen.Cliente.NombreCompletoCliente
                    : datosClienteOrigen.Nombres,
                ApellidoPaterno = datosClienteOrigen.ApellidoPaterno,
                ApellidoMaterno = datosClienteOrigen.ApellidoMaterno,
                NumeroDocumento = datosClienteOrigen.Cliente.NumeroDocumento,
                CodigoTipoDocumento = datosClienteOrigen.Cliente.DocumentoCartillaPorDefecto.CodigoTipoDocumento.Trim(),
                Nacionalidad = datosClienteOrigen.Cliente.CodigoPais?.Trim() ?? string.Empty,
                TipoPersona = datosClienteOrigen.Cliente.TipoPersona == Cliente.TipoPersonaNatural
                    ? ((int)ClienteExternoDTO.Persona.Natural).ToString() : ((int)ClienteExternoDTO.Persona.Juridica).ToString(),
                Telefono = telefono,
                CodigoResidencia = datosClienteOrigen.Cliente.CodigoResidencia,
                CodigoCargo = datosClienteOrigen.Cliente.PersonaFisica?.CodigoCargo ?? 0,
                CodigoDepartamento = datosClienteOrigen.Cliente.DireccionPorDefecto.CodigoPais.Trim().Length > 2 ?
                    datosClienteOrigen.Cliente.DireccionPorDefecto.CodigoPais.Trim()
                    .Substring(datosClienteOrigen.Cliente.DireccionPorDefecto.CodigoPais.Length - 2) :
                    datosClienteOrigen.Cliente.DireccionPorDefecto.CodigoPais.Trim(),
                CodigoDistrito = datosClienteOrigen.Cliente.DireccionPorDefecto.CodigoCanton.Trim().Length > 2 ?
                    datosClienteOrigen.Cliente.DireccionPorDefecto.CodigoCanton.Trim()
                    .Substring(datosClienteOrigen.Cliente.DireccionPorDefecto.CodigoCanton.Length - 2) :
                    datosClienteOrigen.Cliente.DireccionPorDefecto.CodigoCanton.Trim(),
                CodigoProvincia = datosClienteOrigen.Cliente.DireccionPorDefecto.CodigoProvincia.Trim().Length > 2 ?
                    datosClienteOrigen.Cliente.DireccionPorDefecto.CodigoProvincia.Trim()
                    .Substring(datosClienteOrigen.Cliente.DireccionPorDefecto.CodigoProvincia.Length - 2) :
                    datosClienteOrigen.Cliente.DireccionPorDefecto.CodigoProvincia.Trim(),
                FechaNacimiento = datosClienteOrigen.Cliente.PersonaFisica?.FechaNacimiento,
                CodigoOcupacion = datosClienteOrigen.Cliente.PersonaFisica?.CodigoOcupacion ?? string.Empty,
                CodigoActividad = datosClienteOrigen.Cliente.PersonaFisica?.CodigoActividad ?? string.Empty,
                NumeroDocumentoRuc = datosClienteOrigen.Cliente.DocumentoRuc?.NumeroDocumento ?? string.Empty,
                CodigoUsuario = datosClienteOrigen.Cliente.CodigoUsuario ?? General.UsuarioPorDefecto,
                CodigoSubactividad = datosClienteOrigen.Cliente.PersonaFisica?.CodigoSubActividad ?? string.Empty,
                DetalleDireccion = datosClienteOrigen.Cliente.DireccionPorDefecto.DetalleDireccion ?? string.Empty,
                TipoCliente = ClienteExternoDTO.Cliente
            };
        }

        /// <summary>
        /// Extension de cliente origen existente en CMAC TACNA
        /// </summary>
        /// <param name="codigoCuentaInterbancario"></param>
        /// <param name="datosClienteOrigen"></param>
        /// <returns>Retorna datos de cliente</returns>
        public static ClienteExternoDTO ClienteExistente(
            this string numeroCuenta,
            Cliente datosClienteOrigen)
        {
            var telefono = string.Empty;
            telefono += !string.IsNullOrWhiteSpace(datosClienteOrigen.NumeroTelefonoPrincipal)
                ? (datosClienteOrigen.NumeroTelefonoPrincipal + "/") : string.Empty;
            telefono += !string.IsNullOrWhiteSpace(datosClienteOrigen.NumeroTelefonoSecundario)
                ? (datosClienteOrigen.NumeroTelefonoSecundario + "/") : string.Empty;
            telefono += !string.IsNullOrWhiteSpace(datosClienteOrigen.NumeroTelefonoOtro)
                ? (datosClienteOrigen.NumeroTelefonoOtro + "/") : string.Empty;
            if (!string.IsNullOrWhiteSpace(telefono))
                telefono = telefono.Substring(0, telefono.Length - 1);
            return new ClienteExternoDTO()
            {
                CodigoCliente = datosClienteOrigen.CodigoCliente,
                CodigoCuentaInterbancaria = numeroCuenta,
                Nombres = string.IsNullOrEmpty(datosClienteOrigen.Nombres?.Trim())
                    ? datosClienteOrigen.NombreCompletoCliente
                    : datosClienteOrigen.Nombres,
                ApellidoPaterno = datosClienteOrigen.ApellidoPaterno,
                ApellidoMaterno = datosClienteOrigen.ApellidoMaterno,
                NumeroDocumento = datosClienteOrigen.NumeroDocumento,
                CodigoTipoDocumento = datosClienteOrigen.DocumentoCartillaPorDefecto.CodigoTipoDocumento.Trim(),
                Nacionalidad = datosClienteOrigen.CodigoPais?.Trim() ?? string.Empty,
                TipoPersona = datosClienteOrigen.TipoPersona == Cliente.TipoPersonaNatural
                    ? ((int)ClienteExternoDTO.Persona.Natural).ToString() : ((int)ClienteExternoDTO.Persona.Juridica).ToString(),
                Telefono = telefono,
                CodigoResidencia = datosClienteOrigen.CodigoResidencia,
                CodigoCargo = datosClienteOrigen.PersonaFisica?.CodigoCargo ?? 0,
                CodigoDepartamento = datosClienteOrigen.DireccionPorDefecto.CodigoPais.Trim().Length > 2 ?
                    datosClienteOrigen.DireccionPorDefecto.CodigoPais.Trim()
                    .Substring(datosClienteOrigen.DireccionPorDefecto.CodigoPais.Length - 2) :
                    datosClienteOrigen.DireccionPorDefecto.CodigoPais.Trim(),
                CodigoDistrito = datosClienteOrigen.DireccionPorDefecto.CodigoCanton.Trim().Length > 2 ?
                    datosClienteOrigen.DireccionPorDefecto.CodigoCanton.Trim()
                    .Substring(datosClienteOrigen.DireccionPorDefecto.CodigoCanton.Length - 2) :
                    datosClienteOrigen.DireccionPorDefecto.CodigoCanton.Trim(),
                CodigoProvincia = datosClienteOrigen.DireccionPorDefecto.CodigoProvincia.Trim().Length > 2 ?
                    datosClienteOrigen.DireccionPorDefecto.CodigoProvincia.Trim()
                    .Substring(datosClienteOrigen.DireccionPorDefecto.CodigoProvincia.Length - 2) :
                    datosClienteOrigen.DireccionPorDefecto.CodigoProvincia.Trim(),
                FechaNacimiento = datosClienteOrigen.PersonaFisica?.FechaNacimiento,
                CodigoOcupacion = datosClienteOrigen.PersonaFisica?.CodigoOcupacion ?? string.Empty,
                CodigoActividad = datosClienteOrigen.PersonaFisica?.CodigoActividad ?? string.Empty,
                CodigoUsuario = datosClienteOrigen.CodigoUsuario ?? General.UsuarioPorDefecto,
                NumeroDocumentoRuc = datosClienteOrigen.DocumentoRuc?.NumeroDocumento ?? string.Empty,
                CodigoSubactividad = datosClienteOrigen.PersonaFisica?.CodigoSubActividad ?? string.Empty,
                DetalleDireccion = datosClienteOrigen.DireccionPorDefecto?.DetalleDireccion ?? string.Empty,
                TipoCliente = ClienteExternoDTO.Cliente
            };
        }

        /// <summary>
        /// Extension de cliente originante si no existe en la CMAC TACNA
        /// </summary>
        /// <param name="codigoCuentaInterbancariaOriginante"></param>
        /// <param name="codigoNocliente"></param>
        /// <param name="tipoDocumentoOrigen"></param>
        /// <param name="nombres"></param>
        /// <param name="numeroDocumento"></param>
        /// <param name="tipoPersona"></param>
        /// <param name="telefono"></param>
        /// <returns>Retorna datos mapeados</returns>
        public static ClienteExternoDTO ClienteNoExistente(
            this string codigoCuentaInterbancariaOriginante,
            string? codigoNocliente,
            string? tipoDocumentoOrigen,
            string? nombres,
            string? numeroDocumento,
            string? tipoPersona,
            string? telefono)
        {
            return new ClienteExternoDTO()
            {
                CodigoCliente = codigoNocliente,
                CodigoCuentaInterbancaria = codigoCuentaInterbancariaOriginante.Trim(),
                Nombres = nombres,
                NumeroDocumento = numeroDocumento.Trim(),
                CodigoTipoDocumento = tipoDocumentoOrigen.Trim(),
                CodigoUsuario = General.UsuarioPorDefecto,
                TipoPersona = tipoPersona.Trim() == ClienteExternoDTO.TipoPersonaNaturalCCE
                    ? ((int)ClienteExternoDTO.Persona.Natural).ToString() : ((int)ClienteExternoDTO.Persona.Juridica).ToString(),
                Telefono = string.IsNullOrEmpty(telefono) ? string.Empty : telefono.Trim(),
                TipoCliente = ClienteExternoDTO.ClienteExterno
            };
        }

        /// <summary>
        /// Metodo que mapea a datos de un cliente externo
        /// </summary>
        /// <param name="consultaClienteExterno">consulta de cuenta de cliente externo</param>
        /// <returns>Datos de cliente externo</returns>
        public static ClienteExternoDTO AClienteExternoDTO(
            this ResultadoConsultaCuentaCCE consultaClienteExterno,
            string tipoDocumentoTakana)
        {
            return new ClienteExternoDTO
            {
                CodigoTipoDocumento = tipoDocumentoTakana,
                NumeroDocumento = consultaClienteExterno.NumeroIdentidadReceptor,
                TipoCliente = ClienteExternoDTO.ClienteExterno,
                Nombres = string.Empty,
                ApellidoPaterno = consultaClienteExterno.NombreReceptor,
                ApellidoMaterno = string.Empty
            };
        }
    }
}
