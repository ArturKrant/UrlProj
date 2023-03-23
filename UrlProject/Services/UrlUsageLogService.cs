using API.DAL;
using UrlProject.Logger;

namespace UrlProject.Services
{
    public class UrlUsageLogService
    {
        private readonly DataContext data;
        private readonly ComplexUrlService complexUrlService;

        public UrlUsageLogService(DataContext data, ComplexUrlService complexUrlService)
        {
            this.data = data;
            this.complexUrlService = complexUrlService;
        }

        public async Task<List<UrlUsageLog>> GetUrlUsageLogs()
        {
            var log = await Task.Run(() => { return data.UrlUsageLogs.ToList(); });
            return log;
        }

        public async Task<int> AddToUrlUsageLogs(string shortUrl, string ipAdress)
        {
            await data.UrlUsageLogs.AddAsync(new UrlUsageLog { ShortUrl = shortUrl, IpAdress = ipAdress, Time = DateTime.Now });
            return await data.SaveChangesAsync();
        }
    }
}
