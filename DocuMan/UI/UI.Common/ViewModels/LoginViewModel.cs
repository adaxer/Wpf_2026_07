using ADaxer.MvvmNav.Core.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;

namespace DocuMan.UI.Common.ViewModels;

public partial class LoginViewModel : DialogViewModelBase
{
    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty] 
    private string _passWord = string.Empty;
}