using CommunityToolkit.Mvvm.ComponentModel;

namespace DocuMan.UI.Common.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public MainViewModel(StatusBarViewModel statusBar)
    {
        StatusBar = statusBar;
        StatusBar.StatusMessage = "MainView Ready!";
        Title = "DocuMan";
    }

    [ObservableProperty]
    private StatusBarViewModel _statusBar;
}
