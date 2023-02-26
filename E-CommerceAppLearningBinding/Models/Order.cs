using E_CommerceAppLearningBinding.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace E_CommerceAppLearningBinding.Models
{
    public class Order : IValidatableObject
    {
        [Required]
        public int? OrderNo { get; set; } = new Random().Next(1, 100000);

        [Required(ErrorMessage = "OrderDate can't be blank")]
        public DateTime? OrderDate { get; set; }

        [Required(ErrorMessage = "InvoicePrice can't be blank")]
        [ValidationInvoicePrice("Products")]
        public double? InvoicePrice { get; set; }

        [Required(ErrorMessage = "Products can't be blank")]
        public List<Product>? Products { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Products?.Count < 1)
            {
                yield return new ValidationResult("Products count should be more or equal to 1");
            }

            if (OrderDate < new DateTime(2000, 1, 1))
            {
                yield return new ValidationResult("Order date should be greater than or equal to 2000-01-01");
            }
        }
    }
}
