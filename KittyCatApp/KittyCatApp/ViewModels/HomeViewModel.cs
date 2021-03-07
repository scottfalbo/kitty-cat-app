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


        static readonly string key = "32750c2dda8c4834ac8303cf89cc8463";
        static readonly string host = "https://api.cognitive.microsofttranslator.com/";
        static readonly string location = "westus2";
        public async Task<string> TranslateTextAsync(string inputText, string lang)
        {

            string path = $"/translate?api-version=3.0&from=en&to={lang}";

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

                return result;
            }
            
        }

        public string[] DeserializeObject(string json)
        {
            string innerObject = "";

            JArray parsed = JArray.Parse(json);
            foreach (JObject textObject in parsed.Children<JObject>())
            {
                foreach (JProperty prop in textObject.Properties())
                {
                    innerObject = (string)(prop.Value[0]).ToString();
                }
            }

            JObject parsedInner = JObject.Parse(innerObject);
            string[] values = new string[2];

            foreach (JProperty innerProp in parsedInner.Properties())
            {
                string propName = innerProp.Name;
                if (propName.Equals("text"))
                    values[0] = (string)innerProp.Value;
                if (propName.Equals("to"))
                    values[1] = (string)innerProp.Value;
            }

            return values;
        }
    }
}
