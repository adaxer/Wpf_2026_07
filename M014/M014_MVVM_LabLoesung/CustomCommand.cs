using System.Windows.Input;

namespace MVVM_Personenverwaltung;

//vgl. M13_Commands
public class CustomCommand : ICommand
{
    public Action<object> ExecuteMethode { get; set; }
    public Func<object, bool> CanExecuteMethode { get; set; }

    public CustomCommand(Action<object> exe, Func<object, bool> can = null)
    {
        ExecuteMethode = exe;

        if (can == null) CanExecuteMethode = p => true;
        else CanExecuteMethode = can;
    }

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object parameter)
    {
        return CanExecuteMethode(parameter);
    }

    public void Execute(object parameter)
    {
        ExecuteMethode(parameter);
    }
}
