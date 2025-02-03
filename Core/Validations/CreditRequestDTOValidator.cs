using Core.Dtos;
using FluentValidation;

namespace Core.Validations
{
    public class CreditRequestDTOValidator : AbstractValidator<CreditRequestDTO>
    {
        public CreditRequestDTOValidator()
        {
            RuleFor(x => x.ClientId)
                .GreaterThan(0).WithMessage("ClientId must be greater than 0");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than 0");
        }
    }
}
