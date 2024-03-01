﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CalculatorWPF;

public sealed partial class Variable : ObservableValidator
{
    public Variable()
    {
        this.ValidateAllProperties();
    }
    [ObservableProperty]
    private string name = "";
    [ObservableProperty] private double value;
}
public sealed partial class VariablesViewModel : ObservableObject
{
    public VariablesViewModel(Dictionary<string, double> variables)
    {
        this.Variables = new(
            variables.Select(variable => new Variable() { Name = variable.Key, Value = variable.Value })
        );
    }
    public ObservableCollection<Variable> Variables { get; }
    
    public Dictionary<string, double> GetVariables()
    {
        return this.Variables.ToDictionary(
            variable => variable.Name,
            variable => variable.Value
        );
    }
}

