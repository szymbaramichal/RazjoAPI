using System;
using API.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Models
{
    public class Visit
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FamilyId { get; set; }
        public Date Date { get; set; }
        public string Message { get; set; }
    }
}