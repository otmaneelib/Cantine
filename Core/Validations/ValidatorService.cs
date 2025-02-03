using Core.Interfaces.Validation;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core.Validations
{
    public class ValidatorService : IValidatorService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ValidatorService> _logger;

        public ValidatorService(IServiceProvider serviceProvider, ILogger<ValidatorService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public ValidationResult Validate<T>(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity to validate cannot be null");
            }

            var validator = _serviceProvider.GetService<IValidator<T>>();
            if (validator == null)
            {
                _logger.LogError($"No validator found for {typeof(T).Name}");
                return new ValidationResult(new[] { new ValidationFailure(typeof(T).Name, $"No validator found for {typeof(T).Name}") });
            }

            var result = validator.Validate(entity);
            if (!result.IsValid)
            {
                _logger.LogWarning($"Validation failed for {typeof(T).Name}: {string.Join(", ", result.Errors.Select(e => e.ErrorMessage))}");
            }

            return result;
        }
    }
}
