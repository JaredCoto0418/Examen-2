using ApiAmortizacion.Dtos;
using ApiAmortizacion.Entidades;
using ApiAmortizacion.Servicios.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ApiAmortizacion.Servicios.Implementacion
{
    public class AmortizacionServicio : IAmortizacionService
    {
        private readonly ContextoDB contextoDB;

        public AmortizacionServicio(ContextoDB contextoDB)
        {
            this.contextoDB = contextoDB;
        }
        public async Task<RespuestaCrearAmortizacionDTO> CrearAmortizacion(CrearAmortizacionDTO request)
        {
            var cliente = await this.contextoDB.Cliente.Where(x => x.NumeroIdentidad == request.NumeroIdentidad)
                .FirstOrDefaultAsync();
            if (cliente == null)
            {
                cliente = new Cliente { 
                    Nombre = request.Nombre,
                    NumeroIdentidad = request.NumeroIdentidad,
                };
                this.contextoDB.Cliente.Add(cliente);
                await this.contextoDB.SaveChangesAsync();
            }
            var amortizacion = new Amortizacion()
            {
                IdCliente = cliente.Id,
                FechaCreacion = DateTime.Now,
                MontoPrestado = (decimal?)request.MontoPrestado
            };
            this.contextoDB.Amortizacion.Add(amortizacion);
            await this.contextoDB.SaveChangesAsync();
            var fechaActual = request.FechaPrimerPago;
            DateTime mesAnterior = fechaActual;
            double saldoAnterior = (request.MontoPrestado *( request.TasaComision / 100)) + request.MontoPrestado;
            List<PlanAmortizacion> planAmortizaciones = [];
            var i = (request.TasaInteres / 100) / Math.Round(((360*request.plazo)/365.0),2);
            double cuota = (saldoAnterior / ((1 - Math.Pow(1+i,-request.plazo))/i));
            cuota = Math.Round(cuota,2);
            do
            {
                double svsd = saldoAnterior * 0.0015;
                svsd = svsd < 2 ? 2 : svsd;
                var diasMes = planAmortizaciones.IsNullOrEmpty() ? DateTime.DaysInMonth(fechaActual.Year,fechaActual.Month) : (fechaActual - mesAnterior).Days;
                double interes = (saldoAnterior * ((request.TasaInteres/100) / 360.0) * diasMes);
                interes = Math.Round(interes,2);
                var planAmortizacion = new PlanAmortizacion()
                {
                    IdAmortizacion = amortizacion.Id,
                    FechaPago = fechaActual,
                    NumeroCuota = planAmortizaciones.Count + 1,
                    Dias = diasMes,
                    Interes = (decimal?)interes,
                    PagoNiveladoSinSvsd = (decimal?)cuota,
                    PagoNiveladoConSvsd = (decimal)(svsd + cuota),
                    Capital = (decimal?)(cuota - interes),
                    SaldoCapital = (decimal)(saldoAnterior - (cuota - interes))
                };
                planAmortizaciones.Add(planAmortizacion);
                mesAnterior = fechaActual;
                fechaActual = fechaActual.AddMonths(1);
                saldoAnterior = saldoAnterior - (cuota - interes);
                this.contextoDB.PlanAmortizacion.Add(planAmortizacion);
                await this.contextoDB.SaveChangesAsync();
            } while (planAmortizaciones.Count < request.plazo);
            return new RespuestaCrearAmortizacionDTO(
                "Creado con exito",
                planAmortizaciones.Select(x => new PlanAmortizacionDTO(
                    x?.NumeroCuota ?? 0,
                    x.FechaPago.Value.ToShortDateString(),
                    x.Dias ?? 0,
                    (double)x.Interes,
                    (double)x.Capital,
                    (double)x.PagoNiveladoSinSvsd,
                    (double)x.PagoNiveladoConSvsd,
                    (double)x.SaldoCapital
                )).ToArray()
            );
        }

        public async Task<AmortizacionClienteDTO> ObtenerPlanAmortizacion(int idCliente)
        {
            var cliente = await this.contextoDB.Cliente.Where(x => x.Id == idCliente)
                .FirstOrDefaultAsync();
            var amortizador = await this.contextoDB.Amortizacion
                .Where(x => x.IdCliente == cliente.Id)
                .Include(x => x.PlanAmortizacion)
                .FirstOrDefaultAsync();
            return new AmortizacionClienteDTO(
                cliente.Id,
                cliente.Nombre,
                cliente.NumeroIdentidad,
                (double)amortizador?.MontoPrestado,
                amortizador.PlanAmortizacion.Select(x => new PlanAmortizacionDTO(
                    x.NumeroCuota ?? 0,
                    x.FechaPago.Value.ToShortDateString(),
                    x.Dias ?? 0,
                    (double)(x.Interes),
                    (double)x.Capital,
                    (double)x.PagoNiveladoSinSvsd,
                    (double)x.PagoNiveladoConSvsd,
                    (double)x.SaldoCapital
                )).ToArray()
            );

            //  Para probar
        }
    }
}
