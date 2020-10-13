using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;

namespace API.Helpers
{
    public interface IApiHelper
    {
        #region TestMethods
        Task<Value> GetValueById(string id);
        Task<Value> AddValue(Value value);
        #endregion

        #region UserMethods
        Task<bool> AddUser(User user, string password);
        Task<User> Login(string email, string password);
        Task<string> ReturnUserRole(string id);
        Task<string[]> ReturnUserName(string id);
        #endregion

        #region CalendarMethods
        Task<CalendarNote> AddCalendarNote(CalendarNote calendarNote, string userId);
        Task<List<CalendarNote>> ReturnActualMonthNotes(string userId);
        #endregion

        #region FamilyMethods
        Task<Family> CreateFamily(string id, string familyName);
        Task<Family> JoinToFamily(string invitationCode, string id);
        Task<ReturnFamilyDTO> ReturnFamilyInfo(string familyId);
        #endregion

    }
}