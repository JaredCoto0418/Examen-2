using ApiAmortizacion.Dtos;
using ApiAmortizacion.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiAmortizacion.Controllers
{
    [ApiController]
    [Route("api/loans")] // solicitado
    public class AmortizacionController : ControllerBase
    {
        private readonly IAmortizacionService amortizacion;

        public AmortizacionController(IAmortizacionService amortizacion)
        {
            this.amortizacion = amortizacion;
        }


        [HttpPost]
        public async Task<ActionResult> CrearAmortizacion(CrearAmortizacionDTO request)
        {
            var resultado = await this.amortizacion.CrearAmortizacion(request);
            return Ok(resultado);
        }

        [HttpGet("{idCliente}")]
        public async Task<ActionResult> ObtenerAmortizacionesPorCliente(int idCliente)
        {
            try
            {
                var resultado = await this.amortizacion.ObtenerPlanAmortizacion(idCliente);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(new { Error = "Ocurrio un error: "+ e.Message});
            }
        }
    }
}
