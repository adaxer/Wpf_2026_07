using CommunityToolkit.Mvvm.ComponentModel;

using DocuMan.UI.Common.Interfaces;
using DocuMan.UI.Common.Messages;

namespace DocuMan.UI.Common.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public MainViewModel(StatusBarViewModel statusBar, IPubSubService pubSubService)
    {
        StatusBar = statusBar;
        _pubSubService = pubSubService;
        _pubSubService.Publish(new StatusMessage("MainViewModel Ready"));
        Title = "DocuMan";
    }

    [ObservableProperty]
    private StatusBarViewModel _statusBar;
    private readonly IPubSubService _pubSubService;
}
