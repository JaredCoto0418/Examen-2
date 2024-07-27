namespace ApiAmortizacion.Dtos
{
    public record PlanAmortizacionDTO(
        int NumeroCouta,
        string FechaPago,
        int Dias,
        double Intereses,
        double Capital,
        double PagoNiveladoSinSVSD,
        double PagoNiveladoConSVSD,
        double SaldoCapital
    );
}
