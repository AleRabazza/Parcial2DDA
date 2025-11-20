namespace Parcial2DDA.Models
{
    public class Usuario
    {
        public int id { get; set; }

        public string Huella2 { get; set; }
        public decimal DiferenciaMaximaPeso { get; set; }

        public int MedicionesCompletadas { get; set; }

        public int  DiferenciaMaximaDeTiempo { get; set; }
    }
}
