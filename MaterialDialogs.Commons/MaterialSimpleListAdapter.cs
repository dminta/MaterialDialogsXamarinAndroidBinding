using Android.Runtime;
using Android.Support.V7.Widget;
using System;

namespace AFollestad.MaterialDialogs.SimpleList
{
    class CallbackActionWrapper : Java.Lang.Object, MaterialSimpleListAdapter.ICallback
    {
        Action<MaterialDialog, int, MaterialSimpleListItem> _action;

        public CallbackActionWrapper(Action<MaterialDialog, int, MaterialSimpleListItem> action)
        {
            _action = action;
        }

        public void OnMaterialListItemSelected(MaterialDialog dialog, int index1, MaterialSimpleListItem item)
        {
            _action?.Invoke(dialog, index1, item);
        }
    }

    public partial class MaterialSimpleListAdapter : global::Android.Support.V7.Widget.RecyclerView.Adapter, global::AFollestad.MaterialDialogs.Internal.IMDAdapter
    {
        public MaterialSimpleListAdapter(Action<MaterialDialog, int, MaterialSimpleListItem> action)
            : this(new CallbackActionWrapper(action))
        {
        }

        static IntPtr id_onBindViewHolder_Lcom_afollestad_materialdialogs_simplelist_MaterialSimpleListAdapter_SimpleListVH_I;
        // Metadata.xml XPath method reference: path="/api/package[@name='com.afollestad.materialdialogs.simplelist']/class[@name='MaterialSimpleListAdapter']/method[@name='onBindViewHolder' and count(parameter)=2 and parameter[1][@type='com.afollestad.materialdialogs.simplelist.MaterialSimpleListAdapter.SimpleListVH'] and parameter[2][@type='int']]"
        [Register("onBindViewHolder", "(Lcom/afollestad/materialdialogs/simplelist/MaterialSimpleListAdapter$SimpleListVH;I)V", "GetOnBindViewHolder_Lcom_afollestad_materialdialogs_simplelist_MaterialSimpleListAdapter_SimpleListVH_IHandler")]
        public override unsafe void OnBindViewHolder(RecyclerView.ViewHolder p0, int p1)
        {
            if (id_onBindViewHolder_Lcom_afollestad_materialdialogs_simplelist_MaterialSimpleListAdapter_SimpleListVH_I == IntPtr.Zero)
                id_onBindViewHolder_Lcom_afollestad_materialdialogs_simplelist_MaterialSimpleListAdapter_SimpleListVH_I = JNIEnv.GetMethodID(class_ref, "onBindViewHolder", "(Lcom/afollestad/materialdialogs/simplelist/MaterialSimpleListAdapter$SimpleListVH;I)V");
            try
            {
                JValue* __args = stackalloc JValue[2];
                __args[0] = new JValue(p0);
                __args[1] = new JValue(p1);

                if (GetType() == ThresholdType)
                    JNIEnv.CallVoidMethod(((global::Java.Lang.Object)this).Handle, id_onBindViewHolder_Lcom_afollestad_materialdialogs_simplelist_MaterialSimpleListAdapter_SimpleListVH_I, __args);
                else
                    JNIEnv.CallNonvirtualVoidMethod(((global::Java.Lang.Object)this).Handle, ThresholdClass, JNIEnv.GetMethodID(ThresholdClass, "onBindViewHolder", "(Lcom/afollestad/materialdialogs/simplelist/MaterialSimpleListAdapter$SimpleListVH;I)V"), __args);
            }
            finally
            {
            }
        }

        static IntPtr id_onCreateViewHolder_Landroid_view_ViewGroup_I;
        // Metadata.xml XPath method reference: path="/api/package[@name='com.afollestad.materialdialogs.simplelist']/class[@name='MaterialSimpleListAdapter']/method[@name='onCreateViewHolder' and count(parameter)=2 and parameter[1][@type='android.view.ViewGroup'] and parameter[2][@type='int']]"
        [Register("onCreateViewHolder", "(Landroid/view/ViewGroup;I)Lcom/afollestad/materialdialogs/simplelist/MaterialSimpleListAdapter$SimpleListVH;", "GetOnCreateViewHolder_Landroid_view_ViewGroup_IHandler")]
        public override unsafe RecyclerView.ViewHolder OnCreateViewHolder(global::Android.Views.ViewGroup p0, int p1)
        {
            if (id_onCreateViewHolder_Landroid_view_ViewGroup_I == IntPtr.Zero)
                id_onCreateViewHolder_Landroid_view_ViewGroup_I = JNIEnv.GetMethodID(class_ref, "onCreateViewHolder", "(Landroid/view/ViewGroup;I)Lcom/afollestad/materialdialogs/simplelist/MaterialSimpleListAdapter$SimpleListVH;");
            try
            {
                JValue* __args = stackalloc JValue[2];
                __args[0] = new JValue(p0);
                __args[1] = new JValue(p1);

                global::AFollestad.MaterialDialogs.SimpleList.MaterialSimpleListAdapter.SimpleListVH __ret;
                if (GetType() == ThresholdType)
                    __ret = global::Java.Lang.Object.GetObject<global::AFollestad.MaterialDialogs.SimpleList.MaterialSimpleListAdapter.SimpleListVH>(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_onCreateViewHolder_Landroid_view_ViewGroup_I, __args), JniHandleOwnership.TransferLocalRef);
                else
                    __ret = global::Java.Lang.Object.GetObject<global::AFollestad.MaterialDialogs.SimpleList.MaterialSimpleListAdapter.SimpleListVH>(JNIEnv.CallNonvirtualObjectMethod(((global::Java.Lang.Object)this).Handle, ThresholdClass, JNIEnv.GetMethodID(ThresholdClass, "onCreateViewHolder", "(Landroid/view/ViewGroup;I)Lcom/afollestad/materialdialogs/simplelist/MaterialSimpleListAdapter$SimpleListVH;"), __args), JniHandleOwnership.TransferLocalRef);
                return __ret as RecyclerView.ViewHolder;
            }
            finally
            {
            }
        }
    }
}