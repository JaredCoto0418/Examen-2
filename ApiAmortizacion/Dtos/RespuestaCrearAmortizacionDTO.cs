namespace ApiAmortizacion.Dtos
{
    // RESPUESTA DTO CREACION DE AMORTIZACION
    public record RespuestaCrearAmortizacionDTO(string Mensaje, PlanAmortizacionDTO[] planAmortizacion);
}
