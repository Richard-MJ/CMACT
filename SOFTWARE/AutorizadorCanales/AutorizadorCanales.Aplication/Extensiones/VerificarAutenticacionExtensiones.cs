using AutorizadorCanales.Aplication.Common.ServicioSeguridad.DTOs;
using AutorizadorCanales.Aplication.Features.Autenticacion.Commands;
using AutorizadorCanales.Core.Constantes;

namespace AutorizadorCanales.Aplication.Extensiones;

public static class VerificarAutenticacionExtensiones
{
    public static AutorizacionCanalElectronico AAutorizacionCanalElectronico
        (this VerificarAutenticacionCommand command)
    {
        return new AutorizacionCanalElectronico
        {
            IdTipoOperacion = TipoOperacion.GESTION_CLAVE,
            IdVerificacion = command.IdVerificacion,
            CodigoAutorizacion = command.CodigoAutorizacion,
            CodigoMonedaAutorizada = MonedaConstante.SOLES,
            MontoAutorizado = 0m,
            IdTipoOperacionCanalElectronico = TipoOperacionCanalElectronicoLogin.CONFIRMACION_LOGIN,
            IdRegistroNuevoDispositivo = command.NewGuid
        };
    }
}
