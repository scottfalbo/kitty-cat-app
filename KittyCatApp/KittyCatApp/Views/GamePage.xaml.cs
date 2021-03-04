using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KittyCatApp.Views
{
    public partial class GamePage : ContentPage
    {
        public GamePage()
        {
            InitializeComponent();
        }

        public void VibrateMe(object sender, EventArgs e)
        {
            try
            {
                //Vibration.Vibrate();
                var duration = TimeSpan.FromSeconds(1);
                Vibration.Vibrate(duration);
            }
            catch (FeatureNotSupportedException ex)
            {
                Console.WriteLine($"Exception message: {ex}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception message: {ex}");
            }
        }
    }
}