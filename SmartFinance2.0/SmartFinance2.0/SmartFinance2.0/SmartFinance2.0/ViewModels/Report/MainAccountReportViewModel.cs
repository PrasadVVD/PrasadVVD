using Newtonsoft.Json;
using SmartFinance.Helpers;
using SmartFinance.Models;
using SmartFinance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartFinance.ViewModels.Report
{
    public class MainAccountReportViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly IAlertService _alertService;
        private readonly MainAccountService _mainAccountService;
        private readonly string _errorTitle = "Server Error";
        private readonly string _okButton = "Ok";

        public int LineNo { get; set; }                
        public List<string> LineDays { get; set; }
        public List<MainAccountVM> MainAccounts { get; set; }

        public bool DaysListVisible { get; set; }
        public bool ReportListVisible { get; set; }
        public ICommand DeleteMainAccount { get; set; }
        public bool IsLoadingDays { get; set; }
        public ICommand GetDaysFromServer { get; set; }

        public MainAccountReportViewModel(INavigation navigation, IAlertService alertService, int lineNo)
        {
            LineNo = lineNo;
            _navigation = navigation;
            _alertService = alertService;
            _mainAccountService = new MainAccountService();

            DeleteMainAccount = new Command(async (mainAccount) => 
            {
                try
                {
                    var userRes = await _alertService.DisplayAlert("Delete", "Please confirm to delete this main account?", "Ok", "Cancel");
                    if (userRes)
                    {
                        MainAccountVM accountVM = (MainAccountVM)mainAccount;
                        var res = await _mainAccountService.DeleteMainAccount(accountVM.Id);
                        _alertService.DisplayAlert("Main Account", res, "Ok");
                        await _navigation.PopAsync();
                    }
                }
                catch (Exception ex)
                {
                    _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);                   
                }
            });

            GetDaysFromServer = new Command(async () =>
            {
                IsLoadingDays = true;
                try
                {
                    LineDays = await GetLineDaysFromServer();
                    if (LineDays != null && LineDays.Any())
                    {
                        OnPropertyChanged("LineDays");
                        DaysListVisible = true;
                        OnPropertyChanged("DaysListVisible");
                        ReportListVisible = false;
                        OnPropertyChanged("ReportListVisible");
                    }
                }
                catch (Exception ex)
                {
                    _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);                    
                }
                IsLoadingDays = false;
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
                    DaysListVisible = true;
                    OnPropertyChanged("DaysListVisible");
                    ReportListVisible = false;
                    OnPropertyChanged("ReportListVisible");
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

        public async Task GetMainAccountsForDay(string day)
        {
            IsBusy = true;
            try
            {
                MainAccounts = await _mainAccountService.GetMainAcounts(LineNo, day);
                if (MainAccounts != null && MainAccounts.Any())
                {
                    var balanceTransfers = await _mainAccountService.GetBalanceTransfers(LineNo);
                    if (balanceTransfers != null && balanceTransfers.Any())
                    {
                        foreach(var mainAccount in MainAccounts)
                        {
                            var btDetails = balanceTransfers.FirstOrDefault(x => x.FromRecord == mainAccount.Id);
                            if (btDetails!=null)
                            {
                                mainAccount.IsBalanceTransfer = true;
                                mainAccount.BalanceTransferAmount = btDetails.Amount;
                                mainAccount.BalanceTransferComments = btDetails.Comments;
                            }
                            var refundDetails = balanceTransfers.FirstOrDefault(x => x.ToRecord == mainAccount.Id);
                            if (refundDetails != null)
                            {
                                mainAccount.IsRefundTransfer = true;
                                mainAccount.BalanceTransferAmount = refundDetails.Amount;
                                mainAccount.BalanceTransferComments = refundDetails.Comments;
                            }
                        }
                    }

                    MainAccounts.OrderByDescending(u => u.Id).FirstOrDefault().IsDeleteVisible = true;
                    DaysListVisible = false;
                    OnPropertyChanged("DaysListVisible");
                    ReportListVisible = true;
                    OnPropertyChanged("ReportListVisible");
                }
                else
                {
                    MainAccounts = new List<MainAccountVM>();
                }
                OnPropertyChanged("MainAccounts");
            }
            catch (Exception ex)
            {
                _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);                
            }
            IsBusy = false;
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
