using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class Validation
    {
        public static void ObjectValidator(Object obj)
        {
            var validator = new ValidationContext(obj);

            // A list to hold the validation result.
            var errors = new List<ValidationResult>();

            if (!Validator.TryValidateObject(obj, validator, errors, true))
                foreach (var error in errors)
                    throw new ApplicationException($"\n{error.ErrorMessage}");
        }
    }
}
