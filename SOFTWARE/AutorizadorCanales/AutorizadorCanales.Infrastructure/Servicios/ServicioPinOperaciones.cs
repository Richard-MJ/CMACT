using AutorizadorCanales.Aplication.Common.ServicioPinOperacionesApi;
using AutorizadorCanales.Aplication.Common.ServicioPinOperacionesApi.Configuracion;
using AutorizadorCanales.Aplication.Common.ServicioPinOperacionesApi.DTOs;
using AutorizadorCanales.Excepciones;
using AutorizadorCanales.Logging.Interfaz;
using System.Text.Json;

namespace AutorizadorCanales.Infrastructure.Servicios;

public class ServicioPinOperaciones : IServicioPinOperaciones
{
    private readonly string _url;
    private readonly IBitacora<ServicioPinOperaciones> _bitacora;
    private const string DESTINO_HOMEBANKING = "H";
    private const string KEY_AUTORIZADOR = "Api";
    private const string PVKI_AUTORIZADOR = "1";

    public ServicioPinOperaciones(
        ConfiguracionServicioPinOperacionesApi configuracion,
        IBitacora<ServicioPinOperaciones> bitacora)
    {
        _url = configuracion.UrlApiPinOperaciones;
        _bitacora = bitacora;
    }

    public async Task<string> TrasladaPINBlock(
        string pan,
        string pin)
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

        string numeroTarjetaEnmascarada = string.Concat(new string('*', pan.Length - 4),
                pan.Substring(pan.Length - 4, 4));
        _bitacora.Trace("Inicio TraducirPinBlock de: {0} , Canal Destino: {1}", numeroTarjetaEnmascarada, DESTINO_HOMEBANKING);

        string endPoint = "/PinOperaciones/trasladapinblock?";

        string urlRequest = _url + endPoint +
                        $"key={KEY_AUTORIZADOR}&" +
                        $"pan={pan}&" +
                        $"pvki={PVKI_AUTORIZADOR}&" +
                        $"pin={pin}&" +
                        $"canalD={DESTINO_HOMEBANKING}";

        try
        {
            using var cliente = new HttpClient(handler);
            var response = await cliente.GetAsync(urlRequest);

            var result = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var resultado = JsonSerializer.Deserialize<Respuesta>(result, options)!;
            var datosSerializados = JsonSerializer.Serialize(resultado.Datos);
            var resultadoPropiedades = JsonSerializer.Deserialize<PinPropiedades>(datosSerializados, options)!;
            _bitacora.Trace("Respuesta TraducirPinBlock de {0}, Respuesta: {1}", numeroTarjetaEnmascarada, resultadoPropiedades.Resultado + " " + resultadoPropiedades.Mensaje);

            if (resultadoPropiedades.Resultado != "1")
                throw new Exception(resultadoPropiedades.Mensaje);

            return resultadoPropiedades.Mensaje;
        }
        catch (Exception ex)
        {
            _bitacora.Error("Error al trasladar pin con el servicio de pin operaciones Api");
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> ValidarClave(string numeroTarjeta, string pinblock, string numeroPvv)
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

        string numeroTarjetaEnmascarada = string.Concat(new string('*', numeroTarjeta.Length - 4),
                numeroTarjeta.Substring(numeroTarjeta.Length - 4, 4));
        _bitacora.Trace("Inicio ValidarPin de: {0} , Canal Destino: {1}", numeroTarjetaEnmascarada, DESTINO_HOMEBANKING);

        string endPoint = _url + "/PinOperaciones/validapin/";
        try
        {
            string urlRequest = endPoint +
            $"Test/" +
            $"{numeroTarjeta}/" +
            $"1/" +
            $"{pinblock}/" +
            $"{numeroPvv}/" +
            $"{DESTINO_HOMEBANKING}";

            using var cliente = new HttpClient(handler);
            var response = await cliente.GetAsync(urlRequest);

            var result = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var resultado = JsonSerializer.Deserialize<Respuesta>(result, options)!;
            var datosSerializados = JsonSerializer.Serialize(resultado.Datos);
            var resultadoPropiedades = JsonSerializer.Deserialize<PinPropiedades>(datosSerializados, options)
                ?? throw new ExcepcionAUsuario("06", "Error al obtener resultado de validación de clave.");

            _bitacora.Trace("Respuesta ValidarPin de {0}, Respuesta: {1}", numeroTarjetaEnmascarada, resultadoPropiedades.Resultado + " " + resultadoPropiedades.Mensaje);

            if (resultadoPropiedades.Resultado == "-1")
            {
                throw ExcepcionAUsuario.ExcepcionAfiliacionInicioSesion("55");
            }
            else if (resultadoPropiedades.Resultado != "1")
            {
                throw new Exception("Error al validar clave de usuario.");
            }
            return resultadoPropiedades.Resultado == "1";
        }
        catch (ExcepcionAUsuario ex)
        {
            _bitacora.Error(ex.Message,
                new Dictionary<string, object>
                {
                        {"urlApiPinOperaciones", _url},
                        {"indicadorCanal", DESTINO_HOMEBANKING }
                });

            throw new ExcepcionAUsuario(ex.CodigoError, ex.Message);
        }
    }

    public async Task<string> GenerarPvv(string numeroTarjeta, string pinblock)
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

        string numeroTarjetaEnmascarada = string.Concat(new string('*', numeroTarjeta.Length - 4),
                numeroTarjeta.Substring(numeroTarjeta.Length - 4, 4));
        _bitacora.Trace("Inicio ObtenerPvv de: {0} , Canal Destino: {1}", numeroTarjetaEnmascarada, DESTINO_HOMEBANKING);

        string endPoint = "/PinOperaciones/obtenerpvv/";
        string urlRequest = _url + endPoint +
            $"Test/" +
            $"{numeroTarjeta}/" +
            $"1/" +
            $"{pinblock}/" +
            $"{DESTINO_HOMEBANKING}";

        try
        {
            using var cliente = new HttpClient(handler);
            var response = await cliente.GetAsync(urlRequest);

            var result = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var resultado = JsonSerializer.Deserialize<Respuesta>(result, options)!;
            var datosSerializados = JsonSerializer.Serialize(resultado.Datos);
            var resultadoPropiedades = JsonSerializer.Deserialize<PinPropiedades>(datosSerializados, options);

            if (resultadoPropiedades == null)
            {
                throw new NullReferenceException("Error al obtener resultado de validación de clave.");
            }

            _bitacora.Trace("Respuesta ObtenerPvv de {0}, Respuesta: {1}", numeroTarjetaEnmascarada, resultadoPropiedades.Resultado);

            if (resultadoPropiedades.Resultado != "1")
            {
                throw new ExcepcionAUsuario("06", resultadoPropiedades.Mensaje);
            }

            return resultadoPropiedades.Mensaje;
        }
        catch (ExcepcionAUsuario ex)
        {
            _bitacora.Error(ex.Message,
               new Dictionary<string, object>
               {
                        {"urlApiPinOperaciones", _url},
                        {"indicadorCanal", DESTINO_HOMEBANKING }
               });

            throw new ExcepcionAUsuario(ex.CodigoError, ex.Message);
        }
    }

    /// <summary>
    /// Obtiene los pinblocks
    /// </summary>
    /// <param name="password">Contraseña</param>
    /// <param name="numeroTarjeta">Número de tarjeta</param>
    /// <returns></returns>
    public async Task<Tuple<string, string>> ObtenerPinBlock(string password, string numeroTarjeta)
    {
        var clave1 = "0" + password.Substring(0, 3);
        var clave2 = "0" + password.Substring(3, 3);
        var pinblock1 = await TrasladaPINBlock(numeroTarjeta, clave1);
        var pinblock2 = await TrasladaPINBlock(numeroTarjeta, clave2);
        return new Tuple<string, string>(pinblock1, pinblock2);
    }
}
