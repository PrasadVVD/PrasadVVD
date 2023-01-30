using Newtonsoft.Json;
using SmartFinance.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartFinance.Services
{
    public class LineService
    {
        private readonly HttpClient _http;
        private readonly string _baseUrl;

        public LineService()
        {
            _http = new HttpClient();
            //_baseUrl = "https://chinnareddy.bsite.net/api/";
            //_baseUrl = "https://a0fc-123-201-208-198.ngrok.io/api/";
            _baseUrl = "http://chinnareddy-001-site1.atempurl.com/api/";
        }

        public async Task<List<LineVM>> GetLinesForUser(int userId)
        {
            try
            {
                var res = await _http.GetAsync(_baseUrl + $"Lines/GetLinesForUser?userId={userId}");
                var result = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<LineVM>>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> SaveLine(LineVM request)
        {
            try
            {
                var content = JsonConvert.SerializeObject(request);
                var requestStrContent = new StringContent(content, Encoding.UTF8, "application/json");
                var res = await _http.PostAsync(_baseUrl + "Lines/AddNewLine", requestStrContent);
                return await res.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> UpdateLine(LineVM request)
        {
            try
            {
                var content = JsonConvert.SerializeObject(request);
                var requestStrContent = new StringContent(content, Encoding.UTF8, "application/json");
                var res = await _http.PutAsync(_baseUrl + "Lines/UpdateLine", requestStrContent);
                return await res.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DeleteLine(int lineId)
        {
            try
            {
                var res = await _http.DeleteAsync(_baseUrl + $"Lines/RemoveLine?lineNo={lineId}");
                return await res.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> AddUserToLine(int lineId, int userId)
        {
            try
            {
                var res = await _http.PostAsync(_baseUrl + $"Lines/AddUserToLine?lineNo={lineId}&userId={userId}", null);
                return await res.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UserVM>> GetAllEmployeesForLine(int lineId)
        {
            try
            {
                var res = await _http.GetAsync(_baseUrl + $"Lines/GetAllowdUsers?lineId={lineId}");
                var result = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<UserVM>>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DeleteUserFromLine(int lineId, int userId)
        {
            try
            {
                var res = await _http.DeleteAsync(_baseUrl + $"Lines/RemoveUserFromLine?lineNo={lineId}&userId={userId}");
                return await res.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
