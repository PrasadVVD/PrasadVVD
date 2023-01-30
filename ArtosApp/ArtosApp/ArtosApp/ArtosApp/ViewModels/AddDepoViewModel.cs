using ArtosApp.Helpers;
using ArtosApp.Services;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ArtosApp.ViewModels
{
    public class AddDepoViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly IAlertService _alertService;
        private readonly DepoService _depoService;
        private readonly string _errorTitle = "Server Error";
        private readonly string _okButton = "Ok";
        public string DepoName { get; set; }        
        public ICommand SaveButton { get; set; }        
        public bool IsEditMode { get; set; }

        public AddDepoViewModel(INavigation navigation, IAlertService alertService)
        {
            _navigation = navigation;
            _alertService = alertService;
            _depoService = new DepoService();
            SaveButton = new Command(async () =>
            {
                if (!IsEditMode)
                {
                    await SaveDepo();
                }
                else
                {
                    //await UpdateDepo();
                }
            });
        }

        private async Task SaveDepo()
        {
            IsBusy = true;
            try
            {
                var res = await _depoService.SaveDepo(new Models.DepoVM
                {
                    DepoName = DepoName,
                    UserId = StaticVariableHelper.LoggedUser.Id
                });

                _alertService.DisplayAlert("Add Depo", res, "Ok");
                await GetDeposFromServer();
                IsBusy = false;
                await _navigation.PopAsync();
            }
            catch (Exception ex)
            {
                _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);
                IsBusy = false;
            }
        }

        private async Task GetDeposFromServer()
        {
            try
            {
                var depos = await _depoService.GetDeposForUser(StaticVariableHelper.LoggedUser.Id);
                if (depos != null)
                {
                    LocalStorage.SaveByKeyToCache("SavedDepos", JsonConvert.SerializeObject(depos));
                }
            }
            catch (Exception ex)
            {
                _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);
            }
        }
    }
}
