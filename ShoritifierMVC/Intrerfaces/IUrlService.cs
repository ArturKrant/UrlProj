using Microsoft.AspNetCore.Mvc;
using ShoritifierMVC.Models;

namespace ShoritifierMVC.Intrerfaces
{
    public interface IUrlService
    {
        Task<string> GetShortUrlAnonymous(string fullUrl);

        Task<string> GetShortUrlAuthenticated(string fullUrl, int id);

        Task<IActionResult> OnUseShortUrl(string shortUrl);
    }
}
