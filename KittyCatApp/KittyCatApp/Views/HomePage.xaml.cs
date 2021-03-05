using KittyCatApp.ViewModels;
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

        public async void SayTheThing(object sender, EventArgs e)
        {
            await TextToSpeech.SpeakAsync(EntryText.Text, new SpeechOptions
            {
                Volume = (float)SliderVolume.Value
            });
            Task.Run(async () => await _vm.GetLocales()).Wait();
        }

    }
}