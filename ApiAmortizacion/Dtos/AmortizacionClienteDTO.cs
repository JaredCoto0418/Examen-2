namespace ApiAmortizacion.Dtos
{
    // DATIS DE CLIENTE 
    public record AmortizacionClienteDTO(
        int ClienteId,
        string Nombre,
        string NumeroIdentidad,
        double MontoPrestamo,
        PlanAmortizacionDTO[] PlanAmortizacion
    );
}
