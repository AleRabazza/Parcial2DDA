using System.Diagnostics.Eventing.Reader;

namespace Parcial2DDA.Services
{
    public class CalculoDeTiempo
    {

        public int CalcularTiempo(DateTime tiempo2)
        {
          DateTime tiempo1 = DateTime.Now;

            int t1 = (int)((DateTimeOffset)tiempo1).ToUnixTimeSeconds();
            int t2 = (int)((DateTimeOffset)tiempo2).ToUnixTimeSeconds();

            return t2 + t1;

        }

        public int MaximoTiempo(int tiempo1, int tiempo2)
        {

            if (tiempo1 > tiempo2)
            {
                return -1;
            }
            else if (tiempo2 > tiempo1)
            {
                return tiempo2;
            }
            else
            { 
                return tiempo1;
            }
        }
    }
}
