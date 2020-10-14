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
        Task<User> UpdateUserInfo(string id, string firstName, string surname);
        #endregion

        #region CalendarMethods
        Task<CalendarNote> AddCalendarNote(CalendarNote calendarNote, string userId);
        Task<List<CalendarNote>> ReturnActualMonthNotes(string familyId, string userId);
        Task<List<CalendarNote>> ReturnNotesForMonth(string familyId, string userId, int month);
        #endregion

        #region FamilyMethods
        Task<Family> CreateFamily(string id, string familyName);
        Task<Family> JoinToFamily(string invitationCode, string id);
        Task<ReturnFamilyDTO> ReturnFamilyInfo(string familyId, string userId);
        #endregion

    }
}