
namespace ML
{
    public class Libro
    {
        public int IdLibro { get; set; }
        public string Nombre { get; set; }
        public string FechaPublicacion { get; set; }
        public int NumeroPaginas { get; set; }
        public Autor Autor { get; set; }
    }
}
