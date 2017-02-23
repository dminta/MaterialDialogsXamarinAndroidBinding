using AFollestad.MaterialDialogs;
using AFollestad.MaterialDialogs.Color;
using AFollestad.MaterialDialogs.FolderSelector;
using AFollestad.MaterialDialogs.Util;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using CheeseBind;
using System;
using System.Threading;

namespace MaterialDialogs.Sample
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : AppCompatActivity //,
        //FolderChooserDialog.IFolderCallback,
        //FileChooserDialog.IFileCallback,
        //ColorChooserDialog.IColorCallback
    {
        const int StoragePermissionRC = 69;

        EditText passwordInput;
        View positiveAction;
        int primaryPreselect;

        int accentPreselect;
        Toast toast;
        Thread thread;
        Handler handler;

        int chooserDialog;

        void ShowToast(string message)
        {
            toast?.Cancel();
            toast = Toast.MakeText(this, message, ToastLength.Short);
            toast.Show();
        }

        void StartThread(Action action)
        {
            thread?.Interrupt();
            thread = new Thread(new ThreadStart(action));
            thread.Start();
        }

        void ShowToast(int message)
        {
            ShowToast(GetString(message));
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.activity_main);
            Cheeseknife.Bind(this);

            handler = new Handler();
            primaryPreselect = DialogUtils.ResolveColor(this, Resource.Attribute.colorPrimary);
            accentPreselect = DialogUtils.ResolveColor(this, Resource.Attribute.colorAccent);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            handler = null;
        }

        protected override void OnPause()
        {
            base.OnPause();
            if (thread != null && thread.IsAlive)
            {
                thread.Interrupt();
            }
        }

        #region Basic

        [OnClick(Resource.Id.basicNoTitle)]
        public void ShowBasicNoTitle(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                .Content(Resource.String.shareLocationPrompt)
                .PositiveText(Resource.String.agree)
                .NegativeText(Resource.String.disagree)
                .Show();
        }

        [OnClick(Resource.Id.basic)]
        public void ShowBasic(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                .Title(Resource.String.useGoogleLocationServices)
                .Content(Html.FromHtml(GetString(Resource.String.useGoogleLocationServicesPrompt)))
                .PositiveText(Resource.String.agree)
                .NegativeText(Resource.String.disagree)
                .Show();
        }

        [OnClick(Resource.Id.basicLongContent)]
        public void ShowBasicLongContent(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                .Title(Resource.String.useGoogleLocationServices)
                .Content(Resource.String.loremIpsum)
                .PositiveText(Resource.String.agree)
                .NegativeText(Resource.String.disagree)
                .Show();
        }

        [OnClick(Resource.Id.basicIcon)]
        public void ShowBasicIcon(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                .IconRes(Resource.Mipmap.ic_launcher)
                .LimitIconToDefaultSize()
                .Title(Resource.String.useGoogleLocationServices)
                .Content(Html.FromHtml(GetString(Resource.String.useGoogleLocationServicesPrompt)))
                .PositiveText(Resource.String.agree)
                .NegativeText(Resource.String.disagree)
                .Show();
        }

        [OnClick(Resource.Id.basicCheckPrompt)]
        public void ShowBasicCheckPrompt(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                .IconRes(Resource.Mipmap.ic_launcher)
                .LimitIconToDefaultSize()
                .Title(Html.FromHtml(GetString(Resource.String.permissionSample, GetString(Resource.String.app_name))))
                .PositiveText(Resource.String.allow)
                .NegativeText(Resource.String.deny)
                .OnAny((dialog, which) => ShowToast($"Prompt checked? {dialog.PromptCheckBoxChecked}"))
                .CheckBoxPromptRes(Resource.String.dont_ask_again, false, null)
                .Show();
        }

        #endregion
    }
}

