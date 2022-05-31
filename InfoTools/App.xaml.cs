using Xamarin.Forms;
using InfoTools.Pages;

namespace InfoTools
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
