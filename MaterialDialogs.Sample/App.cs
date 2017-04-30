using Android.App;
using Android.Runtime;
using System;

namespace AFollestad.MaterialDialogs.Sample
{
    [Application]
    public class App : Application
    {
        public App() : base()
        {
        }

        public App(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

#if DEBUG
            // TODO add Stetho package
            // Stetho.InitializeWithDefaults(this);
#endif
        }
    }
}