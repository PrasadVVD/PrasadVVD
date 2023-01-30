using ArtosApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ArtosApp.Services
{
    public class UserService
    {
        private readonly HttpClient _http;
        private readonly string _baseUrl;

        public UserService()
        {
            _http = new HttpClient();            
            _baseUrl = "http://chinnareddy-001-site1.atempurl.com/api/";
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
    }
}
