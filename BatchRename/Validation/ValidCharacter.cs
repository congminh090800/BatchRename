using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BatchRename
{
    class ValidCharacter : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (((string)value).IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                return new ValidationResult(false, $"Prefix must not contain invalid characters");
            }
            return new ValidationResult(true, null);
        }
    }
}
