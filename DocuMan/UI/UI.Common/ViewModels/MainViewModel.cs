using CommunityToolkit.Mvvm.ComponentModel;

using DocuMan.UI.Common.Interfaces;
using DocuMan.UI.Common.Messages;

namespace DocuMan.UI.Common.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IPubSubService _pubSubService;
   
    public MainViewModel(StatusBarViewModel statusBar, DocumentListViewModel documents, ModuleHostViewModel moduleHost, IPubSubService pubSubService)
    {
        StatusBar = statusBar;
        Documents = documents;
        ModuleHost = moduleHost;
        _pubSubService = pubSubService;
        _pubSubService.Publish(new StatusMessage("MainViewModel Ready"));
        Title = "DocuMan";
    }

    [ObservableProperty]
    private ModuleHostViewModel _moduleHost;

    [ObservableProperty]
    private StatusBarViewModel _statusBar;

    [ObservableProperty]
    private DocumentListViewModel _documents;

}
