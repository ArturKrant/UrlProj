using Microsoft.AspNetCore.Mvc;
using ShoritifierMVC.Intrerfaces;

namespace ShoritifierMVC.Services
{
    public class UrlService : DataService, IUrlService
    {
        string api = "api/ComplexUrl";

        public UrlService(string uri) : base(uri)
        {
        }

        public async Task<string> GetShortUrlAnonymous(string fullUrl) =>
            await GetResponseResult<string>(await Client.PostAsJsonAsync($"{api}/shortifier", fullUrl));

        public async Task<string> GetShortUrlAuthenticated(string fullUrl, int id) =>
            await GetResponseResult<string>(await Client.PostAsJsonAsync($"{api}/shortifier/{id}", fullUrl));

        public async Task<IActionResult> OnUseShortUrl(string shortUrl) =>
            await Client.GetFromJsonAsync<IActionResult>($"{api}/s/{shortUrl}");
    }
}
