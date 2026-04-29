using System.Text.Json;
using System.Globalization;
using System.Net.Http.Json;
using PagareElectronico.Application.DTOs.Requests;
using PagareElectronico.Application.DTOs.Responses;
using PagareElectronico.Application.Abstractions.Integrations;
using PagareElectronico.Infrastructure.Integrations.Cavali.Mappers;
using PagareElectronico.Infrastructure.Integrations.Cavali.Contracts.Requests;
using PagareElectronico.Infrastructure.Integrations.Cavali.Contracts.Responses;

namespace PagareElectronico.Infrastructure.Integrations.Clients
{
    /// <summary>
    /// Implementa la comunicación HTTP con CAVALI para operaciones de pagarés.
    /// </summary>
    public sealed class CavaliPagareClient : ICavaliPagareGateway
    {
        /// <summary>
        /// Opciones de serialización JSON utilizadas para enviar y leer información.
        /// </summary>
        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        private readonly HttpClient _httpClient;
        private readonly AnnotationAccountRequestMapper _annotationMapper;
        private readonly CancellationRequestMapper _cancellationMapper;
        private readonly DeletionRequestMapper _deletionMapper;
        private readonly ReverseRequestMapper _reverseMapper;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CavaliPagareClient"/>.
        /// </summary>
        /// <param name="httpClient">Cliente HTTP configurado para CAVALI.</param>
        /// <param name="annotationMapper">Mapper de registro y anotación.</param>
        /// <param name="cancellationMapper">Mapper de cancelación.</param>
        /// <param name="deletionMapper">Mapper de retiro.</param>
        /// <param name="reverseMapper">Mapper de reversión.</param>
        public CavaliPagareClient(
            HttpClient httpClient,
            AnnotationAccountRequestMapper annotationMapper,
            CancellationRequestMapper cancellationMapper,
            DeletionRequestMapper deletionMapper,
            ReverseRequestMapper reverseMapper)
        {
            _httpClient = httpClient;
            _annotationMapper = annotationMapper;
            _cancellationMapper = cancellationMapper;
            _deletionMapper = deletionMapper;
            _reverseMapper = reverseMapper;
        }

        /// <summary>
        /// Metodo que registra la anotacion del pagare
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GatewayResponse<DtoRespuestaRegistroPagare>> RegistrarAnotacionAsync(
            DtoSolicitudRegistrarAnotacionPagare request,
            CancellationToken cancellationToken)
        {
            var payload = _annotationMapper.Map(request);

            return await EnviarPostAsync<
                AnnotationAccountRequest,
                CavaliResponseRecordContract,
                DtoRespuestaRegistroPagare>(
                "v1/annotation-account",
                payload,
                MapearRespuestaRegistro,
                cancellationToken);
        }

        /// <summary>
        /// Metodo que cancela el pagare
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GatewayResponse<DtoRespuestaProcesamientoMasivo>> CancelarAsync(
            DtoSolicitudCancelarPagare request,
            CancellationToken cancellationToken)
        {
            var payload = _cancellationMapper.Map(request);

            return await EnviarPostAsync<
                CancellationRequest,
                CavaliResponseProcessingMassContract,
                DtoRespuestaProcesamientoMasivo>(
                "v1/cancellation",
                payload,
                MapearRespuestaProcesamientoMasivo,
                cancellationToken);
        }

        /// <summary>
        /// Metodo que retira el pagare
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GatewayResponse<DtoRespuestaProcesamientoMasivo>> EliminarAsync(
            DtoSolicitudEliminarPagare request,
            CancellationToken cancellationToken)
        {
            var payload = _deletionMapper.Map(request);

            return await EnviarPostAsync<
                DeletionRequest,
                CavaliResponseProcessingMassContract,
                DtoRespuestaProcesamientoMasivo>(
                "v1/deletion",
                payload,
                MapearRespuestaProcesamientoMasivo,
                cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GatewayResponse<DtoRespuestaProcesamientoMasivo>> RevertirCancelacionAsync(
            DtoSolicitudRevertirCancelacionPagare request,
            CancellationToken cancellationToken)
        {
            var payload = _reverseMapper.Map(request);

            return await EnviarPostAsync<
                ReverseRequest,
                CavaliResponseProcessingMassContract,
                DtoRespuestaProcesamientoMasivo>(
                "v1/reverse",
                payload,
                MapearRespuestaProcesamientoMasivo,
                cancellationToken);
        }

        /// <summary>
        /// Envía una solicitud POST a CAVALI y transforma la respuesta al modelo interno.
        /// </summary>
        /// <typeparam name="TPayload">Tipo del request externo.</typeparam>
        /// <typeparam name="TExternalResponse">Tipo del response externo.</typeparam>
        /// <typeparam name="TInternalResponse">Tipo del response interno.</typeparam>
        /// <param name="url">URL relativa del endpoint.</param>
        /// <param name="payload">Payload que se enviará a CAVALI.</param>
        /// <param name="responseMapper">Función que transforma el response externo al interno.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Respuesta del gateway.</returns>
        private async Task<GatewayResponse<TInternalResponse>> EnviarPostAsync<TPayload, TExternalResponse, TInternalResponse>(
            string url,
            TPayload payload,
            Func<TExternalResponse?, string, TInternalResponse> responseMapper,
            CancellationToken cancellationToken)
            where TPayload : class
        {
            using var response = await _httpClient.PostAsJsonAsync(
                url,
                payload,
                JsonOptions,
                cancellationToken);

            var contenido = await response.Content.ReadAsStringAsync(cancellationToken);

            var responseContract = DeserializarRespuesta<TExternalResponse>(contenido);

            var datos = responseMapper(responseContract, contenido);

            return new GatewayResponse<TInternalResponse>(
                response.IsSuccessStatusCode, 
                (int)response.StatusCode,
                response.IsSuccessStatusCode
                    ? "Respuesta procesada correctamente por CAVALI."
                    : "CAVALI devolvió una respuesta no satisfactoria.",
                datos);
        }

        /// <summary>
        /// Deserializa el contenido JSON recibido desde CAVALI.
        /// </summary>
        /// <typeparam name="T">Tipo de contrato a deserializar.</typeparam>
        /// <param name="contenido">Contenido JSON.</param>
        /// <returns>Instancia deserializada o nula si no se pudo interpretar.</returns>
        private static T? DeserializarRespuesta<T>(string contenido)
        {
            if (string.IsNullOrWhiteSpace(contenido))
            {
                return default;
            }

            try
            {
                return JsonSerializer.Deserialize<T>(contenido, JsonOptions);
            }
            catch (JsonException)
            {
                return default;
            }
        }

        /// <summary>
        /// Mapea la respuesta externa de registro al modelo interno.
        /// </summary>
        /// <param name="source">Contrato externo.</param>
        /// <param name="contenido">Contenido crudo recibido.</param>
        /// <returns>Respuesta interna de registro.</returns>
        private static DtoRespuestaRegistroPagare MapearRespuestaRegistro(
            CavaliResponseRecordContract? source,
            string contenido)
        {
            return new DtoRespuestaRegistroPagare
            {
                IdProceso = source?.IdProceso,
                CodigoResultado = source?.ResultCode,
                Mensaje = source?.Message ?? ObtenerMensajePorDefecto(contenido)
            };
        }

        /// <summary>
        /// Mapea la respuesta externa de procesamiento masivo al modelo interno.
        /// </summary>
        /// <param name="source">Contrato externo.</param>
        /// <param name="contenido">Contenido crudo recibido.</param>
        /// <returns>Respuesta interna de procesamiento masivo.</returns>
        private static DtoRespuestaProcesamientoMasivo MapearRespuestaProcesamientoMasivo(
            CavaliResponseProcessingMassContract? source,
            string contenido)
        {
            return new DtoRespuestaProcesamientoMasivo
            {
                CodigoResultado = source?.ResultCode,
                Mensaje = source?.Message ?? ObtenerMensajePorDefecto(contenido),
                CantidadExitosa = source?.Successful ?? 0,
                Fallidos = source?.Failed?.Select(MapearRegistroFallido).ToList() ?? new List<DtoRegistroFallidoProcesamientoMasivo>()
            };
        }

        /// <summary>
        /// Mapea un registro fallido externo al modelo interno.
        /// </summary>
        /// <param name="source">Registro fallido externo.</param>
        /// <returns>Registro fallido interno.</returns>
        private static DtoRegistroFallidoProcesamientoMasivo MapearRegistroFallido(CavaliRecordFailedContract source)
        {
            return new DtoRegistroFallidoProcesamientoMasivo
            {
                LlavePagare = new DtoLlavePagareFallido
                {
                    Banca = source.PromissoryNoteKey?.Banking ?? 0,
                    Producto = source.PromissoryNoteKey?.Product ?? 0,
                    NumeroCredito = source.PromissoryNoteKey?.CreditNumber ?? string.Empty,
                    CodigoUnico = source.PromissoryNoteKey?.UniqueCode ?? string.Empty,
                    FechaCancelacion = ConvertirFecha(source.PromissoryNoteKey?.CancellationDate)
                },
                CodigoResultado = source.ResultCode,
                Mensaje = source.Message ?? string.Empty
            };
        }

        /// <summary>
        /// Convierte una fecha en texto al tipo <see cref="DateOnly"/>.
        /// </summary>
        /// <param name="fechaTexto">Fecha en texto.</param>
        /// <returns>Fecha convertida o nula si no se pudo interpretar.</returns>
        private static DateOnly? ConvertirFecha(string? fechaTexto)
        {
            if (string.IsNullOrWhiteSpace(fechaTexto))
            {
                return null;
            }

            if (DateOnly.TryParseExact(
                fechaTexto,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var fecha))
            {
                return fecha;
            }

            return null;
        }

        /// <summary>
        /// Devuelve un mensaje por defecto cuando la respuesta no pudo deserializarse.
        /// </summary>
        /// <param name="contenido">Contenido crudo recibido.</param>
        /// <returns>Mensaje de respaldo.</returns>
        private static string ObtenerMensajePorDefecto(string contenido)
        {
            return string.IsNullOrWhiteSpace(contenido)
                ? "CAVALI no devolvió contenido en la respuesta."
                : "No se pudo interpretar completamente la respuesta devuelta por CAVALI.";
        }
    }
}