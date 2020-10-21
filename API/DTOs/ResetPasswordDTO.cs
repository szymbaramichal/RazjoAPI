namespace API.DTOs
{
    public class ResetPasswordDTO
    {
        public string Email { get; set; }
        public string ResetCode { get; set; }
        public string Password { get; set; }
    }
}