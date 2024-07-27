using ApiAmortizacion.Dtos;

namespace ApiAmortizacion.Servicios.Interfaces
{
    public interface IAmortizacionService
    {
        Task<RespuestaCrearAmortizacionDTO> CrearAmortizacion(CrearAmortizacionDTO request);
        Task<AmortizacionClienteDTO> ObtenerPlanAmortizacion(int idCliente);
    }
}
