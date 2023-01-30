using SmartFinance.Models;
using SmartFinance.Services;
using SmartFinance.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartFinance.ViewModels
{
    public class HomeViewModel :BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly IAlertService _alertService;

        public int LineNo { get; set; }
        public LineVM Line { get; set; }
        public string HeadingText { get; set; }        
        public ObservableCollection<NavigationItem> NavigationItems { get; set; }
        public HomeViewModel(INavigation navigation, IAlertService alertService, LineVM line)
        {
            _navigation = navigation;
            _alertService = alertService;            

            NavigationItems = new ObservableCollection<NavigationItem>{
                        new NavigationItem
                        {
                            BackgroundColour = "#0197ff",
                            Icon = "reports_weekly_payments.png",
                            Label = "Main Account",
                            NavigationCommand = new Command(async (param) =>
                            {
                                await _navigation.PushAsync(new AddMainAccount(line.Id));
                            })
                        },
                        new NavigationItem
                        {
                            BackgroundColour = "#0197ff",
                            Icon = "icon_reports.png",
                            Label = "Reports",
                            NavigationCommand = new Command(async (param) =>
                            {
                                await _navigation.PushAsync(new Reports(line));
                            })
                        },
                        new NavigationItem
                        {
                            BackgroundColour = "#0197ff",
                            Icon = "icon_settings.png",
                            Label = "Settings",
                            NavigationCommand = new Command(async (param) => await _navigation.PushAsync(new SettingsPage(line)))
                        },
                        new NavigationItem
                        {
                            BackgroundColour = "#0197ff",
                            Icon = "icon_bt.png",
                            Label = "Balance Transfer",
                            NavigationCommand = new Command(async (param) => await _navigation.PushAsync(new BalanceTransferPage(line.Id)))
                        }
            };

            if (line != null)
            {
                HeadingText = $"Welcome to {line.LineName} line";
            }            
        }
    }

    public class NavigationItem
    {
        public string BackgroundColour { get; set; }
        public ICommand NavigationCommand { get; set; }
        public string Label { get; set; }
        public ImageSource Icon { get; set; }
    }
}
