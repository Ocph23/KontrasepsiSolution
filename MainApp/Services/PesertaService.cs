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
    public class PesertaService
    {
        internal async Task<Peserta> GetAsync(int id)
        {
            try
            {
                using var rest = new RestService();
                var response = await rest.GetAsync($"/api/pesertas/{id}");
                var stringData = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var result  = JsonSerializer.Deserialize<Peserta>(stringData, Helper.JsonOptions);
                    return result;
                }
                else
                {
                    throw await Helper.ErrorHandle(response);
                }

            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }



        internal async Task<Peserta> PuAsync(int id, Peserta model)
        {
            try
            {
                using var rest = new RestService();
                var response = await rest.PutAsync($"/api/pesertas/{id}",rest.GenerateHttpContent(model) );
                var stringData = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<Peserta>(stringData, Helper.JsonOptions);
                    Preferences.Set("peserta", stringData);
                    return result;
                }
                else
                {
                    throw await Helper.ErrorHandle(response);
                }

            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }
    }
}
