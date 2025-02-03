using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class MealRepository : RepositoryBase<Meal>, IMealRepository
    {
        public MealRepository(AppDbContext context, ILogger<RepositoryBase<Meal>> logger) : base(context, logger)
        {
        }

        public async Task<Meal> GetMealWithSupplementsAsync(int id)
        {
            return await _context.Meals
                .Include(m => m.Supplements)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
