using System.ComponentModel.DataAnnotations;
using Core.Dtos;
using Core.Entities;
using Core.Enums;
using Core.Interfaces.Mappings;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Interfaces.Validation;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class BillingService : IBillingService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapperService _mapperService;
        private readonly IValidatorService _validatorService;
        private readonly ILogger<BillingService> _logger;

        public BillingService(
            IClientRepository clientRepository,
            IMapperService mapperService,
            IValidatorService validatorService,
            ILogger<BillingService> logger)
        {
            _clientRepository = clientRepository;
            _mapperService = mapperService;
            _validatorService = validatorService;
            _logger = logger;
        }

        /// <summary>
        /// Credits the client's account with the specified amount.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <param name="amount">The amount to credit.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task CreditClientAccount(int clientId, decimal amount)
        {
            _logger.LogInformation("Starting CreditClientAccount for clientId: {ClientId} with amount: {Amount}", clientId, amount);

            var client = await _clientRepository.GetByIdAsync(clientId);
            if (client == null)
            {
                _logger.LogError("Client not found for clientId: {ClientId}", clientId);
                throw new ArgumentException("Client not found");
            }

            client.Budget += amount;
            await _clientRepository.UpdateAsync(client);

            _logger.LogInformation("Successfully credited clientId: {ClientId} with amount: {Amount}", clientId, amount);
        }

        /// <summary>
        /// Processes the payment for a meal and returns a ticket.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <param name="meal">The meal details.</param>
        /// <returns>A task representing the asynchronous operation, with a TicketDTO as the result.</returns>
        public async Task<TicketDTO> PayMeal(int clientId, MealDTO meal)
        {
            _logger.LogInformation("Starting PayMeal for clientId: {ClientId}", clientId);

            // Validation
            var validationResult = _validatorService.Validate(meal);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed for meal: {Meal}", meal);
                throw new ValidationException(validationResult.Errors.ToString());
            }

            var client = await _clientRepository.GetByIdAsync(clientId);
            if (client == null)
            {
                _logger.LogError("Client not found for clientId: {ClientId}", clientId);
                throw new ArgumentException("Client not found");
            }

            decimal totalAmount = CalculateTotalAmount(meal);
            decimal employerCoverage = CalculateEmployerCoverage(client.Type, totalAmount);
            decimal amountToPay = totalAmount - employerCoverage;

            if (amountToPay > client.Budget && client.Type != ClientType.Interne && client.Type != ClientType.VIP)
            {
                _logger.LogError("Insufficient funds for clientId: {ClientId}", clientId);
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

            _logger.LogInformation("Successfully processed PayMeal for clientId: {ClientId}", clientId);
            return _mapperService.Map<TicketDTO>(ticket);
        }

        /// <summary>
        /// Calculates the total amount for the meal.
        /// </summary>
        /// <param name="meal">The meal details.</param>
        /// <returns>The total amount for the meal.</returns>
        private decimal CalculateTotalAmount(MealDTO meal)
        {
            decimal total = 10; // Base price for the meal

            foreach (var supplement in meal.Supplements)
            {
                total += supplement.Price;
            }

            return total;
        }

        /// <summary>
        /// Calculates the employer's coverage based on the client type and total amount.
        /// </summary>
        /// <param name="clientType">The type of the client.</param>
        /// <param name="totalAmount">The total amount for the meal.</param>
        /// <returns>The employer's coverage amount.</returns>
        private decimal CalculateEmployerCoverage(ClientType clientType, decimal totalAmount)
        {
            return clientType switch
            {
                ClientType.Interne => 7.5m,
                ClientType.Prestataire => 6m,
                ClientType.VIP => totalAmount,
                ClientType.Stagiaire => 10m,
                _ => 0m
            };
        }

        /// <summary>
        /// Gets the list of products included in the meal.
        /// </summary>
        /// <param name="meal">The meal details.</param>
        /// <returns>A list of product names included in the meal.</returns>
        private List<string> GetProductsList(MealDTO meal)
        {
            var products = new List<string>
                    {
                        meal.Entree,
                        meal.Plat,
                        meal.Dessert,
                        meal.Pain
                    };

            products.AddRange(meal.Supplements.Select(s => s.Type.ToString()));

            return products;
        }
    }
}
