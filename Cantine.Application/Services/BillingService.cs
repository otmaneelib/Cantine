using System.ComponentModel.DataAnnotations;
using Cantine.Core.Dtos;
using Cantine.Core.Entities;
using Cantine.Core.Interfaces.Mappings;
using Cantine.Core.Interfaces.Repositories;
using Cantine.Core.Interfaces.Services;
using Cantine.Core.Interfaces.Validation;

namespace Cantine.Application.Services
{
    public class BillingService : IBillingService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapperService _mapperService;
        private readonly IValidatorService _validatorService;

        public BillingService(
            IClientRepository clientRepository,
            IMapperService mapperService,
            IValidatorService validatorService)
        {
            _clientRepository = clientRepository;
            _mapperService = mapperService;
            _validatorService = validatorService;
        }

        public async Task CreditClientAccount(int clientId, decimal amount)
        {
            var client = await _clientRepository.GetByIdAsync(clientId);
            if (client == null)
            {
                throw new ArgumentException("Client not found");
            }

            client.Budget += amount;
            await _clientRepository.UpdateAsync(client);
        }

        public async Task<TicketDTO> PayMeal(int clientId, MealDTO meal)
        {
            // Validation
            var validationResult = _validatorService.Validate(meal);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors.ToString());
            }

            var client = await _clientRepository.GetByIdAsync(clientId);
            if (client == null)
            {
                throw new ArgumentException("Client not found");
            }

            decimal totalAmount = CalculateTotalAmount(meal);
            decimal employerCoverage = CalculateEmployerCoverage(client.Type, totalAmount);
            decimal amountToPay = totalAmount - employerCoverage;

            if (amountToPay > client.Budget && client.Type != "Interne" && client.Type != "VIP")
            {
                throw new InvalidOperationException("Insufficient funds");
            }

            client.Budget -= amountToPay;
            await _clientRepository.UpdateAsync(client);

            var ticket = new Ticket
            {
                ClientId = clientId,
                TotalAmount = totalAmount,
                EmployerCoverage = employerCoverage,
                AmountToPay = amountToPay,
                Products = GetProductsList(meal)
            };

            return _mapperService.Map<TicketDTO>(ticket);
        }

        private decimal CalculateTotalAmount(MealDTO meal)
        {
            decimal total = 10; // Base price for the meal

            foreach (var supplement in meal.Supplements)
            {
                total += supplement.Price;
            }

            return total;
        }

        private decimal CalculateEmployerCoverage(string clientType, decimal totalAmount)
        {
            return clientType switch
            {
                "Interne" => 7.5m,
                "Prestataire" => 6m,
                "VIP" => totalAmount,
                "Stagiaire" => 10m,
                _ => 0m
            };
        }

        private List<string> GetProductsList(MealDTO meal)
        {
            var products = new List<string>
            {
                meal.Entree,
                meal.Plat,
                meal.Dessert,
                meal.Pain
            };

            products.AddRange(meal.Supplements.Select(s => s.Type));

            return products;
        }
    }
}
