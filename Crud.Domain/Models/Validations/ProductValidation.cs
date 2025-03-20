using Crud.Business.Entities;
using FluentValidation;

namespace Crud.Business.Models.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided")
                .Length(2, 200).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.Price)
                .GreaterThan(0).WithMessage("The {PropertyName} field must be greater than {ComparisonValue}");
        }
    }
}
