using Microsoft.AspNetCore.Mvc;
using cliente.Models; // Asegúrate de que este namespace coincida con donde esté tu modelo Producto.cs

namespace Servidor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Producto>> Get()
        {
            var productos = new List<Producto>
            {
                new Producto { Id = 1, Nombre = "Cuaderno Profesional", Precio = 35.00m, ImagenUrl = "/images/productos/cuaderno.jpg" },
                new Producto { Id = 2, Nombre = "Bolígrafo Azul", Precio = 5.00m, ImagenUrl = "/images/productos/boligrafo.jpg" },
                new Producto { Id = 3, Nombre = "Lápiz HB", Precio = 3.50m, ImagenUrl = "/images/productos/lapiz.jpg" },
                new Producto { Id = 4, Nombre = "Caja de Colores", Precio = 40.00m, ImagenUrl = "/images/productos/colores.jpg" },
                new Producto { Id = 5, Nombre = "Mochila Escolar", Precio = 250.00m, ImagenUrl = "/images/productos/mochila.jpg" },
                new Producto { Id = 6, Nombre = "Juego de Geometría", Precio = 60.00m, ImagenUrl = "/images/productos/geometria.jpg" },
                new Producto { Id = 7, Nombre = "Libreta de Notas", Precio = 20.00m, ImagenUrl = "/images/productos/libreta.jpg" },
                new Producto { Id = 8, Nombre = "Tijeras Escolares", Precio = 18.00m, ImagenUrl = "/images/productos/tijeras.jpg" },
                new Producto { Id = 9, Nombre = "Pegamento en Barra", Precio = 12.00m, ImagenUrl = "/images/productos/pegamento.jpg" },
                new Producto { Id = 10, Nombre = "Estuche para Lápices", Precio = 45.00m, ImagenUrl = "/images/productos/estuche.jpg" },
                new Producto { Id = 11, Nombre = "Marcadores Fluorescentes", Precio = 55.00m, ImagenUrl = "/images/productos/marcadores.jpg" },
                new Producto { Id = 12, Nombre = "Calculadora Básica", Precio = 120.00m, ImagenUrl = "/images/productos/calculadora.jpg" }
            };

            return Ok(productos);
        }
    }
}
