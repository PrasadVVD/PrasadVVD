using Newtonsoft.Json;
using SmartFinance.Helpers;
using SmartFinance.Models;
using SmartFinance.Services;
using SmartFinance.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartFinance.ViewModels
{
    public class LinesViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly IAlertService _alertService;
        private readonly LineService _lineService;
        private readonly string _errorTitle = "Server Error";
        private readonly string _okButton = "Ok";

        public List<LineVM> LinesList { get; set; }
        public ICommand AddLineCommand { get; set; }
        public ICommand  AddVillagesCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand GetLinesFromServerCommand { get; set; }

        public LinesViewModel(INavigation navigation, IAlertService alertService)
        {
            _navigation = navigation;
            _alertService = alertService;
            _lineService = new LineService();

            GetLinesFromServerCommand = new Command(async () => 
            {
                IsBusy = true;
                try
                {
                    StaticVariableHelper.Lines = LinesList = await GetLinesFromServer();
                    OnPropertyChanged("LinesList");
                }
                catch (Exception ex)
                {
                    _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);
                }
                IsBusy = false;
            });

            AddLineCommand = new Command(async () => 
            {
                await _navigation.PushAsync(new AddLine());
            });

            SettingsCommand = new Command(async () =>
            {
                await _navigation.PushAsync(new SettingsPage(null));
            });
        }

        public async Task GetLines()
        {
            IsBusy = true;
            try
            {
                string savedLines = await LocalStorage.GetByKeyFromCacheAsync<string>("SavedLines");
                if (!string.IsNullOrEmpty(savedLines) && savedLines != "null")
                {
                    var lines = JsonConvert.DeserializeObject<List<LineVM>>(savedLines);
                    StaticVariableHelper.Lines = LinesList = SetBackgrounds(lines);                     
                }
                else
                {
                    StaticVariableHelper.Lines = LinesList = await GetLinesFromServer();
                }
            }
            catch (Exception ex)
            {
                _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);
            }
            OnPropertyChanged("LinesList");
            IsBusy = false;
        }

        private async Task<List<LineVM>> GetLinesFromServer()
        {
            List<LineVM> lines = null;
            try
            {
                lines = await _lineService.GetLinesForUser(StaticVariableHelper.LoggedUser.Id);
                if (lines != null)
                {
                    lines = SetBackgrounds(lines);
                    LocalStorage.SaveByKeyToCache("SavedLines", JsonConvert.SerializeObject(lines));
                }
                else
                {
                    lines = new List<LineVM>();
                }
                return lines;
            }
            catch (Exception ex)
            {
                _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);
            }
            return lines;
        }

        public async Task GotoHomePage(int lineId)
        {
            await _navigation.PushAsync(new HomePage(LinesList.FirstOrDefault(x=>x.Id == lineId)));
        }

        private List<LineVM> SetBackgrounds(List<LineVM> lines)
        {
            int i = 1;
            foreach (var line in lines)
            {
                line.BackGround = GetColor(i++);
                if (i == 10) { i = 1; }
            }
            return lines;
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
