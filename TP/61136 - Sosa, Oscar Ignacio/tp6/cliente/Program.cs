using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using cliente;
using cliente.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// 🔷 Puntos de entrada del componente Blazor
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 🔷 HttpClient configurado para apuntar a la API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5184") // Asegúrate de que este sea el puerto de tu servidor API
});

// 🔷 Inyección del servicio que conecta con la API
builder.Services.AddScoped<ApiService>();

// 🔷 Ejecutar la app
await builder.Build().RunAsync();
