using KittyCatApp.Models;
using KittyCatApp.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KittyCatApp.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public IDataStore<Translation> DataStore => DependencyService.Get<IDataStore<Translation>>();


        static readonly string key = "";
        static readonly string host = "https://api.cognitive.microsofttranslator.com/";
        static readonly string location = "westus2";
        public async Task<string> TranslateTextAsync(string inputText)
        {

            string path = $"/translate?api-version=3.0&from=en&to=fr";

            object[] body = new object[] { new { Text = inputText } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(host + path);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);
                request.Headers.Add("Ocp-Apim-Subscription-Region", location);

                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);

                string result = await response.Content.ReadAsStringAsync();
                JObject responseBody = JObject.Parse(result);

                int thing = 0;

                Console.WriteLine("");

                return result;
            }
        }
    }
}
