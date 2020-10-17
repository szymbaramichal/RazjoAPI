using API.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Models
{
    public class CalendarNote
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserRole { get; set; }
        public string FamilyId { get; set; }
        public Date Date { get; set; }
        public string Message { get; set; }
    }
}