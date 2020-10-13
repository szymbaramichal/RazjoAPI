using System.Collections.Generic;

namespace API.DTOs
{
    public class ReturnFamilyDTO
    {
        public string FamilyId { get; set; }
        public string FamilyName { get; set; }
        public string UserNames { get; set; }
        public string PsychologistNames { get; set; }
        public List<ReturnCalendarNoteDTO> CalendarNotes { get; set; }
    }
}