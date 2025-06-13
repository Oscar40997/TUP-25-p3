using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using cliente.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cliente.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiService> _logger;
        private readonly string _endpointDatos;
        private readonly string _endpointProductos;

        public ApiService(HttpClient httpClient, ILogger<ApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _endpointDatos = "api/datos";
            _endpointProductos = "api/productos";
        }

        // Método para obtener datos generales del servidor
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

        // Método para obtener la lista de productos con imágenes y precios
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

        // Método auxiliar para generar un objeto con error
        private DatosRespuesta Error(string mensaje)
            => new DatosRespuesta(mensaje, DateTime.Now);
    }
}

