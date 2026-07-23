using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using DocuMan.UI.Common.Interfaces;

namespace DocuMan.UI.Wpf.Services;

public class WpfPubSubService : IPubSubService
{
    public void Publish<T>(T message) where T : class
    {
        WeakReferenceMessenger.Default.Send(message);
    }

    public void Subscribe<T>(object subscriber) where T : class
    {
        if (subscriber is IRecipient<T> recipient)
        {
            var handler = new MessageHandler<object, T>((r, m) =>
            {
                if (!(r is IRecipient<T> recipient))
                {
                    return;
                }
                if (!Application.Current.Dispatcher.CheckAccess())
                {
                    Application.Current.Dispatcher.Invoke(() => recipient.Receive(m));
                }
                else
                {
                    recipient.Receive(m);
                }
            });
            WeakReferenceMessenger.Default.Register(recipient, handler);
        }
        else
        {
            throw new ArgumentException("Must be an IRecipient<T>");
        }
    }

    public void Unsubscribe<T>(object subscriber) where T : class
    {
        if (subscriber is IRecipient<T> recipient)
        {
            WeakReferenceMessenger.Default.Unregister<T>(recipient);
        }
        else
        {
            throw new ArgumentException("Must be an IRecipient<T>");
        }
    }
}
