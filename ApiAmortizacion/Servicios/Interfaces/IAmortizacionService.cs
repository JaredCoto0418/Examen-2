using ApiAmortizacion.Dtos;

namespace ApiAmortizacion.Servicios.Interfaces
{
    public interface IAmortizacionService
    {
        // task para crear y obtener plan
        Task<RespuestaCrearAmortizacionDTO> CrearAmortizacion(CrearAmortizacionDTO request);
        Task<AmortizacionClienteDTO> ObtenerPlanAmortizacion(int idCliente);
    }
}
