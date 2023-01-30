using ArtosApp.Helpers;
using ArtosApp.Models;
using ArtosApp.Services;
using ArtosApp.ViewModels;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArtosApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeposPage : ContentPage
    {
        private readonly DeposViewModel _vm;
        public DeposPage()
        {
            InitializeComponent();
            BindingContext = _vm = new DeposViewModel(Navigation, new AlertService());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _vm.GetDepos();
            if (_vm.DeposList!=null && !_vm.DeposList.Any())
            {
                SlNoDepos.IsVisible = true;
            }
            AddDepoBtn.IsVisible = StaticVariableHelper.LoggedUser.Role == UserRole.Admin;
        }

        private async void LinesLst_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var depo = (DepoVM)e.SelectedItem;
            if (depo != null)
            {
                await _vm.GotoHomePage(depo.DepoId);
            }
            ((ListView)sender).SelectedItem = null;
        }
    }
}