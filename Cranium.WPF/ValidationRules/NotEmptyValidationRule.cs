using System.Globalization;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Cranium.WPF.ValidationRules
{
    [ContentProperty(nameof(ErrorMessage))]
    public class NotEmptyValidationRule : ValidationRule
    {
        public string ErrorMessage { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                ? new ValidationResult(false, App.Strings.Required)
                : ValidationResult.ValidResult;
        }
    }
}
