using System.ComponentModel.DataAnnotations;

namespace Crud.API.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }


        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(200, ErrorMessage = "The field {0} must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }


        [Required(ErrorMessage = "The field {0} is required")]
        public decimal Price { get; set; }
    }
}
