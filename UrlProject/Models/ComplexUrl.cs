using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [PrimaryKey(nameof(FullUrl), nameof(ShortUrl))]
    public class ComplexUrl
    {
        [Required]
        public string FullUrl { get; set; }

        [Required]
        public string ShortUrl { get; set; }

        public int NumberOfUses { get; set; } = 0;

        public int? UserId { get; set; }
    }
}
