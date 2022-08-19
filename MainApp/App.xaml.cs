using Plugin.FirebasePushNotification;

namespace MainApp
{
    public partial class App : Application
    {

        private static string FcmToken;

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            CrossFirebasePushNotification.Current.OnTokenRefresh+= (s, p) => {
                FcmToken = p.Token;
                Preferences.Set("FcmToken", FcmToken);
                System.Diagnostics.Debug.WriteLine($"{p.Token}");
            };
            
            
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) => {
            
            
            };

            CrossFirebasePushNotification.Current.OnNotificationOpened+= (s, p) => {


            };


            //CheckToken().Wait();


        }

        private async Task CheckToken()
        {
            var result = await CrossFirebasePushNotification.Current.GetTokenAsync();
        }
    }
}