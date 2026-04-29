using AutorizadorCanales.Aplication.Features.Autenticacion.Commands;
using AutorizadorCanales.Aplication.Servicios.Autenticacion;
using AutorizadorCanales.Excepciones;
using AutorizadorCanales.Excepciones.Constantes;
using MediatR;
using System.Text.Json.Nodes;

namespace AutorizadorCanales.Aplication.Features.Autenticacion.Handlers;
                                                                        
public class AutenticarAperturaProductoCommandHandler : IRequestHandler<AutenticarAperturaProductoCommand, JsonObject>
{
    private readonly IServicioAutenticacionAperturaProductos _servicioAutenticacion;

    /// <summary>
    /// Manejador del comando de autenticación de apertura de producto
    /// </summary>
    /// <param name="servicioAutenticacion"></param>
    public AutenticarAperturaProductoCommandHandler(
        IServicioAutenticacionAperturaProductos servicioAutenticacion)
    {
        _servicioAutenticacion = servicioAutenticacion;
    }

    /// <summary>
    /// Maneja el comando autenticacion de cliente
    /// </summary>
    /// <param name="command">Datos del comando</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Objeto json con las datos de acceso</returns>
    public async Task<JsonObject> Handle(AutenticarAperturaProductoCommand command, CancellationToken cancellationToken)
    {
        ValidarComando(command);

        return await _servicioAutenticacion.AutenticarAperturaProducto(command);
    }

    /// <summary>
    /// Metodo para validar los datos del comando de autenticación de apertura de producto
    /// </summary>
    /// <param name="command"></param>
    /// <exception cref="ExcepcionAUsuario"></exception>
    private void ValidarComando(AutenticarAperturaProductoCommand command)
    {
        if (string.IsNullOrEmpty(command.IdTipoDocumento) || command.IdTipoDocumento == "0")
            throw new ExcepcionAUsuario(ConstMensajeError.CodigoErrorAfiliacionInicioSesion, "El tipo de documento seleccionado no es válido.");

        if (string.IsNullOrEmpty(command.NumeroDocumento))
            throw new ExcepcionAUsuario(ConstMensajeError.CodigoErrorAfiliacionInicioSesion, "Ingrese un número de documento válido.");
    }
}
