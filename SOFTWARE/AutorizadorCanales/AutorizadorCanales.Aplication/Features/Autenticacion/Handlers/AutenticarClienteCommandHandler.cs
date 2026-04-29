using AutorizadorCanales.Aplication.Features.Autenticacion.Commands;
using AutorizadorCanales.Aplication.Servicios.Autenticacion;
using AutorizadorCanales.Core.Constantes;
using AutorizadorCanales.Excepciones;
using AutorizadorCanales.Excepciones.Constantes;
using MediatR;
using System.Text.Json.Nodes;

namespace AutorizadorCanales.Aplication.Features.Autenticacion.Handlers;

public class AutenticarClienteCommandHandler : IRequestHandler<AutenticarClienteCommand, JsonObject>
{
    private readonly IServicioAutenticacion _servicioAutenticacion;

    private const int LONGITUD_CLAVE_TARJETA = 4;

    private const int LONGITUD_CLAVE_INTERNET = 6;

    public AutenticarClienteCommandHandler(
        IServicioAutenticacion servicioAutenticacion)
    {
        _servicioAutenticacion = servicioAutenticacion;
    }

    /// <summary>
    /// Maneja el comando autenticacion de cliente
    /// </summary>
    /// <param name="command">Datos del comando</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Objeto json con las datos de acceso</returns>
    public async Task<JsonObject> Handle(AutenticarClienteCommand command, CancellationToken cancellationToken)
    {
        ValidarComando(command);

        return await _servicioAutenticacion.AutenticarCliente(command);
    }

    private void ValidarComando(AutenticarClienteCommand command)
    {
        if (command.IdTipoOperacionCanalElectronico == TipoOperacionLogin.AFILIACION)
            if (string.IsNullOrEmpty(command.PasswordPrimario) || command.PasswordPrimario!.Trim().Length != LONGITUD_CLAVE_TARJETA)
                throw new ExcepcionAUsuario(ConstMensajeError.CodigoErrorAfiliacionInicioSesion, "Clave de 4 digitos no válida.");

        if (command.IdTipoOperacionCanalElectronico != TipoOperacionLogin.LOGIN_BIOMETRIA && command.Password.Trim().Length != LONGITUD_CLAVE_INTERNET)
            throw new ExcepcionAUsuario(ConstMensajeError.CodigoErrorAfiliacionInicioSesion, "Clave ingresada no válida, ingrese los 6 dígitos.");

        if (string.IsNullOrEmpty(command.IdTipoDocumento) || command.IdTipoDocumento == "0")
            throw new ExcepcionAUsuario(ConstMensajeError.CodigoErrorAfiliacionInicioSesion, "El tipo de documento seleccionado no es válido.");

        if (string.IsNullOrEmpty(command.NumeroDocumento))
            throw new ExcepcionAUsuario(ConstMensajeError.CodigoErrorAfiliacionInicioSesion, "Ingrese un número de documento válido.");
    }
}
