namespace Cantine.Core.Dtos
{
    public record CreditRequestDTO(
        int ClientId,
        decimal Amount
    );
}
