using AutorizadorCanales.Aplication.Features.TramasProcesadas.Commands;
using AutorizadorCanales.Core.Constantes;
using AutorizadorCanales.Domain.Entidades.CC;
using AutorizadorCanales.Domain.Repositorios;
using MediatR;
using System.Transactions;

namespace AutorizadorCanales.Aplication.Features.TramasProcesadas.Handlers
{
    public class RegistrarDatosInicioSesionCommandHandler : IRequestHandler<RegistrarDatosInicioSesionCommand, int>
    {
        private readonly IRepositorioEscritura _repositorioEscritura;

        public RegistrarDatosInicioSesionCommandHandler(IRepositorioEscritura repositorioEscritura)
        {
            _repositorioEscritura = repositorioEscritura;
        }

        /// <summary>
        /// Maneja un comando que registra los datos de inicio de sesión
        /// </summary>
        /// <param name="command">Datos del comando</param>
        /// <param name="cancellationToken">token de cancelacion</param>
        /// <returns>Id del registro de la trama</returns>
        public async Task<int> Handle(RegistrarDatosInicioSesionCommand command, CancellationToken cancellationToken)
        {
            var tramaRegistrada = TramaProcesada.Crear(
                command.IndicadorCanal,
                command.IdClienteFinal,
                TramaProcesadaConstante.TIPO_TRAMA_INICIO_SESION,
                TramaProcesadaConstante.TIPO_PROCESO_INICIO_SESION,
                "",
                command.IdTerminal,
                0,
                "",
                command.FechaOperacion,
                TramaProcesadaConstante.CODIGO_BIN_CANALES,
                "",
                "",
                command.CodigoMensajeUno,
                "",
                command.FechaOperacion,
                Sistema.SEGURIDAD);

            await _repositorioEscritura.AdicionarAsync(tramaRegistrada);

            using (var transaccion = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _repositorioEscritura.GuardarCambiosAsync();
                transaccion.Complete();
            }

            return tramaRegistrada.Id;
        }
    }
}
