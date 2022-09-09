using MainApp.Models;
using MainApp.Pages;
using Microsoft.Maui.Accessibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MainApp
{
    internal class Helper
    {
        //public static string Url { get; internal set; } = "http://192.168.1.15/";
        public static string Url { get; internal set; } = "https://kontrasepsikb.ocphapp.tech/";
        public static JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        internal static async Task SetPage(Page loginPage)
        {
            await Task.Delay(2000);
            var app = (App)Application.Current;
            app.MainPage = loginPage;
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        internal static List<EnumDisplay<Pendidikan>> GetPendidikan()
        {
            var sources = Enum.GetValues(typeof(Pendidikan)).Cast<Pendidikan>().ToList();
            var datas = new List<EnumDisplay<Pendidikan>>();
            foreach (var data in sources)
            {
                datas.Add(new EnumDisplay<Pendidikan>() { Value = data, Text = data.ToStringText() });
            }

            return datas;
        }

        internal static List<EnumDisplay<Pekerjaan>> GetPekerjaan()
        {
            var sources = Enum.GetValues(typeof(Pekerjaan)).Cast<Pekerjaan>().ToList();
            var datas = new List<EnumDisplay<Pekerjaan>>();
            foreach (var data in sources)
            {
                datas.Add(new EnumDisplay<Pekerjaan>() { Value = data, Text = data.ToStringText() });
            }

            return datas;
        }

        internal static async Task<Exception> ErrorHandle(HttpResponseMessage response)
        {

            try
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    _ = LoadLogin();
                    throw new SystemException("Maaf anda tidak memiliki akses !");
                }

                var stringData = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(stringData))
                {
                    throw new SystemException(response.StatusCode.ToString());
                }

                var errorMessage = JsonSerializer.Deserialize<ErrorMessage>(stringData, Helper.JsonOptions);
                throw new SystemException($"{errorMessage.Status} - {errorMessage.Title} - {errorMessage?.Detail}");
            }
            catch (Exception)
            {
                throw new SystemException(response.StatusCode.ToString());
            }
        }

        private static async Task LoadLogin()
        {
            await Task.Delay(5000);
         await  Shell.Current.Navigation.PushAsync(new LoginPage());
        }

        internal static List<EnumDisplay<JKN>> GetJKN()
        {
            var sources = Enum.GetValues(typeof(JKN)).Cast<JKN>().ToList();
            var datas = new List<EnumDisplay<JKN>>();
            foreach (var data in sources)
            {
                datas.Add(new EnumDisplay<JKN>() { Value = data, Text = data.ToStringText() });
            }

            return datas;
        }
    }
}