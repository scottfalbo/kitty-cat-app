using KittyCatApp.Models;
using KittyCatApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace KittyCatApp.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        //public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        public ObservableCollection<Locale> Items { get; set; }

        public HomeViewModel()
        {
            Items = new ObservableCollection<Locale>();
        }

        public async Task GetLocales()
        {
            try
            {
                Items.Clear();
                var locales = await TextToSpeech.GetLocalesAsync();
                
                var locale = locales.ToList();
                foreach (var item in locale)
                {
                    Items.Add(item);
                }
                Console.WriteLine("whatever");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
