namespace Cantine.Core.Dtos
{
    public record MealDTO(
        int Id,
        string Entree,
        string Plat,
        string Dessert,
        string Pain,
        List<SupplementDTO> Supplements
    );
}
