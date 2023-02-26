using E_CommerceAppLearningBinding.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace E_CommerceAppLearningBinding.Models
{
    public class Order : IValidatableObject
    {
        [Display(Name = "Order Number")]
        public int? OrderNo { get; set; } = new Random().Next(1, 100000);

        [Required(ErrorMessage = "{0} can't be blank")]
        [Display(Name = "Order Date")]
        public DateTime? OrderDate { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        [Display(Name = "Invoice Price")]
        [ValidationInvoicePrice("Products")]
        [Range(1, double.MaxValue, ErrorMessage = "{0} should be between a valid number")]
        public double? InvoicePrice { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
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
