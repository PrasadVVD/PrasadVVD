using ArtosApp.Helpers;
using ArtosApp.Services;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ArtosApp.ViewModels
{
    public class AddItemViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly IAlertService _alertService;
        private readonly ItemService _itemService;
        private readonly string _errorTitle = "Server Error";
        private readonly string _okButton = "Ok";

        public string ItemName { get; set; }
        public int BottlesPerCase { get; set; }
        public decimal CasePrice { get; set; }

        public ICommand SaveButton { get; set; }

        public AddItemViewModel(INavigation navigation, IAlertService alertService)
        {
            _navigation = navigation;
            _alertService = alertService;
            _itemService = new ItemService();
            SaveButton = new Command(async () => { await SaveItem(); });
        }

        private async Task SaveItem()
        {
            IsBusy = true;
            try
            {
                if (string.IsNullOrEmpty(ItemName))
                {
                    _alertService.DisplayAlert("Validation error", "Please enter prodcut name", _okButton);
                    IsBusy = false;
                    return;
                }
                var res = await _itemService.SaveNewItem(new Models.ItemVM
                {
                    ItemName = ItemName,
                    BottlesPerCase = BottlesPerCase,
                    CasePrice = CasePrice
                });

                _alertService.DisplayAlert("Add Item", res, "Ok");
                await GetItemsFromServer();
                IsBusy = false;
                await _navigation.PopAsync();
            }
            catch (Exception ex)
            {
                _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);
                IsBusy = false;
            }
        }

        private async Task GetItemsFromServer()
        {
            try
            {
                var items = await _itemService.GetItems();
                if (items != null)
                {
                    LocalStorage.SaveByKeyToCache("SavedItems", JsonConvert.SerializeObject(items));
                }
            }
            catch (Exception ex)
            {
                _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);
            }
        }
    }
}
