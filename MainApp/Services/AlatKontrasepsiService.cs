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
    public class AlatKontrasepsiService
    {
        public AlatKontrasepsiService()
        {

        }

        internal async Task<AlatKontrasepsi> Get()
        {
            try
            {
                using var rest = new RestService();
                var response = await rest.GetAsync("/api/alatkontrasepsi");
                var stringData = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<AlatKontrasepsi>(stringData, Helper.JsonOptions);
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
    }
}
