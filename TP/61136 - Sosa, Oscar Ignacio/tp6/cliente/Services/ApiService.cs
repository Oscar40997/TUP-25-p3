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
        _endpoint = configuration["ApiEndpoint"] ?? "api/datos";
    }

    public async Task<DatosRespuesta> ObtenerDatosAsync()
    {
        try
        {
            var repose = await _httpClient.GetAsync(_endpoint);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Respuesta no exitosa del servidor; {StatusCode} ", response.StatusCode);
                return Error($"Error del servidor: {response.StatusCode}");
            }

            var datos = await response.Content.ReadFromJsonAsync<DatosRespuesta>();

            if (datos is null)
            {
                _logger.LogWarning("La respuesta del servidor fue vacia.");
                return Error("Respuesta vacia del servidor");
            }

            return datos;
        }

        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error de red al contactar con el servidor");
            return Error("Error de red: " + ex.Message);
        }

        catch (NotSupportedException ex)
        {
            _logger.LogError(ex, "Tipo de contenido no soportado.");
            return Error("Contenido no soportado: " + ex.Message);
        }
    }
}