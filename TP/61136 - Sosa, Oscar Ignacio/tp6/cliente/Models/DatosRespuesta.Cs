namespace cliente.Models
{
    public class DatosRespuesta
    {
        public string Mensaje { get; set; } = string.Empty; // ✅ Inicialización segura
        public DateTime? Fecha { get; set; }  // Ya es nullable, está bien

        public DatosRespuesta() { }

        public DatosRespuesta(string mensaje, DateTime? fecha)
        {
            Mensaje = mensaje;
            Fecha = fecha;
        }
    }
}
