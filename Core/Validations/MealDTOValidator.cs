using Core.Dtos;
using FluentValidation;

namespace Core.Validations
{
    public class MealDTOValidator : AbstractValidator<MealDTO>
    {
        public MealDTOValidator()
        {
            RuleFor(m => m.Entree)
                .NotNull().WithMessage("Entree is required")
                .NotEmpty().WithMessage("Entree cannot be empty")
                .Length(1, 100).WithMessage("Entree must be between 1 and 100 characters");

            RuleFor(m => m.Plat)
                .NotNull().WithMessage("Plat is required")
                .NotEmpty().WithMessage("Plat cannot be empty")
                .Length(1, 100).WithMessage("Plat must be between 1 and 100 characters");

            RuleFor(m => m.Dessert)
                .NotNull().WithMessage("Dessert is required")
                .NotEmpty().WithMessage("Dessert cannot be empty")
                .Length(1, 100).WithMessage("Dessert must be between 1 and 100 characters");

            RuleFor(m => m.Pain)
                .NotNull().WithMessage("Pain is required")
                .NotEmpty().WithMessage("Pain cannot be empty")
                .Length(1, 100).WithMessage("Pain must be between 1 and 100 characters");
        }
    }
}
