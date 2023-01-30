using ArtosApp.Services;
using ArtosApp.ViewModels;
using Xamarin.Forms;

namespace ArtosApp
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _vm;
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BindingContext = _vm = new MainPageViewModel(Navigation, new AlertService());
        }
    }
}
