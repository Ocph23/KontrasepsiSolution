using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MainApp
{
    public class RestService : HttpClient
    {
        public RestService()
        {

            // this.MaxResponseContentBufferSize = 256000;
            //var a = ConfigurationManager.AppSettings["IP"];
            string _server = Helper.Url;

            BaseAddress = new Uri(_server);
            DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            //key api = 57557c4f25f436213fe34a2090a266e2
            this.CekTokenAsync();
        }

        public RestService(string apiUrl)
        {
            BaseAddress = new Uri(apiUrl);

        }

        public void CekTokenAsync()
        {
            var token = Preferences.Get("token", string.Empty);
            if (!string.IsNullOrEmpty(token))
            {
                this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }


        public void SetToken(string token)
        {
            if (token != null)
            {
                DefaultRequestHeaders.TryAddWithoutValidation("Authorization",
                    token);
                this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
        public StringContent GenerateHttpContent(object data)
        {
            var json = JsonSerializer.Serialize(data);
            //System.Text.Json.Serialization.Ser JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return content;
        }
    }

    internal class NameValueCollection
    {
        internal void Add(string v1, string v2)
        {
            throw new NotImplementedException();
        }
    }



}