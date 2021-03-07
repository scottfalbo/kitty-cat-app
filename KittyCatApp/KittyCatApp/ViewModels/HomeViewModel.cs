using KittyCatApp.Models;
using KittyCatApp.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KittyCatApp.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public IDataStore<Translation> DataStore => DependencyService.Get<IDataStore<Translation>>();
        public event PropertyChangedEventHandler PropertyChanged;

        static readonly string key = "";  // it's a secret
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
                {
                    values[0] = (string)innerProp.Value;
                    newPhrase = values[0];
                    OnPropertyChanged(nameof(TranslatedPhrase));
                }
                if (propName.Equals("to"))
                    values[1] = (string)innerProp.Value;

            }

            return values;
        }

        public async void SaveTranslation(string input, string lang, string translated)
        {
            Translation saveItem = new Translation()
            {
                Id = 0,
                InputText = input,
                Language = lang,
                TranslatedText = translated
            };
            await DataStore.AddItemAsync(saveItem);
        }

        private string text = string.Empty;
        private string newPhrase = string.Empty;

        public string TranslatedPhrase => $"Translated: {newPhrase}";

        public string Text
        {
            get { return text; }
            set
            {
                if (text == value)
                {
                    return;
                }
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public ObservableCollection<LogTranscript> Logs { get; set; }

        public HomeViewModel()
        {
            Logs = new ObservableCollection<LogTranscript>();
        }

        public async Task RetrieveLogs()
        {
            try
            {
                Logs.Clear();
                var logs = await DataStore.GetItemsAsync();
                foreach (var record in logs)
                {
                    string lang = LanguageText(record.Language);
                    string entry = $"{record.InputText} : translated to {lang} : {record.TranslatedText}";
                    LogTranscript transcript = new LogTranscript() 
                    {
                        Transcript = entry
                    };
 
                    Logs.Add(transcript);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"something went wrong with the DB: {e}");
            }
        }

        public string LanguageText(string input)
        {
            switch (input)
            {
                case "fr":
                    return "French";
                case "de":
                    return "German";
                case "es":
                    return "Spanish";
                case "ja":
                    return "Japan";
                default:
                    return "womp womp"; 
            }      
        }

        void OnPropertyChanged(string text)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(text));
        }
    }
}
