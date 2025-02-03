namespace Core.Dtos
{
    public record TicketDTO(
        int Id,
        int ClientId,
        decimal TotalAmount,
        decimal EmployerCoverage,
        decimal AmountToPay,
        List<string> Products
    );
}
