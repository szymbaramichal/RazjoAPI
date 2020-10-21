using API.Helpers;

namespace API.DTOs
{
    public class ReturnPrivateNoteDTO
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public Date CreationDate { get; set; }
    }
}