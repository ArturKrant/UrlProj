using System.ComponentModel.DataAnnotations;

namespace ShoritifierMVC.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [MinLength(3, ErrorMessage = "The input length is a minimum of 3")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MinLength(3, ErrorMessage = "The input length is a minimum of 3")]
        public string? LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [MinLength(4, ErrorMessage = "The input length is a minimum of 4")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Compare(nameof(Password), ErrorMessage = "The passwords don't match")]
        [Display(Name = "Verify Password")]
        public string? VerifyPassword { get; set; }

    }
}
