using System.Windows.Input;

namespace M14_MVVM;

//vgl. M13_Commands
public class CustomCommand : ICommand
{
    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public Action<object> ExecuteMethode { get; set; }
    public Func<object, bool> CanExecuteMethode { get; set; }

    public bool CanExecute(object? parameter) => CanExecuteMethode(parameter);

    public void Execute(object? parameter) => ExecuteMethode(parameter);


    public CustomCommand(Action<object> exe, Func<object, bool> can = null)
    {
        ExecuteMethode = exe;

        if (can == null) CanExecuteMethode = p => true;
        else CanExecuteMethode = can;
    }
}
