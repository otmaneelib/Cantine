using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IMealRepository
    {
        Task<Meal> GetMealWithSupplementsAsync(int id);
    }
}
