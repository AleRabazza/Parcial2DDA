namespace Parcial2DDA.Models
{
    public class Control : Usuario
    {
       public int Id {  get; set; }
        public string? Huella {  get; set; }

        public decimal Peso { get; set; }

        public DateTime Fecha { get; set; }

    }

}
