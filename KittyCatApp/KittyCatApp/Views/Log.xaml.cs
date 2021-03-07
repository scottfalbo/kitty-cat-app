using KittyCatApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KittyCatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Log : ContentPage
    {
        readonly HomeViewModel _vm;

        public Log()
        {
            InitializeComponent();
            BindingContext = _vm = new HomeViewModel();
        }

        protected override void OnAppearing()
        {
            Task.Run(async () => await _vm.RetrieveLogs()).Wait();
        }
    }
}