using ArtosApp.Helpers;
using ArtosApp.Services;
using ArtosApp.Views;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ArtosApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly IAlertService _alertService;
        private UserService _userService;
        private readonly string _errorTitle = "Server Error";
        private readonly string _okButton = "Ok";

        public string Username { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand { get; set; }
        public LoginViewModel(INavigation navigation, IAlertService alertService)
        {
            _navigation = navigation;
            _alertService = alertService;
            _userService = new UserService();

            LoginCommand = new Command(async () => { await CheckLogin(); });
        }

        public async Task CheckLogin()
        {
            IsBusy = true;
            try
            {
                if (string.IsNullOrEmpty(Username))
                {
                    _alertService.DisplayAlert("Login", "Please enter user name!", "Ok");
                }
                else if (string.IsNullOrEmpty(Password))
                {
                    _alertService.DisplayAlert("Login", "Please enter password!", "Ok");
                }
                else
                {
                    var loginResponse = await _userService.UserLogin(new Models.LoginRequest { UserName = Username, Password = Password });
                    if (loginResponse != null)
                    {
                        LocalStorage.SaveByKeyToCache("LoggedInUser", JsonConvert.SerializeObject(loginResponse));
                        StaticVariableHelper.LoggedUser = loginResponse;
                        await _navigation.PushAsync(new DeposPage());
                    }
                    else
                    {
                        _alertService.DisplayAlert("Login", "Wrong user name or password!", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);
            }
            IsBusy = false;
        }
    }
}
