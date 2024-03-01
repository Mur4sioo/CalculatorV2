using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace CalculatorWPF;

public sealed class UniqueNameValidationRule : ValidationRule
{
    public CollectionViewSource? Collection { get; set; }
    public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
    {
        var variables = this.Collection?.Source as IEnumerable<Variable>;
        var currentValue = (value as BindingExpression)?.DataItem as Variable;
        if (variables is null || currentValue is null)
        {
            return ValidationResult.ValidResult;
        }

        if (string.IsNullOrWhiteSpace(currentValue.Name))
        {
            return new ValidationResult(false, "A variable must have a name");
        }

        foreach (var variable in variables)
        {
            if (currentValue == variable)
            {
                continue;
            }
            if (variable.Name == currentValue.Name)
            {
                return new ValidationResult(false, $"{variable.Name} is already in use");
            }
        }
        return ValidationResult.ValidResult;
    }
}