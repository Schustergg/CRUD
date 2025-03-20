using Crud.Business.Models.Validations;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;


namespace Crud.Business.Entities
{
    public class Product : Entity
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public static Product Create(string name, decimal price)
        {
            return new Product()
            {
                Name = name,
                Price = price
            };
        }

        [NotMapped]
        public ValidationResult Validation { get; set; }

        public bool IsValid()
        {
            var validator = new ProductValidation();
            var result = validator.Validate(this);

            Validation = result;
            return result.IsValid;
        }
    }
}
