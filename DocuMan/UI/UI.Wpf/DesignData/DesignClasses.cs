using DocuMan.Infrastructure.Services;
using DocuMan.UI.Common.ViewModels;
using DocuMan.UI.Wpf.Services;

namespace DocuMan.UI.Wpf.DesignData;

public class DesignStatusBarViewModel : StatusBarViewModel
{
    public DesignStatusBarViewModel() : base(new WpfPubSubService())
    {
    }
}

public class DesignMainViewModel : MainViewModel
{
    public DesignMainViewModel() : base(new DesignStatusBarViewModel(), new DocumentListViewModel(new PdfDocumentService(), new WpfPubSubService()), new ModuleHostViewModel(new WpfPubSubService()), new WpfPubSubService())
    {
    }
}
