namespace Core.Dtos
{
    public record PayMealRequestDTO(
        int ClientId,
        MealDTO Meal
    );
}
