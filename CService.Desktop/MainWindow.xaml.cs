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
            webView.CoreWebView2.HistoryChanged += (_, _) => UpdateNavButtons();
            webView.CoreWebView2.NavigationCompleted += (_, _) => UpdateNavButtons();
        };
    }

    private void UpdateNavButtons()
    {
        btnBack.IsEnabled = webView.CoreWebView2?.CanGoBack ?? false;
        btnForward.IsEnabled = webView.CoreWebView2?.CanGoForward ?? false;
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
        if (webView.CoreWebView2?.CanGoBack == true)
            webView.CoreWebView2.GoBack();
    }

    private void BtnForward_Click(object sender, RoutedEventArgs e)
    {
        if (webView.CoreWebView2?.CanGoForward == true)
            webView.CoreWebView2.GoForward();
    }

    private void BtnReload_Click(object sender, RoutedEventArgs e) => webView.CoreWebView2?.Reload();

    private void BtnHome_Click(object sender, RoutedEventArgs e) => webView.CoreWebView2?.Navigate(_startUrl);
}