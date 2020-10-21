namespace API.DTOs
{
    public class ValidateResetPasswordCodeDTO
    {
        public string ResetCode { get; set; }
        public string Email { get; set; }
    }
}