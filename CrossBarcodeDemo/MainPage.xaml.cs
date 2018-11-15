using System;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace CrossBarcodeDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var barcode = new ZXingBarcodeImageView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            barcode.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
            barcode.BarcodeOptions.Width = 300;
            barcode.BarcodeOptions.Height = 300;
            barcode.BarcodeOptions.Margin = 10;
            barcode.BarcodeValue = "https://github.com/Redth/ZXing.Net.Mobile";

            contentStack.Children.Add(barcode);

            var entry = new Entry
            {
                Placeholder = "Enter your new url",
                Text = "https://github.com/Redth/ZXing.Net.Mobile"
            };
            var button = new Button
            {
                Text = "Create Barcode"
            };
            button.Clicked += (sender, obj) => {
                if (entry.Text != null)
                    barcode.BarcodeValue = entry.Text;
            };
            contentStack.Children.Add(entry);
            contentStack.Children.Add(button);
        }

        async void OnScanBarCode(object sender, EventArgs e)
        {
            var scanPage = new ZXingScannerPage();

            scanPage.OnScanResult += (result) =>
            {
                // Stop scanning
                scanPage.IsScanning = false;

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopAsync();
                    scanResult.Text = result.Text;
                    DisplayAlert("Scanned Barcode", result.Text, "OK");


                    Navigation.PushModalAsync(new WebViewPage(result.Text));
                });

            };

            // Navigate to our scanner page
            await Navigation.PushAsync(new NavigationPage(scanPage));
        }
    }
}
