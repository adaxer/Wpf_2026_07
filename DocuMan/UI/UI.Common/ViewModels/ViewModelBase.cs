using System.Diagnostics;

using CommunityToolkit.Mvvm.ComponentModel;

namespace DocuMan.UI.Common.ViewModels;

public partial class ViewModelBase : ObservableObject //, INotifyPropertyChanged nicht mehr gebraucht übers Toolkit
{
    [ObservableProperty]
    private string _title = nameof(ViewModelBase);

    partial void OnTitleChanged(string? oldValue, string newValue)
    {
        Trace.TraceInformation($"Title changed from {oldValue} to {newValue}.");
    }

    [ObservableProperty]
    private bool _isBusy;

    // Das müsste in jede Property fest einkodiert sein, wenn CommunityToolkit nicht verwendet wird
    //private string _title = nameof(MainViewModel);
    //public string Title
    //{
    //    get => _title;
    //    set
    //    {
    //        _title = value;
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)); // Updates alle Bindings des MainViewModels
    //        //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
    //    }
    //}

    // public event PropertyChangedEventHandler? PropertyChanged; jetzt in Basisklasse ObservableObject
}
