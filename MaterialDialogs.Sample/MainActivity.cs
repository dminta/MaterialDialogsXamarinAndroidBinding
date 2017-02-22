using Android.App;
using Android.OS;

namespace MaterialDialogs.Sample
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView (Resource.Layout.activity_main);
        }
    }
}

