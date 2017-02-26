using AFollestad.MaterialDialogs;
using Android.App;
using Android.OS;
using Android.Support.V7.App;

using DialogFragment = Android.Support.V4.App.DialogFragment;

namespace AFollestad.MaterialDialogs.Sample
{
    public class AboutDialog : DialogFragment
    {
        public static void Show(AppCompatActivity context)
        {
            var dialog = new AboutDialog();
            dialog.Show(context.SupportFragmentManager, "[ABOUT_DIALOG]");
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            return new MaterialDialog.Builder(Activity)
                .Title(Resource.String.about)
                .PositiveText(Resource.String.dismiss)
                .Content(Resource.String.about_body, true)
                .ContentLineSpacing(1.6f)
                .Build();
        }
    }
}