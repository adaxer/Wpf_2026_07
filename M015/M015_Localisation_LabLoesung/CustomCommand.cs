using System.Windows.Input;

namespace MVVM_Personenverwaltung;

public class CustomCommand : ICommand
{
    public Action<object> ExecuteMethode { get; set; }
    public Func<object, bool> CanExecuteMethode { get; set; }

    public CustomCommand(Func<object, bool> can, Action<object> exe)
    {
        CanExecuteMethode = can;
        ExecuteMethode = exe;
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
