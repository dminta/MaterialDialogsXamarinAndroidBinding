using AFollestad.MaterialDialogs;
using AFollestad.MaterialDialogs.Color;
using AFollestad.MaterialDialogs.FolderSelector;
using AFollestad.MaterialDialogs.Internal;
using AFollestad.MaterialDialogs.SimpleList;
using AFollestad.MaterialDialogs.Util;
using Android;
using Android.Annotation;
using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Text;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using CheeseBind;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using Java.IO;
using System.Threading.Tasks;

namespace MaterialDialogs.Sample
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : AppCompatActivity,
        FolderChooserDialog.IFolderCallback,
        FileChooserDialog.IFileCallback,
        ColorChooserDialog.IColorCallback
    {
        const int StoragePermissionRC = 69;

        EditText _passwordInput;
        View _positiveAction;
        int _primaryPreselect;

        int _accentPreselect;
        Toast _toast;
        Task _task;
        CancellationTokenSource _cancellationTokenSrc;
        Handler _handler;

        int _chooserDialog;

        void ShowToast(string message)
        {
            _toast?.Cancel();
            _toast = Toast.MakeText(this, message, ToastLength.Short);
            _toast.Show();
        }

        void StartThread(Action action)
        {
            _cancellationTokenSrc?.Cancel();
            _cancellationTokenSrc = new CancellationTokenSource();
            _task = Task.Factory.StartNew(action, _cancellationTokenSrc.Token);
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

            _handler = new Handler();
            _primaryPreselect = DialogUtils.ResolveColor(this, Resource.Attribute.colorPrimary);
            _accentPreselect = DialogUtils.ResolveColor(this, Resource.Attribute.colorAccent);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _handler = null;
        }

        protected override void OnPause()
        {
            base.OnPause();
            _cancellationTokenSrc?.Cancel();
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

        #region Advanced Lists

        [OnClick(Resource.Id.simpleList)]
        public void ShowSimpleList(object sender, EventArgs e)
        {
            MaterialSimpleListAdapter adapter = new MaterialSimpleListAdapter((dialog, index1, item) => ShowToast(item.Content));
            adapter.Add(new MaterialSimpleListItem.Builder(this)
                    .Content("username@gmail.com")
                    .Icon(Resource.Drawable.ic_account_circle)
                    .BackgroundColor(Color.White)
                    .Build());
            adapter.Add(new MaterialSimpleListItem.Builder(this)
                    .Content("user02@gmail.com")
                    .Icon(Resource.Drawable.ic_account_circle)
                    .BackgroundColor(Color.White)
                    .Build());
            adapter.Add(new MaterialSimpleListItem.Builder(this)
                    .Content(Resource.String.add_account)
                    .Icon(Resource.Drawable.ic_content_add)
                    .IconPaddingDp(8)
                    .Build());

            new MaterialDialog.Builder(this)
                    .Title(Resource.String.set_backup)
                    .Adapter(adapter, null)
                    .Show();
        }

        [OnClick(Resource.Id.customListItems)]
        public void ShowCustomList(object sender, EventArgs e)
        {
            ButtonItemAdapter adapter = new ButtonItemAdapter(this, Resource.Array.socialNetworks);
            adapter.SetCallbacks(
                    itemIndex => ShowToast("Item clicked: " + itemIndex),
                    buttonIndex => ShowToast("Button clicked: " + buttonIndex));
            new MaterialDialog.Builder(this)
                    .Title(Resource.String.socialNetworks)
                    .Adapter(adapter, null)
                    .Show();
        }

        #endregion

        #region Custom Views

        [OnClick(Resource.Id.customView)]
        public void ShowCustomView(object sender, EventArgs e)
        {
            MaterialDialog dialog = new MaterialDialog.Builder(this)
                    .Title(Resource.String.googleWifi)
                    .CustomView(Resource.Layout.dialog_customview, true)
                    .PositiveText(Resource.String.connect)
                    .NegativeText(Android.Resource.String.Cancel)
                    .OnPositive((dialog1, which) => ShowToast("Password: " + _passwordInput.Text)).Build();

            _positiveAction = dialog.GetActionButton(DialogAction.Positive);

            _passwordInput = dialog.CustomView.FindViewById<EditText>(Resource.Id.password);
            _passwordInput.TextChanged += (s, ev) =>
            {
                _positiveAction.Enabled = String.Concat(ev.Text.Select(c => c.ToString())).Trim().Length > 0;
            };


            CheckBox checkbox = dialog.CustomView.FindViewById<CheckBox>(Resource.Id.showPassword);
            checkbox.CheckedChange += (s, ev) =>
            {
                _passwordInput.InputType = (!ev.IsChecked) ? InputTypes.TextVariationPassword : InputTypes.ClassText;
                _passwordInput.TransformationMethod = (!ev.IsChecked) ? PasswordTransformationMethod.Instance : null;
            };

            int widgetColor = ThemeSingleton.Get().WidgetColor;
            MDTintHelper.SetTint(checkbox, 
                widgetColor == 0 ? ContextCompat.GetColor(this, Resource.Color.accent) : widgetColor);

            MDTintHelper.SetTint(_passwordInput,
                widgetColor == 0 ? ContextCompat.GetColor(this, Resource.Color.accent) : widgetColor);

            dialog.Show();
            _positiveAction.Enabled = false;
        }

        [OnClick(Resource.Id.customView_webView)]
        public void ShowCustomWebView(object sender, EventArgs e)
        {
            int accentColor = ThemeSingleton.Get().WidgetColor;
            if (accentColor == 0)
                accentColor = ContextCompat.GetColor(this, Resource.Color.accent);
            ChangelogDialog.Create(false, accentColor)
                    .Show(SupportFragmentManager, "changelog");
        }

        [OnClick(Resource.Id.customView_datePicker)]
        public void ShowCustomDatePicker(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                    .Title(Resource.String.date_picker)
                    .CustomView(Resource.Layout.dialog_datepicker, false)
                    .PositiveText(Android.Resource.String.Ok)
                    .NegativeText(Android.Resource.String.Cancel)
                    .Show();
        }

        #endregion

        #region Color Choosers

        [OnClick(Resource.Id.colorChooser_primary)]
        public void ShowColorChooserPrimary(object sender, EventArgs e)
        {
            new ColorChooserDialog.Builder(this, Resource.String.color_palette)
                    .TitleSub(Resource.String.colors)
                    .Preselect(_primaryPreselect)
                    .Show();
        }

        [OnClick(Resource.Id.colorChooser_accent)]
        public void ShowColorChooserAccent(object sender, EventArgs e)
        {
            new ColorChooserDialog.Builder(this, Resource.String.color_palette)
                    .TitleSub(Resource.String.colors)
                    .AccentMode(true)
                    .Preselect(_accentPreselect)
                    .Show();
        }

        [OnClick(Resource.Id.colorChooser_customColors)]
        public void ShowColorChooserCustomColors(object sender, EventArgs e)
        {
            int[][] subColors = new int[][]
            {
                new int[] 
                {
                    Color.ParseColor("#EF5350").ToArgb(),
                    Color.ParseColor("#F44336").ToArgb(),
                    Color.ParseColor("#E53935").ToArgb()
                },
                new int[]
                {
                    Color.ParseColor("#EC407A").ToArgb(),
                    Color.ParseColor("#E91E63").ToArgb(),
                    Color.ParseColor("#D81B60").ToArgb()
                },
                new int[]
                {
                    Color.ParseColor("#AB47BC").ToArgb(),
                    Color.ParseColor("#9C27B0").ToArgb(),
                    Color.ParseColor("#8E24AA").ToArgb()
                },
                new int[]
                {
                    Color.ParseColor("#7E57C2").ToArgb(),
                    Color.ParseColor("#673AB7").ToArgb(),
                    Color.ParseColor("#5E35B1").ToArgb()
                },
                new int[]
                {
                    Color.ParseColor("#5C6BC0").ToArgb(),
                    Color.ParseColor("#3F51B5").ToArgb(),
                    Color.ParseColor("#3949AB").ToArgb()
                },
                new int[]
                {
                    Color.ParseColor("#42A5F5").ToArgb(),
                    Color.ParseColor("#2196F3").ToArgb(),
                    Color.ParseColor("#1E88E5").ToArgb()
                }
            };

            new ColorChooserDialog.Builder(this, Resource.String.color_palette)
                    .TitleSub(Resource.String.colors)
                    .Preselect(_primaryPreselect)
                    .CustomColors(Resource.Array.custom_colors, subColors)
                    .Show();
        }

        [OnClick(Resource.Id.colorChooser_customColorsNoSub)]
        public void ShowColorChooserCustomColorsNoSub(object sender, EventArgs e)
        {
            new ColorChooserDialog.Builder(this, Resource.String.color_palette)
                    .TitleSub(Resource.String.colors)
                    .Preselect(_primaryPreselect)
                    .CustomColors(Resource.Array.custom_colors, null)
                    .Show();
        }

        #region ColorChooserDialog.IColorCallback implementation

        public void OnColorSelection(ColorChooserDialog dialog, int color)
        {
            if (dialog.IsAccentMode)
            {
                _accentPreselect = color;
                ThemeSingleton.Get().PositiveColor = DialogUtils.GetActionTextStateList(this, color);
                ThemeSingleton.Get().NeutralColor = DialogUtils.GetActionTextStateList(this, color);
                ThemeSingleton.Get().NegativeColor = DialogUtils.GetActionTextStateList(this, color);
                ThemeSingleton.Get().WidgetColor = color;
            }
            else
            {
                _primaryPreselect = color;
                SupportActionBar?.SetBackgroundDrawable(new ColorDrawable(new Color(color)));
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    Window.SetStatusBarColor(new Color(CircleView.ShiftColorDown(color)));
                    Window.SetNavigationBarColor(new Color(color));
                }
            }
        }

        public void OnColorChooserDismissed(ColorChooserDialog dialog)
        {
            ShowToast("Color chooser dismissed!");
        }

        #endregion

        #endregion

        #region Miscellaneous

        [OnClick(Resource.Id.themed)]
        public void ShowThemed(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                    .Title(Resource.String.useGoogleLocationServices)
                    .Content(Html.FromHtml(GetString(Resource.String.useGoogleLocationServicesPrompt)))
                    .PositiveText(Resource.String.agree)
                    .NegativeText(Resource.String.disagree)
                    .PositiveColorRes(Resource.Color.material_red_400)
                    .NegativeColorRes(Resource.Color.material_red_400)
                    .TitleGravity(GravityEnum.Center)
                    .TitleColorRes(Resource.Color.material_red_400)
                    .ContentColorRes(Android.Resource.Color.White)
                    .BackgroundColorRes(Resource.Color.material_blue_grey_800)
                    .DividerColorRes(Resource.Color.accent)
                    .BtnSelector(Resource.Drawable.md_btn_selector_custom, DialogAction.Positive)
                    .PositiveColor(Color.White)
                    .NegativeColorAttr(Android.Resource.Attribute.TextColorSecondaryInverse)
                    .Theme(AFollestad.MaterialDialogs.Theme.Dark)
                    .Show();
        }

        [OnClick(Resource.Id.showCancelDismiss)]
        public void ShowShowCancelDismissCallbacks(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                    .Title(Resource.String.useGoogleLocationServices)
                    .Content(Html.FromHtml(GetString(Resource.String.useGoogleLocationServicesPrompt)))
                    .PositiveText(Resource.String.agree)
                    .NegativeText(Resource.String.disagree)
                    .NeutralText(Resource.String.more_info)
                    .ShowListener(dialog => ShowToast("onShow"))
                    .CancelListener(dialog => ShowToast("onCancel"))
                    .DismissListener(dialog => ShowToast("onDismiss"))
                    .Show();
        }

        [TargetApi(Value=(int)BuildVersionCodes.JellyBean)]
        [OnClick(Resource.Id.file_chooser)]
        public void ShowFileChooser(object sender, EventArgs e)
        {
            _chooserDialog = Resource.Id.file_chooser;
            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage)
                != Android.Content.PM.Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadExternalStorage }, StoragePermissionRC);
                return;
            }
            new FileChooserDialog.Builder(this)
                    .Show();
        }

        #region FileChooserDialog.IFileCallback implementation

        public void OnFileSelection(FileChooserDialog dialog, File file)
        {
            ShowToast(file.AbsolutePath);
        }

        public void OnFileChooserDismissed(FileChooserDialog dialog)
        {
            ShowToast("File chooser dismissed!");
        }

        #endregion

        [TargetApi(Value = (int)BuildVersionCodes.JellyBean)]
        [OnClick(Resource.Id.folder_chooser)]
        public void ShowFolderChooser(object sender, EventArgs e)
        {
            _chooserDialog = Resource.Id.folder_chooser;
            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage)
                != Android.Content.PM.Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.WriteExternalStorage }, StoragePermissionRC);
                return;
            }
            new FolderChooserDialog.Builder(this)
                    .ChooseButton(Resource.String.md_choose_label)
                    .AllowNewFolder(true, 0)
                    .Show();
        }

        #region FolderChooserDialog.IFolderCallback implementation

        public void OnFolderSelection(FolderChooserDialog dialog, File folder)
        {
            ShowToast(folder.AbsolutePath);
        }

        public void OnFolderChooserDismissed(FolderChooserDialog dialog)
        {
            ShowToast("Folder chooser dismissed!");
        }

        #endregion

        #endregion

        #region Input

        [OnClick(Resource.Id.input)]
        public void ShowInputDialog(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                    .Title(Resource.String.input)
                    .Content(Resource.String.input_content)
                    .InputType(InputTypes.ClassText |
                            InputTypes.TextVariationPersonName |
                            InputTypes.TextFlagCapWords)
                    .InputRange(2, 16)
                    .PositiveText(Resource.String.submit)
                    .Input(Resource.String.input_hint, Resource.String.input_hint, false, (dialog, input) =>
                            ShowToast("Hello, " + input + "!")).Show();
        }

        [OnClick(Resource.Id.input_custominvalidation)]
        public void ShowInputDialogCustomInvalidation(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                    .Title(Resource.String.input)
                    .Content(Resource.String.input_content_custominvalidation)
                    .InputType(InputTypes.ClassText |
                            InputTypes.TextVariationPersonName |
                            InputTypes.TextFlagCapWords)
                    .PositiveText(Resource.String.submit)
                    .AlwaysCallInputCallback()
                    .Input(Resource.String.input_hint, 0, false, (dialog, input) =>
                    {
                        if (input.Equals("hello", StringComparison.CurrentCultureIgnoreCase))
                        {
                            dialog.SetContent("I told you not to type that!");
                            dialog.GetActionButton(DialogAction.Positive).Enabled = false;
                        }
                        else
                        {
                            dialog.SetContent(Resource.String.input_content_custominvalidation);
                            dialog.GetActionButton(DialogAction.Positive).Enabled = true;
                        }
                    }).Show();
        }

        [OnClick(Resource.Id.input_checkPrompt)]
        public void ShowInputDialogCheckPrompt(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                    .Title(Resource.String.input)
                    .Content(Resource.String.input_content)
                    .InputType(InputTypes.ClassText |
                            InputTypes.TextVariationPersonName |
                            InputTypes.TextFlagCapWords)
                    .InputRange(2, 16)
                    .PositiveText(Resource.String.submit)
                    .Input(Resource.String.input_hint, Resource.String.input_hint, false, (dialog, input) =>
                            ShowToast("Hello, " + input + "!"))
                    .CheckBoxPromptRes(Resource.String.example_prompt, true, null)
                    .Show();
        }

        #endregion

        #region Progress

        [OnClick(Resource.Id.progress1)]
        public void ShowProgressDeterminateDialog(object sender, EventArgs e)
        {
            new MaterialDialog.Builder(this)
                    .Title(Resource.String.progress_dialog)
                    .Content(Resource.String.please_wait)
                    .ContentGravity(GravityEnum.Center)
                    .Progress(false, 150, true)
                    .CancelListener(dialog => 
                    {
                        _cancellationTokenSrc?.Cancel();
                    })
                    .ShowListener(dialogInterface => 
                    {
                        MaterialDialog dialog = (MaterialDialog)dialogInterface;
                        StartThread(async () => 
                        {
                            while (dialog.CurrentProgress != dialog.MaxProgress)
                            {
                                if (dialog.IsCancelled || (_cancellationTokenSrc?.IsCancellationRequested ?? false))
                                {
                                    break;
                                }

                                await Task.Delay(50);

                                dialog.IncrementProgress(1);
                            }
                            RunOnUiThread(() => 
                            {
                                dialog.SetContent(GetString(Resource.String.md_done_label));
                            });
                        });
                    }).Show();
        }

        [OnClick(Resource.Id.progress2)]
        public void ShowProgressIndeterminateDialog(object sender, EventArgs e)
        {
            ShowIndeterminateProgressDialog(false);
        }

        [OnClick(Resource.Id.progress3)]
        public void ShowProgressHorizontalIndeterminateDialog(object sender, EventArgs e)
        {
            ShowIndeterminateProgressDialog(true);
        }

        void ShowIndeterminateProgressDialog(bool horizontal)
        {
            new MaterialDialog.Builder(this)
                    .Title(Resource.String.progress_dialog)
                    .Content(Resource.String.please_wait)
                    .Progress(true, 0)
                    .ProgressIndeterminateStyle(horizontal)
                    .Show();
        }

        #endregion
    }
}
