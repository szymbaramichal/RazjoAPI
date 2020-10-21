using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Models
{
    public class ResetPassword
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Email { get; set; }
        public string ResetCode { get; set; }
    }
}