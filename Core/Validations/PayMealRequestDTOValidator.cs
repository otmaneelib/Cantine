using Core.Dtos;
using FluentValidation;

namespace Core.Validations
{
    public class PayMealRequestDTOValidator : AbstractValidator<PayMealRequestDTO>
    {
        public PayMealRequestDTOValidator()
        {
            RuleFor(x => x.ClientId)
                .GreaterThan(0).WithMessage("ClientId must be greater than 0");

            RuleFor(x => x.Meal)
                .NotNull().WithMessage("Meal cannot be null");

            RuleFor(x => x.Meal.Entree)
                .NotEmpty().WithMessage("Entree is required");

            RuleFor(x => x.Meal.Plat)
                .NotEmpty().WithMessage("Plat is required");

            RuleFor(x => x.Meal.Dessert)
                .NotEmpty().WithMessage("Dessert is required");

            RuleFor(x => x.Meal.Pain)
                .NotEmpty().WithMessage("Pain is required");
        }
    }
}
