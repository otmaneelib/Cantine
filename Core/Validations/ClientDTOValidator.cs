using Core.Dtos;
using FluentValidation;

namespace Core.Validations
{
    public class ClientDTOValidator : AbstractValidator<ClientDTO>
    {
        public ClientDTOValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(c => c.Budget).GreaterThanOrEqualTo(0).WithMessage("Budget must be positive");
            RuleFor(c => c.Type).NotEmpty().WithMessage("Type is required");
        }
    }
}
