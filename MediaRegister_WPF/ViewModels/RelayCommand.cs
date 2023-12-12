
using System.Windows.Input;

namespace MediaRegister_WPF.ViewModels;

internal class RelayCommand(Action action, Func<bool> canExecute = null!) : ICommand
{
    private Action _action = action;
    private readonly Func<bool> _canExecute = canExecute;

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object? parameter)
    {
        if(_canExecute == null)
        {
            return true;
        }
        return _canExecute();
    }

    public void Execute(object? parameter)
    {
        _action();
    }
}