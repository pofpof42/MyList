using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Hardware;


using Orientation = Android.Content.Res.Orientation;
using Configuration = Android.Content.Res.Configuration;

namespace MyList
{
    [Activity(Label = "MyList", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : Activity, TextureView.ISurfaceTextureListener
    {
        Camera _camera;
        AutoFitTextureView _textureView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);


            //  Button translateButton;
          //  _textureView = new TextureView(this);
           // _textureView.SurfaceTextureListener = this;

           // SetContentView(_textureView);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            _textureView = FindViewById<AutoFitTextureView>(Resource.Id.textureView1);
            _textureView.SurfaceTextureListener = this;
            //SetContentView(Resource.Layout.Main);


        }

        public void OnSurfaceTextureAvailable(Android.Graphics.SurfaceTexture surface, int w, int h)
        {
            _camera = Camera.Open();

            //Camera.Parameters parameters = _camera.GetParameters();
            //parameters.Set("orientation", "portrait");
            //_camera.SetParameters(parameters);
            //  _textureView = FindViewById<TextureView>(Resource.Id.textureView1);




            if (Convert.ToInt32(Build.VERSION.Sdk) >= 8)
                _camera.SetDisplayOrientation(90);
            else
            {
                Camera.Parameters parameters = _camera.GetParameters();
                parameters.Set("orientation", "portrait");


                //  if (Configuration. == Orientation.Portrait)
                //  {
                //    parameters.Set("orientation", "portrait");
                parameters.Set("rotation", 90);
                //}
                //if (Configuration.Orientation == Orientation.Landscape)
                //{
                //    parameters.Set("orientation", "landscape");
                //   parameters.Set("rotation", 90);
                //}

                _camera.SetParameters(parameters);
            }
                _textureView.LayoutParameters =
                   new LinearLayout.LayoutParams(w, h);

            try
            {
                _camera.SetPreviewTexture(surface);
                _camera.StartPreview();
                
            }
            catch (Java.IO.IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public bool OnSurfaceTextureDestroyed(Android.Graphics.SurfaceTexture surface)
        {
            _camera.StopPreview();
            _camera.Release();

            return true;
        }

        public void OnSurfaceTextureSizeChanged(Android.Graphics.SurfaceTexture surface, int width, int height)
        {
            // camera takes care of this
        }

        public void OnSurfaceTextureUpdated(Android.Graphics.SurfaceTexture surface)
        {

        }
    }
}

