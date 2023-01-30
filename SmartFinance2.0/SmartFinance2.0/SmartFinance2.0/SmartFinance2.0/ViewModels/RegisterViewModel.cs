using SmartFinance.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartFinance.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly IAlertService _alertService;
        private readonly UserService _userService;
        private readonly string _errorTitle = "Server Error";
        private readonly string _okButton = "Ok";

        private const string _title = "Register";
        private const string _okMessage = "Ok";
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
        public string Mobile { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ObservableCollection<string> RoleOptions { get; set; }
        
        public RegisterViewModel(INavigation navigation, IAlertService alertService)
        {
            _navigation = navigation;
            _alertService = alertService;
            _userService = new UserService();
            RoleOptions = new ObservableCollection<string> { "Owner", "Employee" };
            RegisterCommand = new Command(async () => { await RegisterNew(); });
        }

        public async Task RegisterNew()
        {
            IsBusy = true;
            try
            {
                if (!IsValid())
                {
                    IsBusy = false;
                    return;
                }
                var userRole = UserRole == "Owner" ? Models.UserRole.Admin : Models.UserRole.Employee;
                var res = await _userService.RegisterNewUser(new Models.UserVM { FullName = Username, AppType = 1, MobileNo = Mobile, Email = Email, Role = userRole, Password = Password });
                if (res != null)
                {
                    _alertService.DisplayAlert("Register", res, "Ok");
                    await _navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);                
            }
            IsBusy = false;
        }

        public bool IsValid()
        {
            if(string.IsNullOrEmpty(Username))
            {
                _alertService.DisplayAlert(_title, "Please enter username", _okMessage);
                return false;
            }
            else if(Username.Contains(" "))
            {
                _alertService.DisplayAlert(_title, "Please enter username without spaces", _okMessage);
                return false;
            }
            else if (string.IsNullOrEmpty(Password))
            {
                _alertService.DisplayAlert(_title, "Please enter mobile", _okMessage);
                return false;
            }
            else if (string.IsNullOrEmpty(UserRole))
            {
                _alertService.DisplayAlert(_title, "Please select user role", _okMessage);
                return false;
            }
            return true;
        }

    }
}
