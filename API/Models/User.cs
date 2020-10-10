using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public bool IsFamilyMember { get; set; }
        public string FamilyId { get; set; }

        //PSY-psychlogist, USR-normal user, PAR- parent
        public string Role { get; set; }
    }
}