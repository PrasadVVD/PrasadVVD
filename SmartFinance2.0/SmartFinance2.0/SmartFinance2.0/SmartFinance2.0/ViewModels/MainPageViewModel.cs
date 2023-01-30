using Newtonsoft.Json;
using SmartFinance.Helpers;
using SmartFinance.Models;
using SmartFinance.Services;
using SmartFinance.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartFinance.ViewModels
{
    public class MainPageViewModel : BaseViewModel
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
            if (loggedInUser!=null)
            {
                StaticVariableHelper.LoggedUser = JsonConvert.DeserializeObject<LoginResponse>(loggedInUser);
                await _navigation.PushAsync(new LinesPage());
            }
            else
            {
                await _navigation.PushAsync(new Login());
            }
            IsBusy = false;            
        }
    }
}
