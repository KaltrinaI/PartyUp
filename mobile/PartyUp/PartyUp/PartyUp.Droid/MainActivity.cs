using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FFImageLoading.Forms.Platform;

namespace PartyUp.Droid
{
    [Activity(Label = "PartyUp", Theme = "@style/MainTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleInstance,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            CachedImageRenderer.Init(true);
            Rg.Plugins.Popup.Popup.Init(this);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this,savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
    }
}