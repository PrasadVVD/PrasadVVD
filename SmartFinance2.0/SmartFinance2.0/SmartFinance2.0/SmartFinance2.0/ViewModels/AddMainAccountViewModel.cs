using SmartFinance.Helpers;
using SmartFinance.Models;
using SmartFinance.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartFinance.ViewModels
{
    public class AddMainAccountViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly IAlertService _alertService;
        private readonly MainAccountService _mainAccountService;
        private readonly string _errorTitle = "Server Error";
        private readonly string _okButton = "Ok";

        public int LineNo { get; set; }
        public DateTime MainAccDate { get; set; }
        public decimal OldBalance { get; set; }
        public decimal Investment { get; set; }
        public string Comments { get; set; } 
        public decimal Collection { get; set; }
        public decimal Payments { get; set; }
        public decimal Expenses { get; set; }
        public decimal Withdraw { get; set; }
        public decimal ChittFund { get; set; }
        public decimal WeekInterest { get; set; }
        public decimal TotalInterest { get; set; }
        public decimal Total { get; set; }
        public ICommand SaveCommand { get; set; }
        public MainAccountVM MainAccountOld { get; set; }
        public AddMainAccountViewModel(INavigation navigation, IAlertService alertService, int lineNo)
        {
            LineNo = lineNo;
            _navigation = navigation;
            _alertService = alertService;
            _mainAccountService = new MainAccountService();
            MainAccDate = DateTime.Now;
            SaveCommand = new Command(async () => 
            {
                try
                {
                    IsBusy = true;
                    var res = await _mainAccountService.AddMainAccount(new MainAccountVM
                    {
                        Investment = Investment,
                        Date = MainAccDate,
                        OldBalance = OldBalance,
                        Coments = Comments,
                        Collection = Collection,
                        Payments = Payments,
                        Withdraw = Withdraw,
                        Expenses = Expenses,
                        ChittFund = ChittFund,
                        LineDay = MainAccDate.Date.ToString("ddd").ToUpper(),
                        LineId = LineNo,
                        UserId = StaticVariableHelper.LoggedUser.Id,
                        TotalInterest = TotalInterest,
                        WeekInterest = WeekInterest,
                        Total = Total
                    });
                    _alertService.DisplayAlert("MainAccount", res, "Ok");
                    IsBusy = false;
                    await _navigation.PopAsync();
                }
                catch (Exception ex)
                {
                    _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);
                    IsBusy = false;
                }
            });
        }

        public async Task GetOldAccount()
        {
            IsBusy = true;
            try
            {
                MainAccountOld = await _mainAccountService.GetOldAccount(LineNo, MainAccDate);
                if (MainAccountOld != null)
                {
                    OldBalance = MainAccountOld.Total;
                    OnPropertyChanged("OldBalance");
                    TotalInterest = MainAccountOld.TotalInterest;
                    OnPropertyChanged("TotalInterest");
                }
            }
            catch (Exception ex)
            {
                _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);
            }            
            IsBusy = false;
        }

        public void Calculate()
        {
            try
            {
                Total = OldBalance + Investment + Collection - Payments - Expenses - Withdraw - ChittFund;
                OnPropertyChanged("Total");
                if (Payments != 0)
                {
                    WeekInterest = CalculateInterest();
                    OnPropertyChanged("WeekInterest");
                    if (MainAccountOld != null)
                    {
                        TotalInterest = MainAccountOld.TotalInterest + WeekInterest;
                    }
                    else
                    {
                        TotalInterest = WeekInterest;
                    }
                    OnPropertyChanged("TotalInterest");
                }
            }
            catch (Exception ex)
            {
                _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);
            }
        }

        private decimal CalculateInterest()
        {
            return (StaticVariableHelper.Lines.FirstOrDefault(x=>x.Id==LineNo).InterestPer980 * (Payments / 980)) - Payments;
        }
    }
}
