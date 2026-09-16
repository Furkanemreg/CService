using System.Windows;

namespace CService.Desktop;

public partial class MainWindow : Window
{
    private readonly string _startUrl;

    public MainWindow(string startUrl)
    {
        InitializeComponent();
        _startUrl = startUrl;
        Loaded += async (_, _) =>
        {
            await webView.EnsureCoreWebView2Async();
            webView.CoreWebView2.Navigate(_startUrl);
        };
    }
}