using CommunityToolkit.Maui.Alerts;
using MainApp.Models;
using MainApp.Pages;
using MainApp.Services;
using Plugin.FirebasePushNotification;

namespace MainApp
{
    public partial class App : Application
    {

        private static string FcmToken;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<AccountService>();
            DependencyService.Register<AlatKontrasepsiService>();
            DependencyService.Register<PengajuanService>();
            DependencyService.Register<PesertaService>();


            var loginUser = Preferences.Get("userName", null);

            if (string.IsNullOrEmpty(loginUser))
            {
                MainPage = new LoginPage();
            }
            else
            {
                MainPage = new AppShell();
            }

            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                FcmToken = p.Token;
                Preferences.Set("FcmToken", FcmToken);
                System.Diagnostics.Debug.WriteLine($"Ini Device Token : {p.Token}");
                var service = DependencyService.Get<AccountService>();
                _ = service.UpdateDeviceToken(FcmToken);

            };


            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                try
                {
                    var body = p.Data.Values.ToList()[0].ToString();
                    var title = p.Data.Values.ToList()[1].ToString();
                    ShowMessage(title, body);
                }
                catch (Exception)
                {

                    //throw;
                }
            };

            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {


            };


            //CheckToken().Wait();


        }

        private Task ShowMessage(string title, string body)
        {
            Shell.Current.DisplaySnackbar(body,()=>Test(), "OK",TimeSpan.FromSeconds(5), new CommunityToolkit.Maui.Core.SnackbarOptions {  });;
            return Task.CompletedTask;
        }

        private void Test() { }

        private async Task CheckToken()
        {
            var result = await CrossFirebasePushNotification.Current.GetTokenAsync();
        }
    }
}