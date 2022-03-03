using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFExample
{
    class RangeRule : ValidationRule
    {
        double min;
        double max;

        public double Min { get => min; set => min = value; }
        public double Max { get => max; set => max = value; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double outNumber = 0;
            if (!double.TryParse(value.ToString(), out outNumber))
            {
                return new ValidationResult(false, "You have to write a number, without symbols or spaces");
            }
            if (outNumber < min || outNumber > max)
            {
                return new ValidationResult(false, "Incorrect number, Out of Range");
            }
            return ValidationResult.ValidResult;
        }
    }
}
