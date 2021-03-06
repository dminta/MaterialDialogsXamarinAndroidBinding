using Android.Runtime;
using System;
using Android.Views;
using Java.Lang;
using System.Linq;
using Android.Content;
using Android.Text;

namespace AFollestad.MaterialDialogs
{
    public partial class MaterialDialog
    {
        public partial class Builder
        {
            class DialogInterfaceOnCancelListenerActionWrapper : Java.Lang.Object, IDialogInterfaceOnCancelListener
            {
                Action<IDialogInterface> _action;

                public DialogInterfaceOnCancelListenerActionWrapper(Action<IDialogInterface> action)
                {
                    _action = action;
                }

                public void OnCancel(IDialogInterface dialog)
                {
                    _action?.Invoke(dialog);
                }
            }

            class DialogInterfaceOnDismissListenerActionWrapper : Java.Lang.Object, IDialogInterfaceOnDismissListener
            {
                Action<IDialogInterface> _action;

                public DialogInterfaceOnDismissListenerActionWrapper(Action<IDialogInterface> action)
                {
                    _action = action;
                }

                public void OnDismiss(IDialogInterface dialog)
                {
                    _action?.Invoke(dialog);
                }
            }

            class DialogInterfaceOnShowListenerActionWrapper : Java.Lang.Object, IDialogInterfaceOnShowListener
            {
                Action<IDialogInterface> _action;

                public DialogInterfaceOnShowListenerActionWrapper(Action<IDialogInterface> action)
                {
                    _action = action;
                }

                public void OnShow(IDialogInterface dialog)
                {
                    _action?.Invoke(dialog);
                }
            }

            class InputCallbackActionWrapper : Java.Lang.Object, IInputCallback
            {
                Action<MaterialDialog, string> _action;

                public InputCallbackActionWrapper(Action<MaterialDialog, string> action)
                {
                    _action = action;
                }

                public void OnInput(MaterialDialog dialog, ICharSequence text)
                {
                    _action?.Invoke(dialog, text.ToString());
                }
            }

            class ListCallbackActionWrapper : Java.Lang.Object, IListCallback
            {
                Action<MaterialDialog, View, int, string> _action;

                public ListCallbackActionWrapper(Action<MaterialDialog, View, int, string> action)
                {
                    _action = action;
                }

                public void OnSelection(MaterialDialog dialog, View view, int which, ICharSequence text)
                {
                    _action?.Invoke(dialog, view, which, text?.ToString());
                }
            }

            class ListCallbackSingleChoiceFuncWrapper : Java.Lang.Object, IListCallbackSingleChoice
            {
                Func<MaterialDialog, View, int, string, bool> _func;

                public ListCallbackSingleChoiceFuncWrapper(Func<MaterialDialog, View, int, string, bool> func)
                {
                    _func = func;
                }

                public bool OnSelection(MaterialDialog dialog, View view, int which, ICharSequence text)
                {
                    return _func?.Invoke(dialog, view, which, text?.ToString()) ?? false;
                }
            }

            class ListCallbackMultiChoiceFuncWrapper : Java.Lang.Object, IListCallbackMultiChoice
            {
                Func<MaterialDialog, int[], string[], bool> _func;

                public ListCallbackMultiChoiceFuncWrapper(Func<MaterialDialog, int[], string[], bool> func)
                {
                    _func = func;
                }

                public bool OnSelection(MaterialDialog dialog, Integer[] which, ICharSequence[] text)
                {
                    var whichArray = which.Select(w => w.IntValue()).ToArray();
                    var textArray = text.Select(t => t.ToString()).ToArray();
                    return _func?.Invoke(dialog, whichArray, textArray) ?? false;
                }
            }

            class ListLongCallbackFuncWrapper : Java.Lang.Object, IListLongCallback
            {
                Func<MaterialDialog, View, int, string, bool> _func;

                public ListLongCallbackFuncWrapper(Func<MaterialDialog, View, int, string, bool> func)
                {
                    _func = func;
                }

                public bool OnLongSelection(MaterialDialog dialog, View view, int which, ICharSequence text)
                {
                    return _func?.Invoke(dialog, view, which, text?.ToString()) ?? false;
                }
            }

            class SingleButtonCallbackActionWrapper : Java.Lang.Object, ISingleButtonCallback
            {
                Action<MaterialDialog, DialogAction> _action;

                public SingleButtonCallbackActionWrapper(Action<MaterialDialog, DialogAction> action)
                {
                    _action = action;
                }

                public void OnClick(MaterialDialog dialog, DialogAction which)
                {
                    _action?.Invoke(dialog, which);
                }
            }

            public Builder OnAny(Action<MaterialDialog, DialogAction> action)
            {
                return OnAny(new SingleButtonCallbackActionWrapper(action));
            }

            public Builder OnNeutral(Action<MaterialDialog, DialogAction> action)
            {
                return OnNeutral(new SingleButtonCallbackActionWrapper(action));
            }

            public Builder OnPositive(Action<MaterialDialog, DialogAction> action)
            {
                return OnPositive(new SingleButtonCallbackActionWrapper(action));
            }

            public Builder ItemsCallback(Action<MaterialDialog, View, int, string> action)
            {
                return ItemsCallback(new ListCallbackActionWrapper(action));
            }

            public Builder ItemsLongCallback(Func<MaterialDialog, View, int, string, bool> func)
            {
                return ItemsLongCallback(new ListLongCallbackFuncWrapper(func));
            }

            public Builder ItemsCallbackSingleChoice(int selectedIndex, Func<MaterialDialog, View, int, string, bool> func)
            {
                return ItemsCallbackSingleChoice(selectedIndex, new ListCallbackSingleChoiceFuncWrapper(func));
            }

            public Builder ItemsCallbackMultiChoice(int[] selectedIndices, Func<MaterialDialog, int[], string[], bool> func)
            {
                var indices = selectedIndices.Select(index => new Integer(index)).ToArray();
                return ItemsCallbackMultiChoice(indices, new ListCallbackMultiChoiceFuncWrapper(func));
            }

            public Builder ItemsDisabledIndices(params int[] indices)
            {
                return ItemsDisabledIndices(indices.Select(i => new Integer(i)).ToArray());
            }

            public Builder ShowListener(Action<IDialogInterface> action)
            {
                return ShowListener(new DialogInterfaceOnShowListenerActionWrapper(action));
            }

            public Builder CancelListener(Action<IDialogInterface> action)
            {
                return CancelListener(new DialogInterfaceOnCancelListenerActionWrapper(action));
            }

            public Builder DismissListener(Action<IDialogInterface> action)
            {
                return DismissListener(new DialogInterfaceOnDismissListenerActionWrapper(action));
            }

            public Builder Input(int hint, int prefill, bool allowEmptyInput, Action<MaterialDialog, string> action)
            {
                return Input(hint, prefill, allowEmptyInput, new InputCallbackActionWrapper(action));
            }

            public Builder InputType(InputTypes inputTypes)
            {
                return InputType((int)inputTypes);
            }
        }

        static IntPtr id_items_arrayLjava_lang_CharSequence_;
        // Metadata.xml XPath method reference: path="/api/package[@name='com.afollestad.materialdialogs']/class[@name='MaterialDialog.Builder']/method[@name='items' and count(parameter)=1 and parameter[1][@type='java.lang.CharSequence...']]"
        [Register("items", "([Ljava/lang/CharSequence;)Lcom/afollestad/materialdialogs/MaterialDialog$Builder;", "GetItems_arrayLjava_lang_CharSequence_Handler")]
        public virtual unsafe global::AFollestad.MaterialDialogs.MaterialDialog.Builder Items(params global::Java.Lang.ICharSequence[] p0)
        {
            if (id_items_arrayLjava_lang_CharSequence_ == IntPtr.Zero)
                id_items_arrayLjava_lang_CharSequence_ = JNIEnv.GetMethodID(class_ref, "items", "([Ljava/lang/CharSequence;)Lcom/afollestad/materialdialogs/MaterialDialog$Builder;");
            IntPtr native_p0 = JNIEnv.NewArray(p0);
            try
            {
                JValue* __args = stackalloc JValue[1];
                __args[0] = new JValue(native_p0);

                global::AFollestad.MaterialDialogs.MaterialDialog.Builder __ret;
                if (GetType() == ThresholdType)
                    __ret = global::Java.Lang.Object.GetObject<global::AFollestad.MaterialDialogs.MaterialDialog.Builder>(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_items_arrayLjava_lang_CharSequence_, __args), JniHandleOwnership.TransferLocalRef);
                else
                    __ret = global::Java.Lang.Object.GetObject<global::AFollestad.MaterialDialogs.MaterialDialog.Builder>(JNIEnv.CallNonvirtualObjectMethod(((global::Java.Lang.Object)this).Handle, ThresholdClass, JNIEnv.GetMethodID(ThresholdClass, "items", "([Ljava/lang/CharSequence;)Lcom/afollestad/materialdialogs/MaterialDialog$Builder;"), __args), JniHandleOwnership.TransferLocalRef);
                return __ret;
            }
            finally
            {
                if (p0 != null)
                {
                    JNIEnv.CopyArray(native_p0, p0);
                    JNIEnv.DeleteLocalRef(native_p0);
                }
            }
        }

        public global::AFollestad.MaterialDialogs.MaterialDialog.Builder Items(params string[] p0)
        {
            global::Java.Lang.ICharSequence[] jlca_p0 = CharSequence.ArrayFromStringArray(p0);
            global::AFollestad.MaterialDialogs.MaterialDialog.Builder __result = Items(jlca_p0);
            return __result;
        }
        
        static IntPtr id_setItems_arrayLjava_lang_CharSequence_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.afollestad.materialdialogs']/class[@name='MaterialDialog']/method[@name='setItems' and count(parameter)=1 and parameter[1][@type='java.lang.CharSequence...']]"
		[Register ("setItems", "([Ljava/lang/CharSequence;)V", "")]
		public unsafe void SetItems (params global:: Java.Lang.ICharSequence[] p0)
		{
			if (id_setItems_arrayLjava_lang_CharSequence_ == IntPtr.Zero)
				id_setItems_arrayLjava_lang_CharSequence_ = JNIEnv.GetMethodID (class_ref, "setItems", "([Ljava/lang/CharSequence;)V");
			IntPtr native_p0 = JNIEnv.NewArray (p0);
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (native_p0);
				JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_setItems_arrayLjava_lang_CharSequence_, __args);
			} finally {
				if (p0 != null) {
					JNIEnv.CopyArray (native_p0, p0);
					JNIEnv.DeleteLocalRef (native_p0);
				}
			}
		}

		public void SetItems (params string[] p0)
		{
			global::Java.Lang.ICharSequence[] jlca_p0 = CharSequence.ArrayFromStringArray(p0);
			SetItems (jlca_p0);
		}
        
        static IntPtr id_getItems;
        // Metadata.xml XPath method reference: path="/api/package[@name='com.afollestad.materialdialogs']/class[@name='MaterialDialog']/method[@name='getItems' and count(parameter)=0]"
        [Register("getItems", "()Ljava/util/ArrayList;", "GetGetItemsHandler")]
        public unsafe global::System.Collections.Generic.IList<global::Java.Lang.ICharSequence> GetItems()
        {
            if (id_getItems == IntPtr.Zero)
                id_getItems = JNIEnv.GetMethodID(class_ref, "getItems", "()Ljava/util/ArrayList;");
            try
            {
                return global::Android.Runtime.JavaList<global::Java.Lang.ICharSequence>.FromJniHandle(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_getItems), JniHandleOwnership.TransferLocalRef);
            }
            finally
            {
            }
        }
    }
}