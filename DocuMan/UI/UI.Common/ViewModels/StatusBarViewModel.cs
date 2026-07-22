using CommunityToolkit.Mvvm.ComponentModel;

namespace DocuMan.UI.Common.ViewModels;

public partial class StatusBarViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _statusMessage = string.Empty;
}
