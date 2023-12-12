
using System.Windows.Input;

namespace MediaRegister_WPF.ViewModels;

internal class RelayCommand : ICommand
{
    private Action _action;
    private readonly Func<bool> _canExecute;

    public RelayCommand(Action action, Func<bool> canExecute = null!)
    {
        _action = action;
        _canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        if(_canExecute == null)
        {
            return true;
        }
        return true;// _canExecute();
    }

    public void Execute(object? parameter)
    {
        _action();
    }
}