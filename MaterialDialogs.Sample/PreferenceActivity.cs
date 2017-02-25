using Android.App;
using Android.OS;
using Android.Preferences;
using Android.Support.V7.App;
using Android.Views;

namespace MaterialDialogs.Sample
{
    [Activity(Label="@string/preference_dialogs")]
    public class PreferenceActivity : AppCompatActivity
    {
        public class SettingsFragment : PreferenceFragment
        {
            public override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);
                AddPreferencesFromResource(Resource.Xml.preferences);
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.preference_activity_custom);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            if (FragmentManager.FindFragmentById(Resource.Id.content_frame) == null)
            {
                FragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new SettingsFragment()).Commit();
            }
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