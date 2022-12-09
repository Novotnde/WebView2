using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Webkit;
using App2;
using App2.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using WebView = Android.Webkit.WebView;

[assembly: ExportRenderer(typeof(ExtendedWebView), typeof(ExtendedWebViewRenderer))]

namespace App2.Droid
{
    public class ExtendedWebViewRenderer : WebViewRenderer
    { 
        public static int _webViewHeight;
        static ExtendedWebView _xwebView = null;
        public WebView _webView;
        bool isScroll;

        [Obsolete]
        public ExtendedWebViewRenderer(Context context) : base(context)
        {
          
        }
     
        class ExtendedWebViewClient : WebViewClient
        {
            WebView _webView;
 
            public async override void OnPageFinished(WebView view, string url)
            {
                try
                {
                    _webView = view;
                    if (_xwebView != null)
                    {
                        view.Settings.JavaScriptEnabled = true;
                        await Task.Delay(100);
                        string result = await _xwebView.EvaluateJavaScriptAsync("(function(){return document.body.scrollHeight;})()");
                        _xwebView.HeightRequest = Convert.ToDouble(result);
                    }
                    base.OnPageFinished(view, url);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
            public override bool ShouldOverrideUrlLoading(Android.Webkit.WebView view, IWebResourceRequest request)
            {
                return true;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);
            _xwebView = e.NewElement as ExtendedWebView;
            _webView = Control;

            if (e.OldElement == null)
            {
                _webView.SetWebViewClient(new ExtendedWebViewClient());
            }
        }
       
       
    }
}