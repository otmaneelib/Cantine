using FluentValidation.Results;

namespace Cantine.Core.Interfaces.Validation
{
    public interface IValidatorService
    {
        ValidationResult Validate<T>(T entity);
    }
}
