using Newtonsoft.Json;
using SmartFinance.Helpers;
using SmartFinance.Models;
using SmartFinance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartFinance.ViewModels
{
    public class BalanceTransferViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly IAlertService _alertService;
        private readonly string _errorTitle = "Server Error";
        private readonly string _messageTitle = "Balance Transfer";
        private readonly string _okButton = "Ok";
        private readonly MainAccountService _mainAccountService;

        public int LineNo { get; set; }
        public List<string> LineDays { get; set; }
        public MainAccountVM FromLineMainAccount { get; set; }
        public MainAccountVM ToLineMainAccount { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public ICommand SaveButton { get; set; }
        public BalanceTransferViewModel(INavigation navigation, IAlertService alertService, int lineNo)
        {
            _navigation = navigation;
            _alertService = alertService;
            _mainAccountService = new MainAccountService();            
            LineNo = lineNo;
            SaveButton = new Command(async () =>
            {
                await DoBalanceTransfer();
            });
        }

        public async Task GetLineDays()
        {
            IsBusy = true;
            try
            {
                string savedLineDays = await LocalStorage.GetByKeyFromCacheAsync<string>("LineDays" + LineNo);
                if (!string.IsNullOrEmpty(savedLineDays) && savedLineDays != "null")
                {
                    StaticVariableHelper.LineDays = JsonConvert.DeserializeObject<List<string>>(savedLineDays);
                    LineDays = StaticVariableHelper.LineDays;
                }
                else
                {
                    LineDays = await GetLineDaysFromServer();

                }
                if (LineDays != null && LineDays.Any())
                {
                    OnPropertyChanged("LineDays");                    
                }
                else
                {
                    LineDays = new List<string>();
                }
            }
            catch (Exception ex)
            {
                _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);
            }
            IsBusy = false;
        }
        
        public async Task<MainAccountVM> GetLastestMainAccountForDay(string lineDay)
        {
            try
            {
                return await _mainAccountService.GetLastestAccountForDay(LineNo, lineDay);
            }
            catch (Exception ex)
            {
                _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);
            }
            return null;
        }
        
        public async Task DoBalanceTransfer()
        {
            if (FromLineMainAccount == null)
            {
                _alertService.DisplayAlert(_messageTitle, "Please select from line day", _okButton);
                return;
            }
            if (ToLineMainAccount == null)
            {
                _alertService.DisplayAlert(_messageTitle, "Please select to line day", _okButton);
                return;
            }
            if (Amount < 0)
            {
                _alertService.DisplayAlert(_messageTitle, "Please enter valid amount!", _okButton);
                return;
            }
            var res = await _mainAccountService.BalanceTransfer(new BalanceTransferVM
            {
                LineId = LineNo,
                TransferDate = DateTime.Now,
                FromRecord = FromLineMainAccount.Id,
                ToRecord = ToLineMainAccount.Id,
                FromDay = FromLineMainAccount.LineDay,
                ToDay = ToLineMainAccount.LineDay,
                Amount = Amount,
                UserId = StaticVariableHelper.LoggedUser.Id,
                Comments = Description
            });

            if (res != null)
            {
                _alertService.DisplayAlert(_messageTitle, "Balance transfer sucessfull.", _okButton);
                await _navigation.PopAsync();
            }
        }
        private async Task<List<string>> GetLineDaysFromServer()
        {
            List<string> days = null;
            try
            {
                days = await _mainAccountService.GetLineDays(LineNo);
                if (days != null)
                {
                    LocalStorage.SaveByKeyToCache("LineDays" + LineNo, JsonConvert.SerializeObject(days));
                }
                else
                {
                    days = new List<string>();
                }
            }
            catch (Exception ex)
            {
                _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);
            }
            return days;
        }
    }
}
