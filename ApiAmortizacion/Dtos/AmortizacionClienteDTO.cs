namespace ApiAmortizacion.Dtos
{
    public record AmortizacionClienteDTO(
        int ClienteId,
        string Nombre,
        string NumeroIdentidad,
        double MontoPrestamo,
        PlanAmortizacionDTO[] PlanAmortizacion
    );
}
