﻿using AFollestad.MaterialDialogs;
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
using System.Text;
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
                .OnAny((dialog, which) => ShowToast($"Prompt checked? {dialog.PromptCheckBoxChecked.ToString().ToLower()}"))
                .CheckBoxPromptRes(Resource.String.dont_ask_again, false, null)
                .Show();
        }

        #endregion

        #region Action buttons

        [OnClick(Resource.Id.stacked)]
        public void ShowStacked(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                .Title(Resource.String.useGoogleLocationServices)
                .Content(Html.FromHtml(GetString(Resource.String.useGoogleLocationServicesPrompt)))
                .PositiveText(Resource.String.speedBoost)
                .NegativeText(Resource.String.noThanks)
                .BtnStackedGravity(GravityEnum.End)
                .StackingBehavior(StackingBehavior.Always)
                .Show();
        }

        [OnClick(Resource.Id.neutral)]
        public void ShowNeutral(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                .Title(Resource.String.useGoogleLocationServices)
                .Content(Html.FromHtml(GetString(Resource.String.useGoogleLocationServicesPrompt)))
                .PositiveText(Resource.String.agree)
                .NegativeText(Resource.String.disagree)
                .NeutralText(Resource.String.more_info)
                .Show();
        }

        [OnClick(Resource.Id.callbacks)]
        public void ShowCallbacks(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                .Title(Resource.String.useGoogleLocationServices)
                .Content(Html.FromHtml(GetString(Resource.String.useGoogleLocationServicesPrompt)))
                .PositiveText(Resource.String.agree)
                .NegativeText(Resource.String.disagree)
                .NeutralText(Resource.String.more_info)
                .OnAny((dialog, which) => ShowToast($"{which.Name()}!"))
                .Show();
        }

        #endregion

        #region Basic Lists

        [OnClick(Resource.Id.list)]
        public void ShowList(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                .Title(Resource.String.socialNetworks)
                .Items(Resource.Array.socialNetworks)
                .ItemsCallback((dialog, view, which, text) => ShowToast($"{which}: {text}"))
                .Show();
        }

        [OnClick(Resource.Id.listNoTitle)]
        public void ShowListNoTitle(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                .Items(Resource.Array.socialNetworks)
                .ItemsCallback((dialog, view, which, text) => ShowToast($"{which}: {text}"))
                .Show();
        }

        [OnClick(Resource.Id.longList)]
        public void ShowLongList(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                .Title(Resource.String.states)
                .Items(Resource.Array.states)
                .ItemsCallback((dialog, view, which, text) => ShowToast($"{which}: {text}"))
                .PositiveText(Android.Resource.String.Cancel)
                .Show();
        }

        [OnClick(Resource.Id.list_longItems)]
        public void ShowListLongItems(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                .Title(Resource.String.socialNetworks)
                .Items(Resource.Array.socialNetworks_longItems)
                .ItemsCallback((dialog, view, which, text) => ShowToast($"{which}: {text}"))
                .Show();
        }
        
        [OnClick(Resource.Id.list_checkPrompt)]
        public void ShowListCheckPrompt(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                .Title(Resource.String.socialNetworks)
                .Items(Resource.Array.socialNetworks)
                .ItemsCallback((dialog, view, which, text) => ShowToast($"{which}: {text}"))
                .CheckBoxPromptRes(Resource.String.example_prompt, true, null)
                .NegativeText(Android.Resource.String.Cancel)
                .Show();
        }

        static int index = 0;
        
        [OnClick(Resource.Id.list_longPress)]
        public void ShowListLongPress(object sender, EventArgs e)
        {
            index = 0;
            new MaterialDialog.Builder(this)
                .Title(Resource.String.socialNetworks)
                .Items(Resource.Array.socialNetworks)
                .ItemsCallback((dialog, view, which, text) => ShowToast($"{which}: {text}"))
                .AutoDismiss(false)
                .ItemsLongCallback((dialog, itemView, position, text) => 
                {
                    dialog.GetItems().RemoveAt(position);
                    dialog.NotifyItemsChanged();
                    return false;
                })
                .OnNeutral((dialog, which) =>
                {
                    index++;
                    dialog.GetItems().Add(new Java.Lang.String($"Item {index}"));
                    dialog.NotifyItemInserted(dialog.GetItems().Count - 1);
                })
                .NeutralText(Resource.String.add_item)
                .Show();
        }

        #endregion

        #region Choice Lists

        [OnClick(Resource.Id.singleChoice)]
        public void ShowSingleChoice(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                .Title(Resource.String.socialNetworks)
                .Items(Resource.Array.socialNetworks)
                .ItemsCallbackSingleChoice(2, (dialog, view, which, text) =>
                {
                    ShowToast($"{which}: {text}");
                    return true;
                })
                .PositiveText(Resource.String.md_choose_label)
                .Show();
        }

        [OnClick(Resource.Id.singleChoice_longItems)]
        public void ShowSingleChoiceLongItems(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                .Title(Resource.String.socialNetworks)
                .Items(Resource.Array.socialNetworks_longItems)
                .ItemsCallbackSingleChoice(2, (dialog, view, which, text) =>
                {
                    ShowToast($"{which}: {text}");
                    return true;
                })
                .PositiveText(Resource.String.md_choose_label)
                .Show();
        }

        [OnClick(Resource.Id.multiChoice)]
        public void ShowMultiChoice(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                .Title(Resource.String.socialNetworks)
                .Items(Resource.Array.socialNetworks)
                .ItemsCallbackMultiChoice(new int[] { 1, 3 }, (dialog, which, text) =>
                {
                    StringBuilder str = new StringBuilder();
                    for (int i = 0; i < which.Length; i++)
                    {
                        if (i > 0) str.Append('\n');
                        str.Append(which[i]);
                        str.Append(": ");
                        str.Append(text[i]);
                    }
                    ShowToast(str.ToString());
                    return true;
                })
                .OnNeutral((dialog, which) => dialog.ClearSelectedIndices())
                .OnPositive((dialog, which) => dialog.Dismiss())
                .AlwaysCallMultiChoiceCallback()
                .PositiveText(Resource.String.md_choose_label)
                .AutoDismiss(false)
                .NeutralText(Resource.String.clear_selection)
                .Show();
        }

        [OnClick(Resource.Id.multiChoiceLimited)]
        public void ShowMultiChoiceLimited(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                .Title(Resource.String.socialNetworks)
                .Items(Resource.Array.socialNetworks)
                .ItemsCallbackMultiChoice(new int[] { 1 }, (dialog, which, text) =>
                {
                    bool allowSelectionChange = which.Length <= 2;
                    if (!allowSelectionChange)
                    {
                        ShowToast(Resource.String.selection_limit_reached);
                    }
                    return allowSelectionChange;
                })
                .PositiveText(Resource.String.dismiss)
                .AlwaysCallMultiChoiceCallback()
                .Show();
        }

        [OnClick(Resource.Id.multiChoiceLimitedMin)]
        public void ShowMultiChoiceLimitedMin(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                .Title(Resource.String.socialNetworks)
                .Items(Resource.Array.socialNetworks)
                .ItemsCallbackMultiChoice(new int[] { 1 }, (dialog, which, text) =>
                {
                    bool allowSelectionChange = which.Length >= 1;
                    if (!allowSelectionChange)
                    {
                        ShowToast(Resource.String.selection_min_limit_reached);
                    }
                    return allowSelectionChange;
                })
                .PositiveText(Resource.String.dismiss)
                .AlwaysCallMultiChoiceCallback()
                .Show();
        }

        [OnClick(Resource.Id.multiChoice_longItems)]
        public void ShowMultiChoiceLongItems(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                .Title(Resource.String.socialNetworks)
                .Items(Resource.Array.socialNetworks_longItems)
                .ItemsCallbackMultiChoice(new int[] { 1, 3 }, (dialog, which, text) =>
                {
                    StringBuilder str = new StringBuilder();
                    for (int i = 0; i < which.Length; i++)
                    {
                        if (i > 0) str.Append('\n');
                        str.Append(which[i]);
                        str.Append(": ");
                        str.Append(text[i]);
                    }
                    ShowToast(str.ToString());
                    return true;
                })
                .PositiveText(Resource.String.md_choose_label)
                .Show();
        }

        [OnClick(Resource.Id.multiChoice_disabledItems)]
        public void ShowMultiChoiceDisabledItems(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                .Title(Resource.String.socialNetworks)
                .Items(Resource.Array.socialNetworks)
                .ItemsCallbackMultiChoice(new int[] { 0, 1, 2 }, (dialog, which, text) =>
                {
                    StringBuilder str = new StringBuilder();
                    for (int i = 0; i < which.Length; i++)
                    {
                        if (i > 0) str.Append('\n');
                        str.Append(which[i]);
                        str.Append(": ");
                        str.Append(text[i]);
                    }
                    ShowToast(str.ToString());
                    return true;
                })
                .OnNeutral((dialog, which) => dialog.ClearSelectedIndices())
                .AlwaysCallMultiChoiceCallback()
                .PositiveText(Resource.String.md_choose_label)
                .AutoDismiss(false)
                .NeutralText(Resource.String.clear_selection)
                .ItemsDisabledIndices(0, 1)
                .Show();
        }

        #endregion
    }
}

