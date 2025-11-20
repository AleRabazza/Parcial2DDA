using Microsoft.AspNetCore.Mvc;
using Parcial2DDA.Data;
using Parcial2DDA.Models;
using Parcial2DDA.Models.DTOs;
using Parcial2DDA.Services;


namespace Parcial2DDA.Controllers
{
    [Controller]
    [Route("[controller]")]

    public class ReportesController : ControllerBase 
    {
        public readonly AppDbContext _context;
        public readonly CalculoDePeso _calculoDePeso;
        public readonly CalculoDeTiempo _calculoDeTiempo;

        public ReportesController(AppDbContext appDbContext, CalculoDeTiempo calculoDeTiempo, CalculoDePeso calculoDePeso)
        {
            _context = appDbContext;
            _calculoDePeso = calculoDePeso;
            _calculoDeTiempo = calculoDeTiempo;
        }


        [HttpPost]

        [Route("/mediciones")]

        public IActionResult IngresoDatos([FromBody] ControlDTO control1)
        {
            if (control1 == null)
            {
                return BadRequest("Ingrese los Datos de froma correcta");

            }

            Usuario usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.Huella2 == control1.Huella);

            Control control2 = _context.Controles.FirstOrDefault(contol2 => contol2.Huella2 == control1.Huella);

            if (control1.tipo == "entrada")
            {

                if (usuario == null)
                {
                    Control control3 = new Control
                    {
                        Huella = control1.Huella,
                        Peso = control1.Peso,
                        Fecha = DateTime.Now

                    };

                    Usuario usuario2 = new Control
                    {
                        Huella2 = "hola"

                    };

                    _context.Controles.Add(control3);
                    _context.Controles.Add(usuario2); 
                    _context.SaveChanges();

                    return Ok(control3);

                }
                else if (control2 == null)
                {
                    control2.Peso = control1.Peso;
                    control2.Fecha = DateTime.Now;
                    _context.SaveChanges();

                    return Ok(control2);
                }
            }
            else if (control1.tipo == "salida" && control2 != null)
            {
                decimal diferenciaPeso = _calculoDePeso.calcularPeso(control1.Peso, control2.Peso);
                decimal maximo = _calculoDePeso.MaximoPeso(diferenciaPeso, usuario.DiferenciaMaximaPeso);

                usuario.DiferenciaMaximaPeso = maximo;



                int diferenciaTiempo = _calculoDeTiempo.CalcularTiempo(control2.Fecha);
                int maximoT = _calculoDeTiempo.MaximoTiempo(usuario.DiferenciaMaximaDeTiempo, diferenciaTiempo);

                usuario.DiferenciaMaximaDeTiempo = maximoT;


                usuario.MedicionesCompletadas = control2.MedicionesCompletadas + 1;

                _context.Remove(control2);
                _context.SaveChanges();
                return Ok();
            }

            return BadRequest("Error");

        }


        [HttpGet]
        [Route("maxima_diferencia_peso")]
        public IActionResult MaximaDiferenciaDePeso(string huella)
        {
            if (string.IsNullOrWhiteSpace(huella))
            {
                return BadRequest("Error al ingresar el dato");
            }

            Usuario usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.Huella2 == huella);

            if(usuario == null)
            {
                return BadRequest("Error");
            }

            string mensaje = "Maxima Diferencia De Peso " + usuario.DiferenciaMaximaPeso; 

            return Ok(mensaje);

        }


        [HttpGet]
        [Route("maxima_diferencia_Tiempo")]
        public IActionResult MaximaDiferenciaDeTiempo(string huella)
        {
            if (string.IsNullOrWhiteSpace(huella))
            {
                return BadRequest("Error al ingresar el dato");
            }

            Usuario usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.Huella2 == huella);

            if (usuario == null)
            {
                return BadRequest("Error");
            }

            string mensaje = "Maxima Diferencia De tiempo " + usuario.DiferenciaMaximaDeTiempo;

            return Ok(mensaje);

        }

    }
}
