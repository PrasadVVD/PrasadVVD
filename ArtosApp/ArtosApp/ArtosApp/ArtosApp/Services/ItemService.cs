using ArtosApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ArtosApp.Services
{
    public class ItemService
    {
        private readonly HttpClient _http;
        private readonly string _baseUrl;

        public ItemService()
        {
            _http = new HttpClient();
            _baseUrl = "http://chinnareddy-001-site1.atempurl.com/api/";
        }

        public async Task<List<ItemVM>> GetItems()
        {
            try
            {
                var res = await _http.GetAsync(_baseUrl + $"Items/GetItems");
                var result = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ItemVM>>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> SaveNewItem(ItemVM request)
        {
            try
            {
                var content = JsonConvert.SerializeObject(request);
                var requestStrContent = new StringContent(content, Encoding.UTF8, "application/json");
                var res = await _http.PostAsync(_baseUrl + "Items/AddNewItem", requestStrContent);
                return await res.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
