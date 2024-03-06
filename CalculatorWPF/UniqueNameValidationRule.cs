using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using Evaluator;

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

        var error = CheckForValidName(currentValue) ?? CheckDuplicateName(currentValue, variables);
        if (error is null)
            return ValidationResult.ValidResult;
        return new ValidationResult(false, error);
    }

    private static string? CheckForValidName(Variable variable)
    {
        if (string.IsNullOrWhiteSpace(variable.Name))
        {
            return "A variable must have a name";
        }

        if (variable.Name.Length > 1)
        {
            return "A variable's name can only be one letter";
        }

        if (char.IsAsciiLetter(variable.Name[0]) == false)
        {
            return "A variable name must be a letter";
        }

        return null;
    }

    private static string? CheckDuplicateName(Variable currentValue, IEnumerable<Variable> variables)
    {
        foreach (var variable in variables)
        {
            if (currentValue == variable)
            {
                continue;
            }

            if (variable.Name == currentValue.Name)
            {
                return $"{variable.Name} is already in use";
            }
        }

        return null;
    }
}