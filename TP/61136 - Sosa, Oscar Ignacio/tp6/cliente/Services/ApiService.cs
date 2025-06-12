using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using cliente.Models; // ✅ Importación del modelo Producto

namespace cliente.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ApiService> _logger;
    private readonly string _endpointDatos;
    private readonly string _endpointProductos;

    public ApiService(HttpClient httpClient, ILogger<ApiService> logger, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _logger = logger;
        _endpointDatos = configuration["ApiEndpointDatos"] ?? "api/datos";
        _endpointProductos = configuration["ApiEndpointProductos"] ?? "api/productos";
    }

    // ✅ Método para obtener mensaje/fecha del servidor
    public async Task<DatosRespuesta> ObtenerDatosAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(_endpointDatos);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Respuesta no exitosa del servidor: {StatusCode}", response.StatusCode);
                return Error($"Error del servidor: {response.StatusCode}");
            }

            var datos = await response.Content.ReadFromJsonAsync<DatosRespuesta>();
            if (datos == null)
            {
                _logger.LogWarning("La respuesta del servidor fue vacía.");
                return Error("Respuesta vacía del servidor");
            }

            return datos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al obtener datos.");
            return Error("Error inesperado: " + ex.Message);
        }
    }

    // ✅ Método para obtener productos del servidor
    public async Task<List<Producto>> ObtenerProductosAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(_endpointProductos);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Error al obtener productos: {StatusCode}", response.StatusCode);
                return new List<Producto>();
            }

            var productos = await response.Content.ReadFromJsonAsync<List<Producto>>();
            return productos ?? new List<Producto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener productos.");
            return new List<Producto>();
        }
    }

    // ✅ Método auxiliar para errores
    private DatosRespuesta Error(string mensaje)
        => new DatosRespuesta(mensaje, DateTime.Now);
}
