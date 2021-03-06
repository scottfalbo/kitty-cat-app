using KittyCatApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KittyCatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {

        readonly HomeViewModel _vm;
        public HomePage()
        {
            InitializeComponent();
            BindingContext = _vm = new HomeViewModel();
        }

        public async void French(object sender, EventArgs e)
        {
            var translatedText = _vm.TranslateTextAsync(EntryText.Text);
            string text = translatedText.ToString();

            var locales = await TextToSpeech.GetLocalesAsync();
            var locale = locales.ToList();

            await TextToSpeech.SpeakAsync(text, new SpeechOptions
            {
                Volume = (float)SliderVolume.Value,
                Locale = locale[22]
            });
            
        }

    }
}