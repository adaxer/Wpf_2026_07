using CommunityToolkit.Mvvm.ComponentModel;

namespace DocuMan.UI.Common.ViewModels;

public partial class NodeViewModel : ItemViewModel
{
    static readonly object s_defaultItem = new object();

    public NodeViewModel(string name, List<ItemViewModel> children) : base(name, s_defaultItem)
    {
        Children = children;
    }


    [ObservableProperty]
    private List<ItemViewModel> _children;

    [ObservableProperty]
    private bool _isExpanded;

}

public partial class ItemViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private bool _isSelected;

    [ObservableProperty]
    private object _item;

    public ItemViewModel(string name, object item)
    {
        Name = name;
        _item = item;
    }
}
