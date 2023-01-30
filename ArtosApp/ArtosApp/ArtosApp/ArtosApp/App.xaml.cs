using ArtosApp.Helpers;
using Xamarin.Forms;

namespace ArtosApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            LocalStorage.Init();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
