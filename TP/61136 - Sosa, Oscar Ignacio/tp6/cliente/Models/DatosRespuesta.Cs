namespace cliente.Models
{
    public class DatosRespuesta
    {
        public string Mensaje { get; set; }
        public DateTime? Fecha { get; set; }  // Nullable

        public DatosRespuesta() { }

        public DatosRespuesta(string mensaje, DateTime? fecha)
        {
            Mensaje = mensaje;
            Fecha = fecha;
        }
    }
}
