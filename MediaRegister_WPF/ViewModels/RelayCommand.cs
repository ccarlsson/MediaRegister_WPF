
using System.Windows.Input;

namespace MediaRegister_WPF.ViewModels;

// This class takes an Action and a Func<bool> as parameters. 
// The Action is the method to be executed when the command is invoked, 
// and the Func<bool> is a method that determines whether the command can be executed.
internal class RelayCommand(Action action, Func<bool> canExecute = null!) : ICommand
{
    private Action _action = action;
    private readonly Func<bool> _canExecute = canExecute;

    // This event is fired when the 'CanExecute' status has changed.
    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    // This method determines whether the command can be executed.
    public bool CanExecute(object? parameter)
    {
        if (_canExecute == null)
        {
            return true;
        }
        return _canExecute();
    }

    // This method executes the command.
    public void Execute(object? parameter)
    {
        _action();
    }
}
