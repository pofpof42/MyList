using System;
using Android.App;
using Android.Support.V7.App;

using Android.Widget;
using Android.OS;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using AlertDialog = Android.Support.V7.App.AlertDialog;
using Android.Views;

namespace support
{
    [Activity(Label = "support", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Android.Support.V7.App.AppCompatActivity
    {


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
           
            MenuInflater.Inflate(Resource.Menu.toolbar_menu, menu);
            for (int i = 0; i < menu.Size(); i++)
            {
                menu.GetItem(i).SetShowAsAction(ShowAsAction.IfRoom);
            }
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            
            switch (item.ItemId)
            {
                case Resource.Id.menu_attachment:
                    // User chose the "Settings" item, show the app settings UI...
                    Toast.MakeText(this, "Bottom toolbar pressed: ", ToastLength.Short).Show();
                    return true;

                case Resource.Id.menu_cut:
                    // User chose the "Favorite" action, mark the current item
                    Toast.MakeText(this, "Bottom toolbar pressed: ", ToastLength.Short).Show();
                    // as a favorite...
                    return true;

                default:
                    // If we got here, the user's action was not recognized.
                    // Invoke the superclass to handle it.
                    return base.OnOptionsItemSelected(item);

            }
            
         //  this.MenuInflater.Inflate(Resource.Menu.toolbar_menu, item.); ;
           
        }

        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            // Set our view from the "main" layout resource
             SetContentView (Resource.Layout.Main);

           
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            //Toolbar will now take on default Action Bar characteristics
            //  SetActionBar(toolbar);
            if (toolbar != null)
            {

                SetSupportActionBar(toolbar);
                SupportActionBar.Title = "Support";
                SupportActionBar.SetHomeButtonEnabled(true);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
                
                toolbar.InflateMenu(Resource.Menu.toolbar_menu);
                toolbar.MenuItemClick += (sender, e) => {
                    Toast.MakeText(this, "Bottom toolbar pressed: " + e.Item.TitleFormatted, ToastLength.Short).Show();
                    
                };
               
            }


            //You can now use and reference the ActionBar
            //  ActionBar.Title = "Hello from Toolbar";
            /*
            var builder = new AlertDialog.Builder(this);

            builder.SetTitle("Hello Dialog")
                   .SetMessage("Is this material design?")
                   .SetPositiveButton("Yes", delegate { Console.WriteLine("Yes"); })
                   .SetNegativeButton("No", delegate { Console.WriteLine("No"); });

            builder.Create().Show();
            */
        }
    }
}

