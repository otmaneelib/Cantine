using Core.Interfaces.Validation;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Validations
{
    public class ValidatorService : IValidatorService
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidatorService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ValidationResult Validate<T>(T entity)
        {
            var validator = _serviceProvider.GetService<IValidator<T>>();
            if (validator == null)
            {
                throw new ValidationException($"No validator found for {typeof(T).Name}");
            }

            return validator.Validate(entity);
        }
    }
}
