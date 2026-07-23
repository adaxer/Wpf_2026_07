using CommunityToolkit.Mvvm.ComponentModel;

using DocuMan.UI.Common.Interfaces;
using DocuMan.UI.Common.Messages;

namespace DocuMan.UI.Common.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IPubSubService _pubSubService;
   
    public MainViewModel(StatusBarViewModel statusBar, DocumentsViewModel documents, IPubSubService pubSubService)
    {
        StatusBar = statusBar;
        Documents = documents;
        _pubSubService = pubSubService;
        _pubSubService.Publish(new StatusMessage("MainViewModel Ready"));
        Title = "DocuMan";
    }

    [ObservableProperty]
    private StatusBarViewModel _statusBar;

    [ObservableProperty]
    private DocumentsViewModel _documents;

}
