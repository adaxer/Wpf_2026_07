using ADaxer.MvvmNav.Abstractions.Navigation;

using CommunityToolkit.Mvvm.ComponentModel;

using DocuMan.UI.Common.Interfaces;
using DocuMan.UI.Common.Messages;

namespace DocuMan.UI.Common.ViewModels;

public partial class MainViewModel : ViewModelBase, IShellViewModel
{
    private readonly IPubSubService _pubSubService;
   
    public MainViewModel(StatusBarViewModel statusBar, DocumentListViewModel documents, ToolBarViewModel toolBar, IPubSubService pubSubService)
    {
        StatusBar = statusBar;
        Documents = documents;
        ToolBar = toolBar;
        _pubSubService = pubSubService;
        _pubSubService.Publish(new StatusMessage("MainViewModel Ready"));
        Title = "DocuMan";
    }

    [ObservableProperty]
    private StatusBarViewModel _statusBar;

    [ObservableProperty]
    private ToolBarViewModel _toolBar;

    [ObservableProperty]
    private DocumentListViewModel _documents;

    [ObservableProperty]
    private object? _currentModule;

    [ObservableProperty]
    private object? _currentDialog;
}
