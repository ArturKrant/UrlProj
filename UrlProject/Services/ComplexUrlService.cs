using API.DAL;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace UrlProject.Services
{
    public class ComplexUrlService
    {
        private readonly DataContext data;

        public ComplexUrlService(DataContext data)
        {
            this.data = data;
        }

        public async Task<string> GetShortUrl(string url, string fullDomain, int id = 0){
            if(!ValidUrl(url))
            { return "This url isn't valid"; }
            if(id==0) //anonymous user
            {
                var complexUrl = await PullComplexUrlIfExists(url);
                if (complexUrl != null) 
                { return complexUrl!.ShortUrl!; }
                else
                {
                    var shortUrl = await RandomShortUrlString(url, fullDomain);
                    await SaveComplexUrl(url, shortUrl, id);
                    return shortUrl;
                }
            }
            else //logged in user
            {
                var complexUrl = await CheckIfUrlExistsInUserById(url, id);
                if (complexUrl != null)
                { return complexUrl!.ShortUrl!;}
                else
                { 
                    var shortUrl = await RandomShortUrlString(url, fullDomain);
                    await SaveComplexUrl(url, shortUrl, id);
                    return shortUrl;
                }
            }
        }

        public async Task<int> AddUsesToShortUrl(string shortUrl)
        {
            var complexUrl = await PullComplexUrlIfExists(shortUrl);
            if (complexUrl != null)
                complexUrl.NumberOfUses++;
            return await data.SaveChangesAsync();
        }

        public async Task SaveComplexUrl(string url, string shortUrl, int id)
        {
            ComplexUrl complexUrl;
            if (id == 0)
            { complexUrl = new ComplexUrl { FullUrl = url, ShortUrl = shortUrl, UserId = null }; }
            else
            { complexUrl = new ComplexUrl { FullUrl = url, ShortUrl = shortUrl, UserId = id }; }
            await data.ComplexUrls.AddAsync(complexUrl);
            await data.SaveChangesAsync();
        }

        public async Task<string> RandomShortUrlString(string url, string fullDomain)
        {
            string result;
            do
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var random = new Random();
                result = new string(
                    Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
            } while(await CheckIfUrlExistsInDb($"{fullDomain}/s/{result}"));
            
            return $"{fullDomain}/s/{result}";
        }

        public bool ValidUrl(string url)
        {
                try
                {
                    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                    request!.Method = "HEAD";
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    var result = response?.StatusCode == HttpStatusCode.OK;
                    response?.Close();
                    return result;
                }
                catch
                {
                    return false;
                }
            
        }

        public async Task<bool> CheckIfUrlExistsInDb(string url)
        {
            if (await data.ComplexUrls.FirstOrDefaultAsync(u => u.ShortUrl == url || u.FullUrl == url) == null)
                return false;
            return true;
        }

        public async Task<ComplexUrl?> CheckIfUrlExistsInUserById(string url, int id)
        {
            return await data.ComplexUrls.Where(u => u.UserId!=null).FirstOrDefaultAsync(u => u.UserId! == id && u.FullUrl == url);
        }

        public async Task<ComplexUrl?> PullComplexUrlIfExists(string url) => 
            await data.ComplexUrls.FirstOrDefaultAsync(u => u.ShortUrl == url || u.FullUrl == url);


    }
}
