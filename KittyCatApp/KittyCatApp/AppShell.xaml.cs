using KittyCatApp.Views;
using Xamarin.Forms;

namespace KittyCatApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(Log), typeof(Log));
        }
    }
}
