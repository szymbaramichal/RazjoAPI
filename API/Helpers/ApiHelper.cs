using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using MongoDB.Driver;

namespace API.Helpers
{
    public class ApiHelper : IApiHelper
    {
        #region Variables
        private IMongoCollection<User> _users;
        private IMongoCollection<Value> _values;
        private IMongoCollection<CalendarNote> _calendarNotes;
        private IMongoCollection<Family> _familes;
        private IMongoDatabase database;

        #endregion

        #region Constructor
        public ApiHelper()
        {
            var client = new MongoClient("mongodb+srv://test:test@main.qhp4n.mongodb.net/<dbname>?retryWrites=true&w=majority");
            database = client.GetDatabase("Main");
            _users = database.GetCollection<User>("Users");
            _values = database.GetCollection<Value>("Values");
            _calendarNotes = database.GetCollection<CalendarNote>("CalendarNotes");
            _familes = database.GetCollection<Family>("Families");
        }
        #endregion

        #region UserMethods
        public async Task<bool> AddUser(User user, string password)
        {
            if(await _users.Find<User>(x => x.Email == user.Email).AnyAsync()) return false;

            using var hmac = new HMACSHA512();

            user.Email = user.Email.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            user.PasswordSalt = hmac.Key;
            user.FirstName = "";
            user.Surname = "";
            user.FamilyId = "";

            await _users.InsertOneAsync(user);

            return true;
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await _users.Find<User>(x => x.Email == email.ToLower()).FirstOrDefaultAsync();
            if (user == null) return null;

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return null;
            }

            return user;
        }

        public async Task<string> ReturnUserRole(string id)
        {
            var user = await _users.Find<User>(x => x.Id == id).FirstOrDefaultAsync();

            return user.Role;
        }
        #endregion

        #region TestMethods
        public async Task<Value> GetValueById(string id)
        {
            return await _values.Find<Value>(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Value> AddValue(Value value)
        {
            await _values.InsertOneAsync(value);
            return value;
        }

        #endregion
    
        #region CalendarMethods
        public async Task<CalendarNote> AddCalendarNote(CalendarNote calendarNote, string userId)
        {
            calendarNote.Day = DateTime.Today.Day;
            calendarNote.Month = DateTime.Today.Month;
            calendarNote.Year = DateTime.Today.Year;
            calendarNote.UserId = userId;

            await _calendarNotes.InsertOneAsync(calendarNote);
            
            return calendarNote;
        }
        public async Task<List<CalendarNote>> ReturnActualMonthNotes(string userId)
        {
            var notes = await _calendarNotes.Find<CalendarNote>(x => x.UserId == userId && x.Month == DateTime.Today.Month).ToListAsync();

            return notes;
        }

        #endregion
    
        #region FamilyMethods
        public async Task<Family> CreateFamily(string id, string familyName)
        {
            Family familyToAdd = new Family{
                FamilyName = familyName,
                PSYId = id
            };

            await _familes.InsertOneAsync(familyToAdd);

            var user = await _users.Find<User>(x => x.Id == id).FirstOrDefaultAsync();
            user.IsFamilyMember = true;
            user.FamilyId = familyToAdd.Id;

            await _users.FindOneAndReplaceAsync<User>(x => x.Id == id, user);
            
            return familyToAdd;
        }
        #endregion
    }
}