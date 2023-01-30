using ArtosApp.Helpers;
using ArtosApp.Models;
using ArtosApp.Services;
using ArtosApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ArtosApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly IAlertService _alertService;
        private readonly ItemService _itemService;
        private readonly string _errorTitle = "Server Error";
        private readonly string _okButton = "Ok";

        public List<ItemVM> ItemsList { get; set; }
        public ICommand AddItemCommand { get; set; }        
        public ICommand GetItemsFromServerCommand { get; set; }        

        public ItemsViewModel(INavigation navigation, IAlertService alertService)
        {
            _navigation = navigation;
            _alertService = alertService;
            _itemService = new ItemService();            

            GetItemsFromServerCommand = new Command(async () =>
            {
                IsBusy = true;
                try
                {
                    StaticVariableHelper.Items = ItemsList = await GetItemsFromServer();
                    OnPropertyChanged("ItemsList");
                }
                catch (Exception ex)
                {
                    _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);
                }
                IsBusy = false;
            });

            AddItemCommand = new Command(async () =>
            {
                await _navigation.PushAsync(new AddItemPage());
            });
        }

        public async Task GetItems()
        {
            IsBusy = true;
            try
            {
                string savedItems = await LocalStorage.GetByKeyFromCacheAsync<string>("SavedItems");
                if (!string.IsNullOrEmpty(savedItems) && savedItems != "null")
                {
                    var items = JsonConvert.DeserializeObject<List<ItemVM>>(savedItems);
                    StaticVariableHelper.Items = ItemsList = SetBackgrounds(items);
                }
                else
                {
                    StaticVariableHelper.Items = ItemsList = await GetItemsFromServer();
                }
            }
            catch (Exception ex)
            {
                _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);
            }
            OnPropertyChanged("ItemsList");
            IsBusy = false;
        }

        public async Task<List<ItemVM>> GetItemsFromServer()
        {
            List<ItemVM> items = null;
            try
            {
                items = await _itemService.GetItems();
                if (items != null)
                {
                    items = SetBackgrounds(items);
                    LocalStorage.SaveByKeyToCache("SavedItems", JsonConvert.SerializeObject(items));
                }
                else
                {
                    items = new List<ItemVM>();
                }
                return items;
            }
            catch (Exception ex)
            {
                _alertService.DisplayAlert(_errorTitle, ex.Message, _okButton);
            }
            return items;
        }

        private List<ItemVM> SetBackgrounds(List<ItemVM> items)
        {
            int i = 1;
            foreach (var item in items)
            {
                item.BackGround = GetColor(i++);
                if (i == 10) { i = 1; }
            }
            return items;
        }

        private Color GetColor(int id)
        {
            switch (id)
            {
                case 1: return Color.CadetBlue;
                case 2: return Color.Crimson;
                case 3: return Color.DarkCyan;
                case 4: return Color.DarkOliveGreen;
                case 5: return Color.DarkSlateBlue;
                case 6: return Color.DarkSlateGray;
                case 7: return Color.DarkRed;
                case 8: return Color.Indigo;
                case 9: return Color.MediumBlue;
                case 10: return Color.Teal;
                default: return Color.Brown;
            }
        }
    }
}
