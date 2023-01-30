using SmartFinance.Models;
using SmartFinance.Services;
using SmartFinance.Views.ReportViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace SmartFinance.ViewModels
{
    public class ReportsViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly IAlertService _alertService;

        public LineVM Line { get; set; }
        public ObservableCollection<NavigationItem> NavigationItems { get; set; }
        public ReportsViewModel(INavigation navigation, IAlertService alertService, LineVM line)
        {
            _navigation = navigation;
            _alertService = alertService;
            Line = line;

            NavigationItems = new ObservableCollection<NavigationItem>
            {
                new NavigationItem
                {
                    BackgroundColour = "#2E8B57",
                    Icon = "reports_customers_payments_icon.png",
                    Label = "Main Account Report",
                    NavigationCommand = new Command(async (param) =>
                    {
                        await _navigation.PushAsync(new MainAccountReport(line.Id));
                    })
                }
                /*new NavigationItem
                {
                    BackgroundColour = "#DB7093",
                    Icon = "reports_weekly_payments.png",
                    Label = "Weekly Payment Report",
                    NavigationCommand = new Command(async (param) =>
                    {
                        await SafeNavigate(new WeeklyPaymentReportPageModel(LineNo));
                    })
                },
                new NavigationItem
                {
                    BackgroundColour = "#4682B4",
                    Icon = "icon_reports.png",
                    Label = "Single Customer Report",
                    NavigationCommand = new Command(async (param) =>
                    {
                        await SafeNavigate(new IndividualCustomerReportPageModel(LineNo));
                    })
                }*/
            };
        }

    }
}
