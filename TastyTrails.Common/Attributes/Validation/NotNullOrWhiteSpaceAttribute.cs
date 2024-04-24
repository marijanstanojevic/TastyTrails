using System.ComponentModel.DataAnnotations;

namespace TastyTrails.Common.Attributes.Validation
{
    public class NotNullOrWhiteSpaceAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string valueString && !string.IsNullOrWhiteSpace(valueString))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"{validationContext.DisplayName} cannot be null, empty or white space.");
        }
    }
}
