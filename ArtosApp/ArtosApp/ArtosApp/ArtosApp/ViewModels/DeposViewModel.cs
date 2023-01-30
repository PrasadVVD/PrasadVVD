using ArtosApp.Helpers;
using ArtosApp.Models;
using ArtosApp.Services;
using ArtosApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ArtosApp.ViewModels
{
    public class DeposViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly IAlertService _alertService;
        private readonly DepoService _depoService;
        private readonly string _errorTitle = "Server Error";
        private readonly string _okButton = "Ok";

        public List<DepoVM> DeposList { get; set; }
        public ICommand AddDepoCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand GetDeposFromServerCommand { get; set; }

        public DeposViewModel(INavigation navigation, IAlertService alertService)
        {
            _navigation = navigation;
            _alertService = alertService;
            _depoService = new DepoService();

            GetDeposFromServerCommand = new Command(async () =>
            {
                IsBusy = true;
                try
                {
                    StaticVariableHelper.Depos = DeposList = await GetDeposFromServer();
                    OnPropertyChanged("DeposList");
                }
                catch (Exception ex)
                {
                    _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);
                }
                IsBusy = false;
            });

            AddDepoCommand = new Command(async () =>
            {
                await _navigation.PushAsync(new AddDepoPage());
            });

            SettingsCommand = new Command(async () =>
            {
                //await _navigation.PushAsync(new SettingsPage(null));
            });
        }

        public async Task GetDepos()
        {
            IsBusy = true;
            try
            {
                string savedDepos = await LocalStorage.GetByKeyFromCacheAsync<string>("SavedDepos");
                if (!string.IsNullOrEmpty(savedDepos) && savedDepos != "null")
                {
                    var depos = JsonConvert.DeserializeObject<List<DepoVM>>(savedDepos);
                    StaticVariableHelper.Depos = DeposList = SetBackgrounds(depos);
                }
                else
                {
                    StaticVariableHelper.Depos = DeposList = await GetDeposFromServer();
                }
            }
            catch (Exception ex)
            {
                _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);
            }
            OnPropertyChanged("DeposList");
            IsBusy = false;
        }

        private async Task<List<DepoVM>> GetDeposFromServer()
        {
            List<DepoVM> depos = null;
            try
            {
                depos = await _depoService.GetDeposForUser(StaticVariableHelper.LoggedUser.Id);
                if (depos != null)
                {
                    depos = SetBackgrounds(depos);
                    LocalStorage.SaveByKeyToCache("SavedDepos", JsonConvert.SerializeObject(depos));
                }
                else
                {
                    depos = new List<DepoVM>();
                }
                return depos;
            }
            catch (Exception ex)
            {
                _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);
            }
            return depos;
        }

        public async Task GotoHomePage(int depoId)
        {
            await _navigation.PushAsync(new HomePage(DeposList.FirstOrDefault(x => x.DepoId == depoId)));
        }

        private List<DepoVM> SetBackgrounds(List<DepoVM> depos)
        {
            int i = 1;
            foreach (var depo in depos)
            {
                depo.BackGround = GetColor(i++);
                if (i == 10) { i = 1; }
            }
            return depos;
        }

        private Color GetColor(int id)
        {
            switch (id)
            {
                case 1: return Color.CadetBlue;
                case 2: return Color.Crimson;
                case 3: return Color.DarkCyan;
                case 4: return Color.DarkOliveGreen;
                case 5: return Color.DarkSlateBlue;
                case 6: return Color.DarkSlateGray;
                case 7: return Color.DarkRed;
                case 8: return Color.Indigo;
                case 9: return Color.MediumBlue;
                case 10: return Color.Teal;
                default: return Color.Brown;
            }
        }
    }
}
