using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShoritifierMVC.Models
{
    public class UserDto
    {
        [Display(Name = "First Name")]
        [MinLength(3, ErrorMessage = "The input length is a minimum of 3")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MinLength(3, ErrorMessage = "The input length is a minimum of 3")]
        public string? LastName { get; set; }

        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "The input length is a minimum of 4")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The passwords don't match")]
        [Display(Name = "Verify Password")]
        public string? VerifyPassword { get; set; }
    }
}
