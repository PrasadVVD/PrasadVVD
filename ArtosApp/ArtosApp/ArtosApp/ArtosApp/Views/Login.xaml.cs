using ArtosApp.Services;
using ArtosApp.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArtosApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private readonly LoginViewModel _vm;
        public Login()
        {
            InitializeComponent();
            BindingContext = _vm = new LoginViewModel(Navigation, new AlertService());
        }
    }
}