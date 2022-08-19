using MainApp.Models;
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
                    Preferences.Set("peserta", stringData);
                    Preferences.Set("user", result.Email);
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
                if (response.IsSuccessStatusCode)
                {
                    var stringData = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<Peserta>(stringData);
                    Preferences.Set("peserta", stringData);
                    Preferences.Set("user", result.Email);
                    await Shell.Current.DisplayAlert("Info", "Registrasi Berhasil !, Silahkan Periksan Email Anda !", "OK");
                    return true;
                }

                throw new SystemException("Registrasi Tidak Berhasil !, Coba Ulangi Lagi !");
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }
    }
}
