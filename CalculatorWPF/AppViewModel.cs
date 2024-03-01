using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Evaluator;

namespace CalculatorWPF;

public enum CalculatorView
{
    Calculator,
    Settings,
    Variables
}

public partial class AppViewModel : ObservableObject
{
    private ExpressionOptions options = ExpressionOptions.Default;
    private Dictionary<string, double> variables = new();

    public AppViewModel()
    {
        this.SelectedPage = new CalculatorViewModel(options, variables);
    }
    
    private object selectedPage;
    public object SelectedPage
    {
        get => this.selectedPage;
        private set => this.SetProperty(ref this.selectedPage, value);
    }

    [RelayCommand]
    private void SwitchView(CalculatorView view)
    {
        //if (SelectedPage.View == view)
        //    return;
        switch (this.SelectedPage)
        {
            case SettingsViewModel settingsPage:
                this.options = settingsPage.GetOptions();
                break;
            case VariablesViewModel variablesPage:
                this.variables = variablesPage.GetVariables();
                break;
        }
        {
            
        }
        this.SelectedPage = view switch
        {
            CalculatorView.Calculator => new CalculatorViewModel(options,variables),
            CalculatorView.Settings => new SettingsViewModel(options),
            CalculatorView.Variables => new VariablesViewModel(variables),
            _ => throw new ArgumentOutOfRangeException(nameof(view),view,default)
        };
    }
}