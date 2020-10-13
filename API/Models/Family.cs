using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Models
{
    public class Family
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FamilyName { get; set; }
        public string USRId { get; set; }
        public string PSYId { get; set; }
        public string InvitationCode { get; set; }
    }
}