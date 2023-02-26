using E_CommerceAppLearningBinding.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace E_CommerceAppLearningBinding.CustomValidation
{
    public class ValidationInvoicePriceAttribute : ValidationAttribute
    {
        public string DefaultErrorMessage { get; set; } = "Invoice Price should be equal to the total cost of all products (i.e. {0}) in the order.";
        public string Products { get; set; }
        public ValidationInvoicePriceAttribute(string products)
        {
            Products = products;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not null)
            {
                double invoicePrice = (double)value;

                PropertyInfo? listProductsProperty = validationContext.ObjectType.GetProperty(Products);

                if (listProductsProperty is not null)
                {
                    List<Product> products = (List<Product>)listProductsProperty.GetValue(validationContext.ObjectInstance)!;

                    double calculatedInvoicePrice = 0;
                    foreach (Product product in products)
                    {
                        if (product.Price is not null && product.Quantity is not null)
                        {
                            calculatedInvoicePrice += (double)product.Price * (double)product.Quantity;
                        }
                    }

                    if (calculatedInvoicePrice > 0 && invoicePrice != calculatedInvoicePrice)
                    {
                        if (invoicePrice != calculatedInvoicePrice)
                        {
                            return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, calculatedInvoicePrice), new string[] { nameof(validationContext.MemberName) });
                        }
                    }
                    else
                    {
                        return new ValidationResult("No products found to validate invoice price", new string[] { nameof(validationContext.MemberName) });
                    }

                    return ValidationResult.Success;
                }

                return null;
            }

            return null;
        }
    }
}
