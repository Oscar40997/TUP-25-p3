using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Cliente.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ApiService> _logger;
    private readonly string _endpoint;

    public ApiService(HttpClient httpClient, ILogger<ApiService> logger, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _logger = logger;
        _endpoint = configuration["ApiEndpoint"] ?? "/api/datos";
    }

    public async Task<DatosRespuesta> ObtenerDatosAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(_endpoint);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Respuesta no exitosa del servidor: {StatusCode}", response.StatusCode);
                return new DatosRespuesta("Error del servidor: " + response.StatusCode, DateTime.Now);
            }

            var datos = await response.Content.ReadFormJsonAsync<DatosRespuesta>();

            if (datos is null)
            {
                _logger.LogWarning("La respuesta del servidor fue vacía.");
                return new DatosRespuesta("Respuesta vacia del servidor", DateTime.Now);
            }

            return datos;
        }

        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error de red al contactar con el servidor.");
            return new DatosRespuesta("Error de red: " + ex.Message, DateTime.Now);
        }

        catch (NotSupportedException ex)
        {
            _logger.LogError(ex, "Tipo de contenido no soportado.");
            return new DatosRespuesta("Contenido no soportado: " + ex.Message, DateTime.Now);

        }

        catch (System.Text.Json.JsonException ex)
        {
            _logger.LogError(ex, "Error al deserializar la respuesta JSON.");
            return new DatosRespuesta("Error de datos:" + ex.Message, DateTime.Now);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al obtener datos.");
            return new DatosRespuesta("Error inesperado: " + ex.Message, DateTime.Now);
        }
    }

}