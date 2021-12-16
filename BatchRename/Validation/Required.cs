using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BatchRename
{
    public class Required : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string val = (string)value;
            val = val.Trim();
            if (String.IsNullOrEmpty(val) || val.Length == 0)
            {
                return new ValidationResult(false, "Required");
            }
            return new ValidationResult(true, null);
        }
    }
}
