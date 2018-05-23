using System;
using INM;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android;

namespace INM.Droid
{
				[Activity(Label = "INM", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
				public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
				{
								protected override void OnCreate(Bundle bundle)
								{
												TabLayoutResource = Resource.Layout.Tabbar;
												ToolbarResource = Resource.Layout.Toolbar;

												base.OnCreate(bundle);

												global::Xamarin.Forms.Forms.Init(this, bundle);
												LoadApplication(new App());
								}
				}
}

