using System.ComponentModel.DataAnnotations;

namespace ShoritifierMVC.Models
{
    public class ComplexUrl
    {
        [DataType(DataType.Url)]
        [Required]
        public string FullUrl { get; set; }
        
        public string ShortUrl { get; set; }

        public int NumberOfUses { get; set; }
    }
}
