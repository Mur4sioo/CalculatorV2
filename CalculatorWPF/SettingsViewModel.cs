using System.Windows;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Evaluator;

namespace CalculatorWPF;

public sealed partial class SettingsViewModel : ObservableObject
{
    public SettingsViewModel(ExpressionOptions options)
    {
        this.UseDotForDecimal = options.DecimalPointCharacter is '.';
    }

    [ObservableProperty] private bool useDotForDecimal;
    public ExpressionOptions GetOptions()
    {
        if (this.UseDotForDecimal)
        {
            return new ExpressionOptions('.', ',');
        }
        return new ExpressionOptions(',', '.');
    }
    
    [RelayCommand]
    private void Info()
    {
        MessageBox.Show(
            """
            Additional Fuctions:
            2**3 => 2 to the power of 3
            sqrt(2) => square of 2
            abs(2) => absolute value of 2
            sin(3) => sine of 3
            cos(3) => cosine of 3
            tan(3) => tangens of 3
            clamp(4,2,5) => clamp function
            """
        );
    }
}
