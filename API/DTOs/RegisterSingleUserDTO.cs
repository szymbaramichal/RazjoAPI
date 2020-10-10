using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterSingleUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

    }
}