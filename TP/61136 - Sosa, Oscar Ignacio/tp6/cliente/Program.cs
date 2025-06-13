using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using cliente;
using cliente.Services;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configurar HttpClient con la URL base del servidor API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5184") // Cambia el puerto si es necesario
});

// Registrar ApiService sin IConfiguration
builder.Services.AddScoped<ApiService>();

await builder.Build().RunAsync();
