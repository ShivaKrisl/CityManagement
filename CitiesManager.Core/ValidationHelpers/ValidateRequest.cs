using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesManager.Core.ValidationHelpers
{
    public static class ValidateRequest
    {

        public static string ErrorMessage { get; set; } = string.Empty;

        public static bool IsModelValid(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            ValidationContext context = new ValidationContext(obj);
            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(obj, context, results, true);

            if (!isValid)
            {
                foreach (var validationResult in results)
                {
                    ErrorMessage += validationResult.ErrorMessage + "\n";
                }
            }
            return isValid;
        }
    }
}
