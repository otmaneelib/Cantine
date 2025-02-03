namespace Cantine.Core.Dtos
{
    public record ClientDTO(
        int Id,
        string Name,
        decimal Budget,
        string Type // Interne, Prestataire, VIP, Stagiaire, Visiteur
    );
}
