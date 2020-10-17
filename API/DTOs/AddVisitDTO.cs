using System;
using API.Helpers;

namespace API.DTOs
{
    public class AddVisitDTO
    {
        public string FamilyId { get; set; }
        public Date Date { get; set; }
        public string Message { get; set; }
    }
}