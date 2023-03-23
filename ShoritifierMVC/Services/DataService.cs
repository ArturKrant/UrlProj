using System.Net;

namespace ShoritifierMVC.Services
{
    public class DataService
    {
        public HttpClient Client { get; set; } = new HttpClient();

        public DataService(string uri)
        {
            if (string.IsNullOrEmpty(uri))
                uri = "https://localhost:7212";
            Client.BaseAddress = new Uri(uri);
        }

        public async Task<T?> GetResponseResult<T>(HttpResponseMessage response) =>
            response.StatusCode == HttpStatusCode.OK ? await response.Content.ReadFromJsonAsync<T>() : default;


    }
}
