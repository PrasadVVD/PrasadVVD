using ArtosApp.Models;
using ArtosApp.Services;
using ArtosApp.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ArtosApp.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly IAlertService _alertService;

        public int DepoId { get; set; }
        public DepoVM Depo { get; set; }
        public string HeadingText { get; set; }
        public ObservableCollection<NavigationItem> NavigationItems { get; set; }

        public HomeViewModel(INavigation navigation, IAlertService alertService, DepoVM depo)
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
                                //await _navigation.PushAsync(new AddMainAccount(depo.DepoId));
                                await _navigation.PushAsync(new Register());
                            })
                        },
                        new NavigationItem
                        {
                            BackgroundColour = "#0197ff",
                            Icon = "icon_reports.png",
                            Label = "Reports",
                            NavigationCommand = new Command(async (param) =>
                            {
                                //await _navigation.PushAsync(new Reports(depo));
                                await _navigation.PushAsync(new Register());
                            })
                        },
                        new NavigationItem
                        {
                            BackgroundColour = "#0197ff",
                            Icon = "icon_products",
                            Label = "Product Management",
                            NavigationCommand = new Command(async (param) => 
                            {
                                await _navigation.PushAsync(new ItemsPage());
                            })
                        },
                        new NavigationItem
                        {
                            BackgroundColour = "#0197ff",
                            Icon = "icon_inventory.png",
                            Label = "Stock Management",
                            NavigationCommand = new Command(async (param) => 
                            {
                                //await _navigation.PushAsync(new BalanceTransferPage(line.Id))
                                await _navigation.PushAsync(new Register());
                            })
                        }
            };

            if (depo != null)
            {
                HeadingText = $"Welcome to {depo.DepoName} depo";
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
