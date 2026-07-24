using ADaxer.MvvmNav.Abstractions.Dialogs;
using ADaxer.MvvmNav.Abstractions.Navigation;

using CommunityToolkit.Mvvm.Input;

namespace DocuMan.UI.Common.ViewModels;

public partial class ToolBarViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    public ToolBarViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        await _navigationService.ShowDialogAsync<LoginViewModel>();
    }
}
