using ShoritifierMVC.Log;

namespace ShoritifierMVC.Intrerfaces
{
    public interface IUrlUsageLogService
    {
        Task<List<UrlUsageLog>> GetUrlUsageLogs();
    }
}
