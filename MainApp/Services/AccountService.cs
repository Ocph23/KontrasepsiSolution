using MainApp.Models;
using MainApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MainApp.Services
{
    public class AccountService
    {
        public AccountService()
        {

        }

        internal async Task<Peserta> RegisterAsync(Peserta model)
        {
            try
            {
                using var rest = new RestService();
                var response = await rest.PostAsync("/api/pesertas", rest.GenerateHttpContent(model));
                var stringData = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<Peserta>(stringData, Helper.JsonOptions);
                    await Application.Current.MainPage.DisplayAlert("Info", "Registrasi Berhasil !, Silahkan Periksan Email Anda !", "OK");
                    return result;
                }
                else
                {
                    var errorMessage = JsonSerializer.Deserialize<ErrorMessage>(stringData, Helper.JsonOptions);
                    throw new SystemException($"{errorMessage.Status} - {errorMessage.Title} - {errorMessage?.Detail}");
                }

            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }


        internal async Task<bool> LoginAsync(UserLogin model)
        {
            try
            {
                using var rest = new RestService();
                var response = await rest.PostAsync("/api/account/login", rest.GenerateHttpContent(model));
                var stringData = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<AuthenticateResponse>(stringData, Helper.JsonOptions);
                    var role = result.Roles.FirstOrDefault();
                    if(role== null || ! role.ToLower().Equals("pasien"))
                    {
                        throw new SystemException("Maaf anda bukan peserta/pasien");
                    }

                    Preferences.Set("account", stringData);
                    Preferences.Set("token", result.Token);
                    var pesertaString = Preferences.Get("userName", null);
                    if (pesertaString == null || pesertaString.ToLower() != result.UserName )
                    {
                        Preferences.Set("userName", result.UserName);
                        _=GetProfile();
                    }
                    return true;
                }

                var errorMessage = JsonSerializer.Deserialize<ErrorMessage>(stringData, Helper.JsonOptions);
                throw new SystemException($"{errorMessage.Status} - {errorMessage.Title} - {errorMessage?.Detail}");
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        internal async Task<Peserta> GetProfile()
        {
            try
            {
                using var rest = new RestService();
                var response = await rest.GetAsync("/api/account/profile");
                var stringData = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<Peserta>(stringData);
                    Preferences.Set("peserta", stringData);
                    return result;
                }

                var errorMessage = JsonSerializer.Deserialize<ErrorMessage>(stringData, Helper.JsonOptions);
                throw new SystemException($"{errorMessage.Status} - {errorMessage.Title} - {errorMessage?.Detail}");
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }


        internal Task Logout()
        {
            Preferences.Set("token", null);
            Preferences.Set("userName", null);
            Preferences.Set("account", null);
            Preferences.Set("peserta", null);
            Application.Current.MainPage = new LoginPage();
            return Task.CompletedTask;

        }

        internal async Task UpdateDeviceToken(string fcmToken)
        {
            try
            {
                await Task.Delay(1000);
                using var rest = new RestService();
                var response = await rest.GetAsync($"/api/account/updatedevicetoken?deviceToken={fcmToken}");
                var stringData = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                await  Helper.ErrorHandle(response);
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }
    }
}
