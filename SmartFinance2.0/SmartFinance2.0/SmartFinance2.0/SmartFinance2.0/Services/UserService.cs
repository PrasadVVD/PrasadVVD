using Newtonsoft.Json;
using SmartFinance.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartFinance.Services
{
    public class UserService
    {
        private readonly HttpClient _http;
        private readonly string _baseUrl;
        public UserService()
        {
            _http = new HttpClient();
            //_baseUrl = "https://chinnareddy.bsite.net/api/";
            //_baseUrl = "https://a0fc-123-201-208-198.ngrok.io/api/";
            _baseUrl = "http://chinnareddy-001-site1.atempurl.com/api/";
        }

        public async Task<List<UserVM>> GetUsers()
        {
            try
            {
                var res = await _http.GetAsync(_baseUrl + "User");
                var result = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<UserVM>>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LoginResponse> UserLogin(LoginRequest request)
        {
            try
            {
                var content = JsonConvert.SerializeObject(request);
                var requestStrContent = new StringContent(content, Encoding.UTF8, "application/json");
                var res = await _http.PostAsync(_baseUrl + "User/Login", requestStrContent);
                var result = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LoginResponse>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> RegisterNewUser(UserVM request)
        {
            try
            {
                var content = JsonConvert.SerializeObject(request);
                var requestStrContent = new StringContent(content, Encoding.UTF8, "application/json");
                var res = await _http.PostAsync(_baseUrl + "User", requestStrContent);
                return await res.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserVM> GetUserByCode(string code)
        {
            try
            {
                var res = await _http.GetAsync(_baseUrl + $"User/GetUserByCode?code={code}");
                var result = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserVM>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> UpdatePassword(int userId, string password)
        {
            try
            {
                var res = await _http.PutAsync(_baseUrl + $"User/UpdatePassword?userId={userId}&newPassword={password}", null);
                return await res.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
