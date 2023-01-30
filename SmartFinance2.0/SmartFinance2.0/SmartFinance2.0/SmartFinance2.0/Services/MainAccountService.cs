using Newtonsoft.Json;
using SmartFinance.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartFinance.Services
{
    public class MainAccountService
    {
        private readonly HttpClient _http;
        private readonly string _baseUrl;

        public MainAccountService()
        {
            _http = new HttpClient();
            //_baseUrl = "https://chinnareddy.bsite.net/api/";
            //_baseUrl = "https://a0fc-123-201-208-198.ngrok.io/api/";
            _baseUrl = "http://chinnareddy-001-site1.atempurl.com/api/";
        }

        public async Task<List<string>> GetLineDays(int lineId)
        {
            try
            {
                var res = await _http.GetAsync(_baseUrl + $"MainAccount/GetLineDays?lineNo={lineId}");
                if (res.IsSuccessStatusCode)
                {
                    var result = await res.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<string>>(result);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<MainAccountVM>> GetMainAcounts(int lineId, string lineDay)
        {
            try
            {
                var res = await _http.GetAsync(_baseUrl + $"MainAccount/GetMainAccounts?lineNo={lineId}&lineDay={lineDay}");
                var result = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<MainAccountVM>>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MainAccountVM> GetOldAccount(int lineId, DateTime date)
        {
            try
            {
                string date1 = date.ToString("yyyy-MM-dd");
                var res = await _http.GetAsync(_baseUrl + $"MainAccount/GetOldAccount?lineId={lineId}&date={date1}");
                var result = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<MainAccountVM>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MainAccountVM> GetLastestAccountForDay(int lineId, string lineDay)
        {
            try
            {
                var res = await _http.GetAsync(_baseUrl + $"MainAccount/GetLastestAccountForDay?lineId={lineId}&lineDay={lineDay}");
                var result = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<MainAccountVM>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> AddMainAccount(MainAccountVM request)
        {
            try
            {
                var content = JsonConvert.SerializeObject(request);
                var requestStrContent = new StringContent(content, Encoding.UTF8, "application/json");
                var res = await _http.PostAsync(_baseUrl + "MainAccount/AddMainAccount", requestStrContent);
                return await res.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> UpdateMainAccount(MainAccountVM request)
        {
            try
            {
                var content = JsonConvert.SerializeObject(request);
                var requestStrContent = new StringContent(content, Encoding.UTF8, "application/json");
                var res = await _http.PutAsync(_baseUrl + "MainAccount/UpdateMainAccount", requestStrContent);
                return await res.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DeleteMainAccount(int id)
        {
            try
            {
                var res = await _http.DeleteAsync(_baseUrl + $"MainAccount/DeleteMainAccount?mainAccountId={id}");
                return await res.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> BalanceTransfer(BalanceTransferVM request)
        {
            try
            {
                var content = JsonConvert.SerializeObject(request);
                var requestStrContent = new StringContent(content, Encoding.UTF8, "application/json");
                var res = await _http.PostAsync(_baseUrl + "MainAccount/BalanceTransfer", requestStrContent);
                return await res.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<BalanceTransferVM>> GetBalanceTransfers(int lineId)
        {
            try
            {
                var res = await _http.GetAsync(_baseUrl + $"MainAccount/GetBalanceTransfers?lineId={lineId}");
                if (res.IsSuccessStatusCode)
                {
                    var result = await res.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<BalanceTransferVM>>(result);
                }
                else
                {
                    return null;
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
