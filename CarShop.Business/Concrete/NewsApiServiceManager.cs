using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CarShop.Business.Abstract;
using CarShop.Entity.Models;
using Newtonsoft.Json;

namespace CarShop.Business.Concrete
{
    public class NewsApiServiceManager
    {
        private readonly HttpClient _httpClient;
        private const string API_KEY = "57075ed26475486480f9455672e92089"; 
        private const string BASE_URL = "https://newsapi.org/v2/";

        public NewsApiServiceManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<NewsApiResponse> GetLatestCarNews(string query = "car", string sortBy = "publishedAt", int page = 1)
        {
            var requestUrl = $"{BASE_URL}everything?q={query}&sortBy={sortBy}&page={page}&apiKey={API_KEY}";
            

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API Error: {response.StatusCode} - {response.ReasonPhrase}");
                }

                string json = await response.Content.ReadAsStringAsync();
                var newsResponse = JsonConvert.DeserializeObject<NewsApiResponse>(json);
                return newsResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                throw;
            }
        }
    }
}