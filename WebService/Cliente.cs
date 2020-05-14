using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
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

        public async Task<T> Put<T>(Entrada item, string url)
        {
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            response = await client.PutAsync(url, content);
            json = await response.Content.ReadAsStringAsync();
            CodigoHttp = response.StatusCode;
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task Delete(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync(url);
            CodigoHttp = response.StatusCode;
        }

        public async Task<T> Patch<T>(Content item, string url)
        {
            HttpClient client = new HttpClient();
            var json = serializePatch(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PatchAsync(url, content);
            json = await response.Content.ReadAsStringAsync();
            CodigoHttp = response.StatusCode;
            return JsonConvert.DeserializeObject<T>(json);
        }

        protected string serializePatch(Content item)
        {
            Dictionary<string, string> propsNotNull = new Dictionary<string, string>();
            string data = string.Empty;

            PropertyInfo[] allProperties = item.GetType().GetProperties();
            foreach (PropertyInfo property in allProperties)
            {
                data = (string)property.GetValue(item, null);

                if (string.IsNullOrWhiteSpace(data))
                {
                    continue;
                }

                propsNotNull[property.Name] = data;
            }

            return JsonConvert.SerializeObject(propsNotNull);
        }
    }
}