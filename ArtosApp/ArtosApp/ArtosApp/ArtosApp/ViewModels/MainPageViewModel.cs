using ArtosApp.Helpers;
using ArtosApp.Models;
using ArtosApp.Services;
using ArtosApp.Views;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ArtosApp.ViewModels
{
    internal class MainPageViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly IAlertService _alertService;

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        public MainPageViewModel(INavigation navigation, IAlertService alertService)
        {
            _navigation = navigation;
            _alertService = alertService;
            LoginCommand = new Command(async () =>
            {
                await Login();
            });
            RegisterCommand = new Command(async () => {
                await _navigation.PushAsync(new Register());
            });
        }

        public async Task Login()
        {
            IsBusy = true;

            string loggedInUser = await LocalStorage.GetByKeyFromCacheAsync<string>("LoggedInUser");
            if (loggedInUser != null)
            {
                StaticVariableHelper.LoggedUser = JsonConvert.DeserializeObject<LoginResponse>(loggedInUser);
                await _navigation.PushAsync(new DeposPage());
            }
            else
            {
                await _navigation.PushAsync(new Login());
            }
            IsBusy = false;
        }
    }
}
