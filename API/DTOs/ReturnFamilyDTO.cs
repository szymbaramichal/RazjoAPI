using System.Collections.Generic;

namespace API.DTOs
{
    public class ReturnFamilyDTO
    {
        public string FamilyId { get; set; }
        public string FamilyName { get; set; }
        public string PsyId { get; set; }
        public string PsychologistNames { get; set; }
        public string UsrId { get; set; }
        public string UserNames { get; set; }
        public string InvitationCode { get; set; }
        public List<ReturnCalendarNoteDTO> CalendarNotes { get; set; }
        public List<ReturnVisitDTO> Visits { get; set; }
    }
}