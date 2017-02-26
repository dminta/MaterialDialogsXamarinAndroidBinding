using AFollestad.MaterialDialogs;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Webkit;
using Java.IO;
using Java.Lang;
using System.IO;

using DialogFragment = Android.Support.V4.App.DialogFragment;

namespace AFollestad.MaterialDialogs.Sample
{
    class ChangelogDialog : DialogFragment
    {
        public static ChangelogDialog Create(bool darkTheme, int accentColor)
        {
            var dialog = new ChangelogDialog();
            var args = new Bundle();
            args.PutBoolean("dark_theme", darkTheme);
            args.PutInt("accent_color", accentColor);
            dialog.Arguments = args;
            return dialog;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            View customView;

            try
            {
                customView = LayoutInflater.From(Activity).Inflate(Resource.Layout.dialog_webview, null);
            }
            catch (InflateException e)
            {
                throw new IllegalStateException("This device does not support Web Views.");
            }

            MaterialDialog dialog = new MaterialDialog.Builder(Activity)
                .Theme(Arguments.GetBoolean("dark_theme") ? AFollestad.MaterialDialogs.Theme.Dark : AFollestad.MaterialDialogs.Theme.Light)
                .Title(Resource.String.changelog)
                .CustomView(customView, false)
                .PositiveText(Android.Resource.String.Ok)
                .Build();

            WebView webView = customView.FindViewById<WebView>(Resource.Id.webview);
            try
            {
                var buf = new StringBuilder();
                Stream json = Activity.Assets.Open("changelog.html");
                BufferedReader input = new BufferedReader(new InputStreamReader(json, "UTF-8"));

                string str;
                while ((str = input.ReadLine()) != null)
                {
                    buf.Append(str);
                }
                input.Close();

                int accentColor = Arguments.GetInt("accent_color");
                webView.LoadData(buf.ToString()
                                .Replace("{style-placeholder}", Arguments.GetBoolean("dark_theme") ?
                                        "body { background-color: #444444; color: #fff; }" :
                                        "body { background-color: #fff; color: #000; }")
                                .Replace("{link-color}", ColorToHex(ShiftColor(accentColor, true)))
                                .Replace("{link-color-active}", ColorToHex(accentColor))
                        , "text/html", "UTF-8");
            }
            catch (Throwable e)
            {
                webView.LoadData("<h1>Unable to load</h1><p>" + e.LocalizedMessage + "</p>", "text/html", "UTF-8");
            }
            return dialog;
        }

        string ColorToHex(int color)
        {
            return Integer.ToHexString(color).Substring(2);
        }

        int ShiftColor(int color, bool up)
        {
            float[] hsv = new float[3];
            Android.Graphics.Color.ColorToHSV(new Android.Graphics.Color(color), hsv);
            hsv[2] *= (up ? 1.1f : 0.9f);
            return Android.Graphics.Color.HSVToColor(hsv);
        }
    }
}