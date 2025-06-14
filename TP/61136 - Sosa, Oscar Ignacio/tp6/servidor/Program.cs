using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ServidorAPI.Data; // Cambia según tu namespace real

var builder = WebApplication.CreateBuilder(args);

// 1. Registrar servicios
builder.Services.AddControllers();

// 2. Registrar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.WithOrigins("http://localhost:5177") // Puerto del cliente Blazor (solo si se ejecuta por separado)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 3. Configurar EF Core + SQLite (opcional, si usas base de datos)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=tienda.db"));

// 4. Preparar app
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// 5. Habilitar CORS
app.UseCors("AllowBlazor");

// 6. Servir archivos estáticos y Blazor WebAssembly
app.UseBlazorFrameworkFiles(); // 👉 Servir frontend Blazor
app.UseStaticFiles();          // 👉 Para JS/CSS/etc.

// 7. Rutas del API
app.MapControllers(); // Para [ApiController] o rutas en Controladores

// 8. SPA fallback (para que Blazor maneje las rutas)
app.MapFallbackToFile("index.html");

// 9. Ejecutar la app
app.Run();
