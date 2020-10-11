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
        #endregion

        #region CalendarMethods
        Task<ReturnCalendarNoteDTO> AddCalendarNote(CalendarNote calendarNote, string userId);
        Task<List<ReturnCalendarNoteDTO>> ReturnLastMonthNotes(string userId);
        #endregion

    }
}