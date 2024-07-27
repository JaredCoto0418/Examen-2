namespace ApiAmortizacion.Dtos
{
    // RESPUESTA 
    public record RespuestaCrearAmortizacionDTO(string Mensaje, PlanAmortizacionDTO[] planAmortizacion);
}
