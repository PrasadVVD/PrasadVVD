using Newtonsoft.Json;
using SmartFinance.Helpers;
using SmartFinance.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartFinance.ViewModels
{
    public class AddLineViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly IAlertService _alertService;
        private readonly LineService _lineService;
        private readonly string _errorTitle = "Server Error";
        private readonly string _okButton = "Ok";
        public string LineName { get; set; }
        public decimal InterestRate { get; set; }
        public ICommand SaveButton { get; set; }
        public int CollectionMode { get; set; }
        public bool IsEditMode { get; set; }
        public AddLineViewModel(INavigation navigation, IAlertService alertService)
        {
            _navigation = navigation;
            _alertService = alertService;
            _lineService = new LineService();
            SaveButton = new Command(async () =>
            {
                if (!IsEditMode)
                {
                    await SaveLine();
                }
                else
                {
                    await UpdateLine();
                }
            });
        }

        private async Task SaveLine()
        {
            IsBusy = true;
            try
            {
                var res = await _lineService.SaveLine(new Models.LineVM
                {
                    CollectionMode = CollectionMode,
                    LineName = LineName,
                    InterestPer980 = InterestRate,
                    UserId = StaticVariableHelper.LoggedUser.Id
                });
                _alertService.DisplayAlert("Add Line", res, "Ok");
                await GetLinesFromServer();
                IsBusy = false;
                await _navigation.PopAsync();
            }
            catch (Exception ex)
            {
                _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);
                IsBusy = false;
            }
        }

        private async Task UpdateLine()
        {
            IsBusy = true;
            try
            {
                var res = await _lineService.UpdateLine(new Models.LineVM
                {
                    CollectionMode = CollectionMode,
                    LineName = LineName,
                    InterestPer980 = InterestRate,
                    UserId = StaticVariableHelper.LoggedUser.Id
                });
                _alertService.DisplayAlert("Update Line", res, "Ok");
                await GetLinesFromServer();
                IsBusy = false;
                await _navigation.PopAsync();
            }
            catch (Exception ex)
            {
                _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);
                IsBusy = false;
            }
        }

        private async Task GetLinesFromServer()
        {
            try
            {
                var lines = await _lineService.GetLinesForUser(StaticVariableHelper.LoggedUser.Id);
                if (lines != null)
                {
                    LocalStorage.SaveByKeyToCache("SavedLines", JsonConvert.SerializeObject(lines));
                }
            }
            catch (Exception ex)
            {
                _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);                
            }
        }
    }
}
