using ShoritifierMVC.Intrerfaces;
using ShoritifierMVC.Log;

namespace ShoritifierMVC.Services
{
    public class UrlUsageLogService : DataService, IUrlUsageLogService
    {
        string api = "api/UrlLog";

        public UrlUsageLogService(string uri) : base(uri)
        {
        }

        public async Task<List<UrlUsageLog>> GetUrlUsageLogs() =>
            await Client.GetFromJsonAsync<List<UrlUsageLog>>($"{api}/get");
    }
}
