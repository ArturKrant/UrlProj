using Microsoft.AspNetCore.Mvc;
using UrlProject.Logger;
using UrlProject.Services;

namespace UrlProject.Controllers
{
    [Route("api/UrlLog")]
    [ApiController]
    public class UrlUsageLogController : ControllerBase
    {
        private readonly UrlUsageLogService urlUsageLogService;

        public UrlUsageLogController(UrlUsageLogService urlUsageLogService) => 
            this.urlUsageLogService = urlUsageLogService;

        [HttpGet]
        [Route("get")]
        public async Task<List<UrlUsageLog>> GetUrlUsageLogs() =>
            await urlUsageLogService.GetUrlUsageLogs();
    }
}
