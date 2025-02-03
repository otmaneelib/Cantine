namespace Core.Dtos
{
    public record SupplementDTO(
        int Id,
        string Type, // Boisson, Fromage, Pain, etc.
        decimal Price
    );
}
