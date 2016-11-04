using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ZXing;

using ZXing.Mobile;
using System.Collections.Generic;


namespace MyListZxingMobile
{
    [Activity(Label = "MyListZxingMobile", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        TextureView _textureview;
        MobileBarcodeScanner scanner;
        Button button;
        Button buttonContinuousScan;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            MobileBarcodeScanner.Initialize(Application);
            var scanner = new MobileBarcodeScanner();
            // Get our button from the layout resource,
            // and attach an event to it
            button = FindViewById<Button>(Resource.Id.MyButton);
            buttonContinuousScan = FindViewById<Button>(Resource.Id.button1);
            _textureview = FindViewById<TextureView>(Resource.Id.textureView1);
            button.Click += async (object sender, EventArgs e) =>
            {
                MobileBarcodeScanningOptions options = new MobileBarcodeScanningOptions();
                options.PossibleFormats = new List<ZXing.BarcodeFormat>() {ZXing.BarcodeFormat.EAN_8, ZXing.BarcodeFormat.EAN_13};

                options.TryHarder = true;
                options.TryInverted = true;
                options.UseFrontCameraIfAvailable = true;
                scanner.AutoFocus();
               


                //  scanner.UseCustomOverlay = true;
                //  scanner.CustomOverlay = _textureview;
                scanner.TopText = "Hold the camera up to the barcode\nAbout 6 inches away";
                scanner.BottomText = "Wait for the barcode to automatically scan!";
                var result = await scanner.Scan(this, options);

                

                if (result != null)
                {
                  
                    button.Text = "Scanned Barcode: " + result.Text;
                }
                    
            };

            buttonContinuousScan.Click +=  (object sender, EventArgs e) =>
            {
                scanner.UseCustomOverlay = false;
                scanner.AutoFocus();
               
                //We can customize the top and bottom text of the default overlay
                scanner.TopText = "Hold the camera up to the barcode\nAbout 6 inches away";
                scanner.BottomText = "Wait for the barcode to automatically scan!";

                var opt = new MobileBarcodeScanningOptions();
                opt.AutoRotate = true;
                opt.TryHarder = true;
                opt.TryInverted = true;
                opt.PossibleFormats = new List<ZXing.BarcodeFormat>() { ZXing.BarcodeFormat.EAN_8, ZXing.BarcodeFormat.EAN_13 };
                opt.PureBarcode = true;
                opt.DelayBetweenContinuousScans = 3000;

                //Start scanning
                scanner.ScanContinuously(opt, HandleScanResult);

            };
        }
        void HandleScanResult(ZXing.Result result)
        {
            string msg = "";

            if (result != null && !string.IsNullOrEmpty(result.Text))
                msg = "Found Barcode: " + result.Text;
            else
                msg = "Scanning Canceled!";

            this.RunOnUiThread(() => Toast.MakeText(this, msg, ToastLength.Short).Show());
        }
    }
}

