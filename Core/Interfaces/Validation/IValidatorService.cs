using FluentValidation.Results;

namespace Core.Interfaces.Validation
{
    public interface IValidatorService
    {
        ValidationResult Validate<T>(T entity);
    }
}
