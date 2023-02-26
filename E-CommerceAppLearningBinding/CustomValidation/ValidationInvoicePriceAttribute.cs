using E_CommerceAppLearningBinding.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace E_CommerceAppLearningBinding.CustomValidation
{
    public class ValidationInvoicePriceAttribute : ValidationAttribute
    {
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
                    List<Product> products = listProductsProperty.GetValue(validationContext.ObjectInstance) as List<Product>;

                    double calculatedInvoicePrice = 0;
                    foreach (Product product in products)
                    {
                        if (product.Price is not null && product.Quantity is not null)
                        {
                            calculatedInvoicePrice += (double)product.Price * (double)product.Quantity;
                        }
                    }

                    if (invoicePrice != calculatedInvoicePrice)
                    {
                        return new ValidationResult("InvoicePrice doesn't match with the total cost of the specified products in the order.");
                    }
                    else
                    {
                        return ValidationResult.Success;
                    }
                }
            }

            return null;
        }
    }
}
