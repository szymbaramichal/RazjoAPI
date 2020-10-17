using API.Helpers;

namespace API.DTOs
{
    public class ReturnCalendarNoteDTO
    {
        public string UserId { get; set; }
        public string UserRole { get; set; }
        public Date Date { get; set; }
        public string Message { get; set; }
    }
}