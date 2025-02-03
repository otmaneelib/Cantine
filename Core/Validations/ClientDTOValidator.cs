using Core.Dtos;
using Core.Enums;
using FluentValidation;

namespace Core.Validations
{
    public class ClientDTOValidator : AbstractValidator<ClientDTO>
    {
        public ClientDTOValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters");

            RuleFor(c => c.Budget)
                .GreaterThanOrEqualTo(0).WithMessage("Budget must be positive")
                .ScalePrecision(2, 18).WithMessage("Budget must be a valid decimal number with up to 2 decimal places");

            RuleFor(c => c.Type)
                .NotEmpty().WithMessage("Type is required")
                .Must(BeAValidType).WithMessage("Type is not valid");
        }

        private bool BeAValidType(string type)
        {
            return Enum.TryParse(typeof(ClientType), type, out _);
        }
    }
}
