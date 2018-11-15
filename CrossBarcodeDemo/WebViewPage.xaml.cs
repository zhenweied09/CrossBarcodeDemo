using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CrossBarcodeDemo
{
    public partial class WebViewPage : ContentPage
    {
        public WebViewPage(string url)
        {
            InitializeComponent();

            webView.Source = url;
        }
    }
}
