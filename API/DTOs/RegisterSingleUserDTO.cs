using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class RegisterSingleUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}