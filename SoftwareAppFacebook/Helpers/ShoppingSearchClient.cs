using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using SoftwareAppFacebook.Models;

namespace SoftwareAppFacebook
{
    public static class ShoppingSearchClient
    {
        private const string SearchApiTemplate = "https://www.googleapis.com/shopping/search/v1/public/products?key={0}&country=US&q={1}&alt=json";
        private static HttpClient client = new HttpClient();

        public static string AppKey = ConfigurationManager.AppSettings["Search:AppKey"];

        public static Task<SearchResult> GetProductsAsync(string query)
        {
            if (String.IsNullOrEmpty(AppKey))
            {
                throw new InvalidOperationException("Aratma:AppKey boş olamaz. Konfigürasyon dosyasında bunu set ettiğinize emin olun.");
            }

            query = query.Replace(" ", "+");
            string searchQuery = String.Format(SearchApiTemplate, AppKey, query);
            var response = client.GetAsync(searchQuery).Result.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<SearchResult>();
        }
    }
}
