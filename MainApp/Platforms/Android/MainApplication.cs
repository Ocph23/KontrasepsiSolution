using Android.App;
using Android.OS;
using Android.Runtime;
using Plugin.FirebasePushNotification;

namespace MainApp
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {

        }



        public override void OnCreate()
        {
            base.OnCreate();

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                FirebasePushNotificationManager.DefaultNotificationChannelId = "FirebasePushNotificationChannel";
                FirebasePushNotificationManager.DefaultNotificationChannelName = "General";
            }

            FirebasePushNotificationManager.Initialize(this, true);

            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) => {

            };
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}