using System.Collections.Generic;

namespace API.DTOs
{
    public class ReturnUserDTO
    {
        public string Token { get; set; }
        public List<ReturnCalendarNoteDTO> CalendarNotes { get; set; }
    }
}