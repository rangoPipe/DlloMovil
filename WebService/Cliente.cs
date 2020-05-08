using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace WebService
{
    public class Cliente
    {
        public HttpStatusCode CodigoHttp { get; set; }

        public Cliente()
        {
            CodigoHttp = HttpStatusCode.OK;
        }


        public async Task<T> Get<T>(string url)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            CodigoHttp = response.StatusCode;
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<T> Post<T>(Entrada item, string url)
        {
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            response = await client.PostAsync(url, content);
            json = await response.Content.ReadAsStringAsync();
            CodigoHttp = response.StatusCode;
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}