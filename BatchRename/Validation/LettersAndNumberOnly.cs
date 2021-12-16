using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BatchRename
{
    public class LettersAndNumbersOnly : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9]+$");
            return !regex.IsMatch((string)value)
                ? new ValidationResult(false, "Extension must contains only numbers and letters")
                : new ValidationResult(true, null);
        }
    }
}
