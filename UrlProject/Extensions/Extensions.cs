using System.Runtime.CompilerServices;

namespace UrlProject.Extensions
{
    public static class Extensions
    {
        public static string GetFullDomain(this HttpContext httpContext)
        {
            var req = httpContext.Request;
            return $"{req.Scheme}://{req.Host}";
        }

        public static async Task<string?> GetIpAdress(this HttpContext httpContext) =>
            await Task.Run(() => { return httpContext.Connection.RemoteIpAddress?.ToString(); });
    }
}
