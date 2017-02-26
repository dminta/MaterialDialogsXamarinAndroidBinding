using Android.Annotation;
using Android.App;
using Android.OS;
using Android.Views;

namespace AFollestad.MaterialDialogs.Sample
{
    [TargetApi(Value=(int)BuildVersionCodes.GingerbreadMr1)]
    [Activity(Label = "@string/preference_dialogs")]
    public class PreferenceActivityCompat : Android.Preferences.PreferenceActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AddPreferencesFromResource(Resource.Xml.preferences);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                OnBackPressed();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}