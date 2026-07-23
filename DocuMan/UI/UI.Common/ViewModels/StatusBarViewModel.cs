using CommunityToolkit.Mvvm.ComponentModel;

using DocuMan.UI.Common.Interfaces;

namespace DocuMan.UI.Common.ViewModels;

public partial class StatusBarViewModel : ViewModelBase
{
    private readonly IPubSubService _pubSubService;

    public StatusBarViewModel(IPubSubService pubSubService)
    {
        _pubSubService = pubSubService;
    }

    [ObservableProperty]
    private string _statusMessage = string.Empty;
}
