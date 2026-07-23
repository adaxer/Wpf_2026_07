using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DocuMan.Domain.Models;
using DocuMan.UI.Common.ViewModels;

namespace DocuMan.UI.Wpf.Views;
/// <summary>
/// Interaction logic for DocumentListView.xaml
/// </summary>
public partial class DocumentListView : UserControl
{
    public DocumentListView()
    {
        InitializeComponent();
    }

    //Konnte entfernt werden, weil EventToCommand sauberer ist
    //private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    //{
    //    Trace.WriteLine($"Selected item changed: {e.OldValue} => {e.NewValue}");
    //    if(e.NewValue is ItemViewModel selectedItem && selectedItem.Item is PdfDocument pdfDocument && DataContext is DocumentListViewModel viewModel)
    //    {
    //        viewModel.ShowPdf(pdfDocument);
    //    }
    //}
}
