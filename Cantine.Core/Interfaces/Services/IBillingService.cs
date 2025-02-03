using Cantine.Core.Dtos;

namespace Cantine.Core.Interfaces.Services
{
    public interface IBillingService
    {
        Task CreditClientAccount(int clientId, decimal amount);

        Task<TicketDTO> PayMeal(int clientId, MealDTO meal);
    }
}
