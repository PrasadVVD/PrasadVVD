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
    public partial class ItemsPage : ContentPage
    {
        private readonly ItemsViewModel _vm;
        public ItemsPage()
        {
            InitializeComponent();
            BindingContext = _vm = new ItemsViewModel(Navigation, new AlertService());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _vm.GetItems();
            if (_vm.ItemsList != null && !_vm.ItemsList.Any())
            {
                SlNoItems.IsVisible = true;
            }
            AddItemBtn.IsVisible = StaticVariableHelper.LoggedUser.Role == UserRole.Admin;
        }

        private async void LinesLst_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var item = (ItemVM)e.SelectedItem;
            if (item != null)
            {
                //await _vm.GotoHomePage(depo.DepoId);
            }
            ((ListView)sender).SelectedItem = null;
        }
    }
}