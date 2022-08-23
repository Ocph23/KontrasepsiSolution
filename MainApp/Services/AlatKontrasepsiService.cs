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
        static List<AlatKontrasepsi> list;

        public AlatKontrasepsiService()
        {

        }

        internal async Task<List<AlatKontrasepsi>> Get()
        {
            try
            {
                if (list != null)
                    return list;

                using var rest = new RestService();
                var response = await rest.GetAsync("/api/AlatKontrasepsis");
                var stringData = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    list = JsonSerializer.Deserialize<List<AlatKontrasepsi>>(stringData, Helper.JsonOptions);
                    return list;
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
