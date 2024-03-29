﻿using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Evaluator;

namespace CalculatorWPF;

public sealed partial class CalculatorViewModel : ObservableObject
{
    private ExpressionOptions options;
    private readonly Dictionary<string, double> variables;

    public CalculatorViewModel(ExpressionOptions options, Dictionary<string, double> variables)
    {
        this.options = options;
        this.variables = variables;
    }
    [ObservableProperty]
    private string display  = "";
    
    [ObservableProperty]
    private string history = "";
    
    private bool CanEnterNumber(string? parameter)
    {
        return parameter is string;
    }

    [RelayCommand(CanExecute = nameof(CanEnterNumber))]
    private void Number(string? parameter)
    {
        if (parameter is not null)
            this.Display += parameter;
    }
    [RelayCommand]
    private void BackButton()
    {
        var text = this.Display;
        if (text.Length == 0)
            return;
        text = text[..^1];
        this.Display = text;
    }

    [RelayCommand]
    private void EqualsButton()
    {
        
        var doubleResult = CalculatorEngine.Evaluate(this.Display, variables, options);
        var textResult = doubleResult.ToString(CultureInfo.InvariantCulture);
        var convertedTextResult = ExpressionOptions.ChangeDecimalPoint(
            textResult,
            convertFrom: '.',
            convertTo: options.DecimalPointCharacter
        );
        this.History = $"{this.Display}={convertedTextResult}";
        this.Display = "";
    }
}
    

