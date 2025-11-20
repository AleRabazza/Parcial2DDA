using System.Diagnostics.Eventing.Reader;

namespace Parcial2DDA.Services
{
    public class CalculoDePeso
    {

        public decimal calcularPeso(decimal peso1, decimal peso2)
        {
            return peso1 + peso2;
        }

        public decimal MaximoPeso(decimal uno, decimal dos)
        {
            if (uno > dos)
            {
                return uno;

            }
            else if (dos > uno)
            {
                return dos;
            }
            else
            {
                return uno;
            }
        }
    }
}
