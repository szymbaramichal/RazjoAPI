using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class AddCalendarNoteDTO
    {
        [Required]
        public string Message { get; set; }
    }
}