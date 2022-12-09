using App2;
using App2.iOS;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedWebView), typeof(ExtendedWebViewRenderer))]
namespace App2.iOS
{
    public class ExtendedWebViewRenderer : ViewRenderer<ExtendedWebView, WKWebView>
    {      
        WKWebView _wkWebView;
        static ExtendedWebView _xwebView = null;

        protected  override void OnElementChanged(ElementChangedEventArgs<ExtendedWebView> e)
        {
            base.OnElementChanged(e);
            
            if (Control == null)
            {
                var config = new WKWebViewConfiguration();
                _wkWebView = new WKWebView(Frame, config);
                _wkWebView.NavigationDelegate = new CustomWKNavigationDelegate(this);
                SetNativeControl(_wkWebView);
            }
            
            if (e.NewElement != null)
            {
                HtmlWebViewSource source = (Xamarin.Forms.HtmlWebViewSource)Element.Source;
                string html = source?.Html ?? string.Empty;
                if(string.IsNullOrWhiteSpace(html))
                    _wkWebView.LoadHtmlString(html, baseUrl: null);
                _wkWebView.ScrollView.ScrollEnabled = false;
                _wkWebView.SizeToFit();
            }
        }
    }
        
    public class CustomWKNavigationDelegate : WKNavigationDelegate
    {

        ExtendedWebViewRenderer _webViewRenderer;

        public CustomWKNavigationDelegate(ExtendedWebViewRenderer webViewRenderer)
        {
            _webViewRenderer = webViewRenderer;
        }

        public  override async void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
        {
            var wv = _webViewRenderer.Element as ExtendedWebView;
            if (wv != null)
            {
                await System.Threading.Tasks.Task.Delay(100); // wait here till content is rendered
                wv.HeightRequest = (double)webView.ScrollView.ContentSize.Height;
            }
        }
        
    }
}