using Core.Dtos;
using FluentValidation;

namespace Core.Validations
{
    public class MealDTOValidator : AbstractValidator<MealDTO>
    {
        public MealDTOValidator()
        {
            RuleFor(m => m.Entree).NotEmpty().WithMessage("Entree is required");
            RuleFor(m => m.Plat).NotEmpty().WithMessage("Plat is required");
            RuleFor(m => m.Dessert).NotEmpty().WithMessage("Dessert is required");
            RuleFor(m => m.Pain).NotEmpty().WithMessage("Pain is required");
        }
    }
}
