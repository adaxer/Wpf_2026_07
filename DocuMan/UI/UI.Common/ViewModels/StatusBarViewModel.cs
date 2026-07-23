using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

using DocuMan.UI.Common.Interfaces;
using DocuMan.UI.Common.Messages;

namespace DocuMan.UI.Common.ViewModels;

public partial class StatusBarViewModel : ViewModelBase, IRecipient<StatusMessage>
{
    public StatusBarViewModel(IPubSubService pubSubService)
    {
        pubSubService.Subscribe<StatusMessage>(this);
    }

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    public void Receive(StatusMessage message)
    {
        StatusMessage = message.Message;
    }
}
