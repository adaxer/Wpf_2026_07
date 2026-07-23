using DocuMan.UI.Common.ViewModels;
using DocuMan.UI.Wpf.Services;

using Infrastructure.Services;

namespace DocuMan.UI.Wpf.DesignData;

public class DesignStatusBarViewModel : StatusBarViewModel
{
    public DesignStatusBarViewModel() : base(new WpfPubSubService())
    {
    }
}

public class DesignMainViewModel : MainViewModel
{
    public DesignMainViewModel() : base(new DesignStatusBarViewModel(), new DocumentsViewModel(new PdfDocumentService(), new WpfPubSubService()), new WpfPubSubService())
    {
    }
}
