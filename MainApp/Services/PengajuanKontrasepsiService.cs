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
    public class PengajuanService
    {
        internal async Task<Pelayanan> GetAsync(int id)
        {
            try
            {
                using var rest = new RestService();
                var response = await rest.GetAsync($"/api/pengajuan/{id}");
                var stringData = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var result  = JsonSerializer.Deserialize<Pelayanan>(stringData, Helper.JsonOptions);
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


        internal async Task<Pelayanan> PostAsync(Pelayanan model)
        {
            try
            {
                using var rest = new RestService();
                var response = await rest.PostAsync("/api/pengajuan", rest.GenerateHttpContent(model));
                var stringData = await response.Content.ReadAsStringAsync();

                
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<Pelayanan>(stringData, Helper.JsonOptions);
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

        internal async Task<Pelayanan> PutsAsync(Pelayanan model)
        {
            try
            {
                using var rest = new RestService();
                var response = await rest.PutAsync($"/api/pengajuan?id={model.Id}", rest.GenerateHttpContent(model));
                var stringData = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<Pelayanan>(stringData, Helper.JsonOptions);
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
