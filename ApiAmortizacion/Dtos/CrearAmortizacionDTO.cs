namespace ApiAmortizacion.Dtos
{

    //
    public record CrearAmortizacionDTO(
        string Nombre,
        string NumeroIdentidad,
        double MontoPrestado,
        double TasaComision,
        double TasaInteres,
        int plazo,
        DateTime FechaDesembolso,
        DateTime FechaPrimerPago
    );

}
