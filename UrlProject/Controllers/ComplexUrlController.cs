using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using UrlProject.Extensions;
using UrlProject.Services;

namespace UrlProject.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ComplexUrlController : ControllerBase
    {
        private readonly ComplexUrlService complexUrlService;
        private readonly UrlUsageLogService urlUsageLogService;

        public ComplexUrlController(ComplexUrlService complexUrlService, UrlUsageLogService urlUsageLogService)
        {
            this.complexUrlService = complexUrlService;
            this.urlUsageLogService = urlUsageLogService;
        }

        [HttpPost]
        [Route("shortifier")]
        public async Task<string> AnonymusShortifyFullUrl([FromBody] string fullUrl)
        {
            var result = await complexUrlService.GetShortUrl(fullUrl, HttpContext.GetFullDomain());
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost]
        [Route("shortifier/{id}")]
        public async Task<string> UserShortifyFullUrl([FromBody] string fullUrl, int id)
        {
            var result = await complexUrlService.GetShortUrl(fullUrl, HttpContext.GetFullDomain(), id);
            return JsonConvert.SerializeObject(result);
        }

        [HttpGet]
        [Route("/s/{shortUrl}")]
        public async Task<IActionResult> OnShortUrlUse(string shortUrl)
        {
            string ipAdress = await HttpContext.GetIpAdress();
            string fullShortUrl = $"{HttpContext.GetFullDomain()}/s/{shortUrl}";
            await urlUsageLogService.AddToUrlUsageLogs(fullShortUrl, ipAdress);
            await complexUrlService.AddUsesToShortUrl(fullShortUrl);
            var complexUrl = await complexUrlService.PullComplexUrlIfExists(fullShortUrl);
            return Redirect(complexUrl.FullUrl);
        }
    }
}
