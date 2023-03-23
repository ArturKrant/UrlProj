using System.ComponentModel.DataAnnotations;

namespace UrlProject.Logger
{
    public class UrlUsageLog
    {
        public int Id { get; set; }

        [Required]
        public string ShortUrl { get; set; }

        [Required]
        public string IpAdress { get; set; }

        [Required]
        public DateTime Time { get; set; }
    }
}
