using SmartFinance.Services;
using SmartFinance.ViewModels;
using Xamarin.Forms;

namespace SmartFinance
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _vm;
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BindingContext = _vm = new MainPageViewModel(Navigation, new AlertService());              
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();            
        }
    }
}
